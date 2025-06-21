using HotelReservation.Domain;
using HotelReservation.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Reservation.Commands.Add;
public class Handler(
    HotelReservation.Queries.Reservation.GetAll.IRepository reservationGetRepo,
    HotelReservation.Queries.Room.GetByIds.IRepository getRoomsByIdsRepo,
    Infrastructure.Room.Update.IRepository updateRoomRepo,
    Infrastructure.Reservation.Add.IRepository reservationAddRepo,
    Infrastructure.UnitOfWork.IRepository unitOfWork) 
    : IRequestHandler<Request, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationMap = await reservationGetRepo.GetReservationsForRooms(request.RoomIds);

        List<Guid> unAvailableRoomIds = [];
        foreach (var roomId in request.RoomIds)
        {
            var reservations = reservationMap.ContainsKey(roomId)
                ? reservationMap[roomId]
                : [];
        
            var isAvailable = Domain.Entities.Room.IsAvailableBetween(
                request.CheckInDate, request.CheckOutDate, reservations);

            if(isAvailable.IsFailure)
                return Result<Guid>.Failure(
                    isAvailable.Errors, isAvailable.StatusCode);
            
            if(!isAvailable.Value)
                unAvailableRoomIds.Add(roomId);
        }

        if(unAvailableRoomIds.Count > 0)
            return Result<Guid>.Failure(
                [$"Some rooms are unavailable: {string.Join(",", unAvailableRoomIds)}"]);


        var reservation = new Domain.Entities.Reservation
        {
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            CustomerPhoneNumber = request.CustomerPhoneNumber,
            HotelId = request.HotelId,
            Status = BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        double total = 0;

        ///// we need to get rooms first and then update them to make them unavailable

        var roomsResult = await getRoomsByIdsRepo.GetByIds(request.RoomIds, request.HotelId);

        if (roomsResult.IsFailure)
            return Result<Guid>.Failure(roomsResult.Errors, roomsResult.StatusCode);

        foreach(var room in roomsResult.Value!)
        {
            var updatedResult = Domain.Entities.Room.Update(room, new Domain.Entities.Room.UpdateRoomData(

                RoomNumber: room.RoomNumber,
                Type: room.Type,
                IsAvailable: false,
                Capacity: room.Capacity,
                Description: room.Description,
                HotelId: room.HotelId
            ));
            if(updatedResult.IsFailure)
                return Result<Guid>.Failure(updatedResult.Errors);

            updateRoomRepo.Update(updatedResult.Value!);
            double dynamicPrice = CalculateDynamicPrice(room, request.CheckInDate, request.CheckOutDate);
            total += dynamicPrice;

            reservation.ReservationRooms.Add(new Domain.Entities.ReservationRoom
            {
                RoomId = room.Id,
                ReservationId = reservation.Id,
                PricePerNight = dynamicPrice
            });
        }

        reservation.TotalPrice = total;
        reservationAddRepo.Add(reservation);
        var saveResult = await unitOfWork.SaveChanges();

        if(saveResult.IsSuccess && saveResult.StatusCode == StatusCodes.Status409Conflict)
            return Result<Guid>.Success(
                reservation.Id,
                StatusCodes.Status409Conflict,
                saveResult.Code);

        return saveResult.IsSuccess
            ? Result<Guid>.Success(reservation.Id, StatusCodes.Status201Created, saveResult.Code)
            : Result<Guid>.Failure(
                ["Failed to create reservation"], 
                StatusCodes.Status500InternalServerError,
                saveResult.Code);
    }

    private static double CalculateDynamicPrice(Domain.Entities.Room room, 
        DateTime checkIn, DateTime checkOut)
    {
        double totalPrice = 0;
        double basePrice = room.BasePricePerNight;
        int totalNights = (checkOut - checkIn).Days;

        for (DateTime date = checkIn; date < checkOut; date = date.AddDays(1))
        {
            double nightPrice = basePrice;

            // High season (July, August, September) +20%
            if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                nightPrice *= 1.2;
            }

            //  Weekend (Saturday or Sunday) +10%
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                nightPrice *= 1.1;
            }

            totalPrice += nightPrice;
        }

        // discount (7 nights or more) −10% off total
        if (totalNights >= 7)
        {
            totalPrice *= 0.9;
        }

        return Math.Round(totalPrice / totalNights, 2);
    }
}
