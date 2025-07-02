using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Commands.Delete;
public class Handler(
    HotelReservation.Queries.Reservation.GetById.IRepository getReservationRepo,
    HotelReservation.Queries.Reservation.GetAllRooms.IRepository getReservationRoomsRepo,
    Infrastructure.Reservation.Delete.IRepository deleteReservationRepo,
    Infrastructure.Room.Update.IRepository updateRoomRepo,
    Infrastructure.UnitOfWork.IRepository unitOfWorkRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationResult = await getReservationRepo.GetById(request.ReservationId);

        if (reservationResult.IsFailure)
            return Result.Failure(reservationResult.Errors, reservationResult.StatusCode);

        var reservationRooms = await getReservationRoomsRepo.GetAllRooms(request.ReservationId);
        if (reservationRooms.IsFailure)
            return Result.Failure(reservationRooms.Errors, reservationRooms.StatusCode);

        // If there are rooms associated with the reservation, we can update them
        if (reservationRooms.Value!.Count > 0)
        {
            foreach(var reservationRoom in reservationRooms.Value)
            {
                // Update the room to make it available again
                reservationRoom.IsAvailable = true;
                updateRoomRepo.Update(reservationRoom);
            }
        }

        deleteReservationRepo.Delete(reservationResult.Value!);

        return await unitOfWorkRepo.SaveChanges();
    }
}
