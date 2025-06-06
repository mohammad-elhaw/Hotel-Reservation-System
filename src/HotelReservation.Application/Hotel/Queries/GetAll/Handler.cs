using HotelReservation.Domain;
using HotelReservation.Queries.Hotel.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Hotel.Queries.GetAll;
public class Handler(IRepository repository) : IRequestHandler<Query, Result<List<Response>>>
{
    public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var hotelsEntity = await repository.GetAll();

        if (hotelsEntity.IsFailure)
            return Result<List<Response>>.Failure(hotelsEntity.Errors, 
                StatusCodes.Status500InternalServerError);

        return Result<List<Response>>.Success(
            hotelsEntity.Value!.Select(h => new Response
            (
                Id: h.Id,
                Name: h.Name,
                Address: h.Address,
                Description: h.Description,
                Rating: h.Rating
            )).ToList()
        );
    }
}
