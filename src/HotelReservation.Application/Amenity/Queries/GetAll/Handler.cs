using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Queries.GetAll;
public class Handler(
    HotelReservation.Queries.Amenity.GetAll.IRepository repository) 
    : IRequestHandler<Query, Result<List<Response>>>
{
    public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAll();

        if (result.IsFailure)
            return Result<List<Response>>.Failure(result.Errors, result.StatusCode);

        return Result<List<Response>>.Success(result.Value!.Select(a => new Response(
            a.Id,
            a.Name,
            a.Type)).ToList());
    }
}
