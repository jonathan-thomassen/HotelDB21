using System.Collections.Generic;
using System.Threading.Tasks;
using HotelDBConsole21.Models;

namespace HotelDBConsole21.Interfaces
{
    public interface IRoomServiceAsync
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<List<Room>> GetAllRoomsFromHotelIdAsync(int hotelNr);
        Task<Room> GetRoomFromRoomIdAsync(int roomNr, int hotelNr);
        Task<bool> CreateRoomAsync(int hotelNr, Room room);
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr);
    }
}