using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Reservation.Commands.Cancel;
public class Handler(
    HotelReservation.Queries.Reservation.GetById.IRepository getReservationRepo,
    Infrastructure.Reservation.Cancel.IRepository cancelReservationRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationResult = await getReservationRepo.GetById(request.ReservationId);
        if (reservationResult.IsFailure)
            return Result.Failure(reservationResult.Errors, reservationResult.StatusCode);

        reservationResult.Value!.Status = Domain.Entities.Enums.BookingStatus.Cancelled;

        cancelReservationRepo.CancelReservation(reservationResult.Value!);

        int saveResult = await cancelReservationRepo.SaveChanges();

        return saveResult > 0
            ? Result.Success(StatusCodes.Status200OK, "Reservation Cancelled Successfully.")
            : Result.Failure(
                ["Failed to cancel reservation, please try again later."],
                StatusCodes.Status500InternalServerError);
    }
}
