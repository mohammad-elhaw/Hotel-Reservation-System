using HotelReservation.Domain;
using HotelReservation.Queries.Hotel.GetById;
using MediatR;

namespace HotelReservation.Application.Hotel.Queries.GetById;
public class Handler(IRepository repository) : IRequestHandler<Query, Result<Response>>
{
    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var result = await repository.GetById(request.Id);

        if (result.IsFailure)
            return Result<Response>.Failure(result.Errors);

        var hotel = result.Value!;

        var response = new Response(
            hotel.Id,
            hotel.Name,
            new Domain.Entities.Address
            {
                City = hotel.Address.City,
                Country = hotel.Address.Country,
                State = hotel.Address.State,
                Street = hotel.Address.Street,
                ZipCode = hotel.Address.ZipCode
            },
            hotel.Description,
            hotel.Rating);

        return Result<Response>.Success(response);
    }

}
