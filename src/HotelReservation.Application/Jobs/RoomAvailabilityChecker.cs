namespace HotelReservation.Application.Jobs;
public class RoomAvailabilityChecker(
    HotelReservation.Queries.Room.CleanUpRooms.IRepository cleanupRoomsRepo)
{
    public async Task CheckAndFreeRooms()
    {
        var roomsResult = await cleanupRoomsRepo.CleanupRoomAvailability();
        if(roomsResult.IsFailure)
        {
            // we need to Log the errors
        }

    }
}
