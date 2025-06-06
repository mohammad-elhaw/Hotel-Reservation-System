using HotelReservation.Domain;
using HotelReservation.Queries.Hotel.GetById;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Delete;
public class Handler(
    Infrastructure.Hotel.Delete.IRepository deleteRepo,
    IRepository queryRepo
    ) : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var hotel = await queryRepo.GetById(request.Id);
        if (hotel.IsFailure)
            return Result.Failure(new List<string> { "Hotel not found" });

        deleteRepo.DeleteHotel(hotel.Value!);
        var result = await deleteRepo.SaveChanges();
        if (result > 0)
            return Result.Success();
        
        return Result.Failure(new List<string> { "Failed to delete hotel" });
    }
}
