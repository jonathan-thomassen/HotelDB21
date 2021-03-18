using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRooms();
        Task<List<Room>> GetAllRoomsFromHotelId(int hotelNo);
        Task<Room> GetRoomFromRoomId(int roomNo, int hotelNo);
        Task<bool> CreateRoom(int hotelNo, Room room);
        Task<bool> UpdateRoom(Room room, int roomNo, int hotelNo);
        Task<Room> DeleteRoom(int roomNo, int hotelNo);
    }
}