using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// Henter alle værelser fra databasen.
        /// </summary>
        /// <returns>En liste med værelser</returns>
        Task<List<Room>> GetAllRooms();

        /// <summary>
        /// Henter alle værelser fra databasen der tilhører et specifikt hotel-id.
        /// </summary>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <returns>En liste med værelser</returns>
        Task<List<Room>> GetAllRoomsFromHotelId(int hotelNo);

        /// <summary>
        /// Henter alle værelser fra databasen der tilhører et specifikt hotel-id, filtreret efter type.
        /// </summary>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <param name="type">Angiver typen på værelserne man ønsker at finde</param>
        /// <returns>En liste med værelser</returns>
        Task<List<Room>> GetAllRoomsFromHotelIdFilterByType(int hotelNo, char type);

        /// <summary>
        /// Henter alle værelser fra databasen, filtreret efter type.
        /// </summary>
        /// <param name="type">Angiver typen på værelserne man ønsker at finde</param>
        /// <returns>En liste med værelser</returns>
        Task<List<Room>> GetAllRoomsFilterByType(char type);

        /// <summary>
        /// Henter et værelse fra databasen der har et specifikt hotel-id og værelse-nr.
        /// </summary>
        /// <param name="roomNo">Angiver nr på værelset man ønsker at finde</param>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <returns>Et værelse</returns>
        Task<Room> GetRoomFromRoomId(int roomNo, int hotelNo);

        /// <summary>
        /// Opretter et værelse og knytter det til et bestemt hotel-id.
        /// </summary>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <param name="room">Værelset man ønsker at oprette</param>
        /// <returns>True hvis det lykkedes at oprette værelset, ellers false.</returns>
        Task<bool> CreateRoom(int hotelNo, Room room);

        /// <summary>
        /// Opdatterer et specifikt værelse og indsætter ny information i dets sted.
        /// </summary>
        /// <param name="room">Værelset man ønsker at udskifte det eksisterende værelse med</param>
        /// <param name="roomNo">Angiver nr på værelset man ønsker at opdatere</param>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <returns>True hvis det lykkedes at opdatere værelset, ellers false.</returns>
        Task<bool> UpdateRoom(Room room, int roomNo, int hotelNo);

        /// <summary>
        /// Sletter et værelse fra databasen.
        /// </summary>
        /// <param name="roomNo">Angiver nr på værelset man ønsker at slette</param>
        /// <param name="hotelNo">Angiver id'et på hotellet der hentes fra databasen</param>
        /// <returns>Det værelse der blev slettet</returns>
        Task<Room> DeleteRoom(int roomNo, int hotelNo);
    }
}