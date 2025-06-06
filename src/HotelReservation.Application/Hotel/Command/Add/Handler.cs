using HotelReservation.Domain;
using HotelReservation.Infrastructure.Hotel.Add;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Add;
public class Handler(IRepository repository) : IRequestHandler<Request, Result<Response>>
{
    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {

        var result = Domain.Entities.Hotel.Create
        (

            new Domain.Entities.Hotel.HotelData
            (
                request.Name,
                request.Address,
                request.Description,
                request.PhoneNumber,
                request.Email,
                request.Rating
            )
        );

        if (result.IsFailure)
            return Result<Response>.Failure(result.Errors);

        repository.AddHotel(result.Value!);
        await repository.SaveChanges();

        var response = new Response
        (
            result.Value!.Id,
            result.Value.Name,
            result.Value.Address,
            result.Value.Description,
            result.Value.PhoneNumber,
            result.Value.Email,
            result.Value.Rating
        );
        return Result<Response>.Success(response);
    }
}
