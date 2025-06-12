using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Queries.GetById;
public class Handler(
    HotelReservation.Queries.Amenity.GetById.IRepository repository) 
    : IRequestHandler<Query, Result<Response>>
{
    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var result = await repository.GetById(request.AmenityId);

        if (result.IsFailure)
            return Result<Response>.Failure(result.Errors, result.StatusCode);

        return Result<Response>.Success(new Response(
            result.Value!.Id,
            result.Value.Name,
            result.Value.Type));
    }
}
