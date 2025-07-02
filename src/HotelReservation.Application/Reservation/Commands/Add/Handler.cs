using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Reservation.Commands.Add;
public class Handler(
    HotelReservation.Queries.Reservation.GetAll.IRepository reservationGetRepo,
    HotelReservation.Queries.Room.GetByIds.IRepository getRoomsByIdsRepo,
    HotelReservation.Queries.Hotel.GetById.IRepository hotelRepo,
    Infrastructure.Reservation.Add.IRepository reservationAddRepo,
    Infrastructure.UnitOfWork.IRepository unitOfWork) 
    : IRequestHandler<Request, Result<Response>>
{
    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationMap = await reservationGetRepo.GetValidReservationsForRooms(request.RoomIds);

        var hotelResult = await hotelRepo.GetById(request.HotelId);

        if (hotelResult.IsFailure)
            return Result<Response>.Failure(
                hotelResult.Errors, hotelResult.StatusCode);

        List<Guid> unAvailableRoomIds = [];
        foreach (var roomId in request.RoomIds)
        {
            var reservations = reservationMap.ContainsKey(roomId)
                ? reservationMap[roomId]
                : [];
        
            var isAvailable = Domain.Entities.Room.IsAvailableBetween(
                request.CheckInDate, request.CheckOutDate, reservations);

            if(isAvailable.IsFailure)
                return Result<Response>.Failure(
                    isAvailable.Errors, isAvailable.StatusCode);
            
            if(!isAvailable.Value)
                unAvailableRoomIds.Add(roomId);
        }

        if(unAvailableRoomIds.Count > 0)
            return Result<Response>.Failure(
                [$"Some rooms are unavailable: {string.Join(",", unAvailableRoomIds)}"]);


        
        var reservationResult = Domain.Entities.Reservation
            .Create(new Domain.Entities.Reservation.ReservationData
        (
            CheckInDate: request.CheckInDate,
            CheckOutDate: request.CheckOutDate,
            TotalPrice: 0,
            CustomerName: request.CustomerName,
            CustomerEmail: request.CustomerEmail,
            CustomerPhoneNumber: request.CustomerPhoneNumber,
            HotelId: request.HotelId
        ));

        if(reservationResult.IsFailure)
            return Result<Response>.Failure(
                reservationResult.Errors, reservationResult.StatusCode);

        double total = 0;

        ///// we need to make room unavailable only if the reservation is confirmed

        var roomsResult = await getRoomsByIdsRepo.GetByIds(request.RoomIds, request.HotelId);

        if (roomsResult.IsFailure)
            return Result<Response>.Failure(roomsResult.Errors, roomsResult.StatusCode);

        Domain.Entities.Reservation reservation = reservationResult.Value!;

        foreach (var room in roomsResult.Value!)
        {
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
            return Result<Response>.Success(MappingToResponse(reservation, hotelResult.Value!),
                StatusCodes.Status409Conflict,
                saveResult.Code);

        return saveResult.IsSuccess
            ? Result<Response>.Success(MappingToResponse(reservation, hotelResult.Value!),
                StatusCodes.Status201Created, saveResult.Code)
            : Result<Response>.Failure(
                ["Failed to create reservation"], 
                StatusCodes.Status500InternalServerError,
                saveResult.Code);
    }

    private static Response MappingToResponse(Domain.Entities.Reservation reservation,
        Domain.Entities.Hotel hotel)
    {
        return new Response(
            Id: reservation.Id,
            CheckInDate: reservation.CheckInDate,
            CheckOutDate: reservation.CheckOutDate,
            CreatedAt: reservation.CreatedAt,
            TotalPrice: reservation.TotalPrice,
            CustomerName: reservation.CustomerName,
            CustomerEmail: reservation.CustomerEmail,
            CustomerPhoneNumber: reservation.CustomerPhoneNumber,
            Status: reservation.Status,
            Hotel: new HotelResponse(
                Id: hotel.Id,
                Name: hotel.Name,
                Address: hotel.Address,
                Description: hotel.Description,
                Rating: hotel.Rating));
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
