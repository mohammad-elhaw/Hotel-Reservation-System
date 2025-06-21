using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Queries.GetById;
public class Handler(
    HotelReservation.Queries.Hotel.Exists.IRepository hotelExistsRepo,
    HotelReservation.Queries.Reservation.GetById.IRepository reservationRepo)
    : IRequestHandler<Request, Result<Response>>
{
    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var hotelExistsResult = await hotelExistsRepo.Exists(request.HotelId);
    
        if(hotelExistsResult.IsFailure)
            return Result<Response>.Failure(hotelExistsResult.Errors, 
                hotelExistsResult.StatusCode);
    
        if(hotelExistsResult.IsSuccess && !hotelExistsResult.Value)
            return Result<Response>.Failure(
                ["Hotel does not exist"],
                hotelExistsResult.StatusCode,
                "HotelNotFound");

        var reservationResult = await reservationRepo.GetById(request.ReservationId);

        return reservationResult.IsSuccess
            ? Result<Response>.Success(new Response(
                reservationResult.Value!.Id,
                reservationResult.Value.CheckInDate,
                reservationResult.Value.CheckOutDate,
                reservationResult.Value.CreatedAt,
                reservationResult.Value.TotalPrice,
                reservationResult.Value.CustomerName,
                reservationResult.Value.CustomerEmail,
                reservationResult.Value.CustomerPhoneNumber,
                reservationResult.Value.Status,
                new Hotel.Queries.GetById.Response(
                    reservationResult.Value.Hotel.Id,
                    reservationResult.Value.Hotel.Name,
                    reservationResult.Value.Hotel.Address,
                    reservationResult.Value.Hotel.Description,
                    reservationResult.Value.Hotel.Rating)),
                reservationResult.StatusCode)
            : Result<Response>.Failure(reservationResult.Errors,
                reservationResult.StatusCode);

    }
}
