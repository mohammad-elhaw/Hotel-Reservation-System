using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Queries.GetAll;
public class Handler (
    HotelReservation.Queries.Reservation.GetAll.IRepository reservationRepo)
    : IRequestHandler<Request, Result<List<Response>>>
{
    public async Task<Result<List<Response>>> Handle(Request request, CancellationToken cancellationToken)
    {
        var reservationResult = await reservationRepo.GetAllReservations(request.HotelId);

        if(reservationResult.IsFailure)
            return Result<List<Response>>.Failure(reservationResult.Errors, 
                reservationResult.StatusCode);

        if (reservationResult.Value is null || reservationResult.Value.Count == 0)
            return Result<List<Response>>.Success(
                new List<Response>(),
                reservationResult.StatusCode,
                reservationResult.Code);

        return Result<List<Response>>.Success(reservationResult.Value
            .Select(r => new Response(
                r.Id,
                r.CheckInDate,
                r.CheckOutDate,
                r.CreatedAt,
                r.TotalPrice,
                r.CustomerName,
                r.CustomerEmail,
                r.CustomerPhoneNumber,
                r.Status,
                new Hotel.Queries.GetById.Response(
                    r.Hotel.Id,
                    r.Hotel.Name,
                    r.Hotel.Address,
                    r.Hotel.Description,
                    r.Hotel.Rating))).ToList());
    }
}
