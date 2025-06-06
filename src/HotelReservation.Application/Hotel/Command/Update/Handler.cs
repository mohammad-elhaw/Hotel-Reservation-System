using HotelReservation.Domain;
using HotelReservation.Queries.Hotel.GetById;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Update;
public class Handler(
    Infrastructure.Hotel.Update.IRepository updateRepo,
    IRepository queryRepo
    ) : IRequestHandler<Request, Result<Domain.Entities.Hotel>>
{
    public async Task<Result<Domain.Entities.Hotel>> Handle(Request request, CancellationToken cancellationToken)
    {
        var hotel = await queryRepo.GetById(request.Id);
        if (hotel.IsFailure)
            return Result<Domain.Entities.Hotel>.Failure(hotel.Errors);

        var hotelEntity = hotel.Value!;

        var updatedHotel = Domain.Entities.Hotel.Update(hotelEntity, new Domain.Entities.Hotel.HotelData(
            request.Name,
            request.Address,
            request.Description,
            request.PhoneNumber,
            request.Email,
            request.Rating));

       
        if(updatedHotel.IsFailure)
            return Result<Domain.Entities.Hotel>.Failure(updatedHotel.Errors);

        updateRepo.UpdateHotel(updatedHotel.Value!);
        var updateResult = await updateRepo.SaveChanges();

        if(updateResult > 0)
            return Result<Domain.Entities.Hotel>.Success(updatedHotel.Value!);

        return Result<Domain.Entities.Hotel>.Failure(new List<string> { "Failed to update hotel." });
    }
}
