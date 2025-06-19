using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Commands.Delete;
public class Handler(
    HotelReservation.Queries.Reservation.GetById.IRepository getReservationRepo,
    Infrastructure.Reservation.Delete.IRepository deleteReservationRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationResult = await getReservationRepo.GetById(request.ReservationId);

        if (reservationResult.IsFailure)
            return Result.Failure(reservationResult.Errors, reservationResult.StatusCode);

        deleteReservationRepo.Delete(reservationResult.Value!);

        return await deleteReservationRepo.SaveChanges();
    }
}
