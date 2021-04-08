using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IHotelService
    {
        /// <summary>
        /// Henter alle hoteller fra databasen
        /// </summary>
        /// <returns>Liste af hoteller</returns>
        Task<List<Hotel>> GetAllHotels();

        /// <summary>
        /// Henter et specifikt hotel fra databasen
        /// </summary>
        /// <param name="hotelNo">Udpeger det hotel der ønskes fra databasen</param>
        /// <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        Task<Hotel> GetHotelFromId(int hotelNo);

        /// <summary>
        /// Indsætter et nyt hotel i databasen
        /// </summary>
        /// <param name="hotel">Hotellet der skal indsættes</param>
        /// <returns>Sand hvis det er gået godt, ellers falsk</returns>
        Task<bool> CreateHotel(Hotel hotel);

        /// <summary>
        /// Opdaterer et hotel i databasen
        /// </summary>
        /// <param name="hotel">De nye værdier til hotellet</param>
        /// <param name="hotelNo">Nummeret på det hotel der skal opdateres</param>
        /// <returns>Sand hvis det er gået godt, ellers falsk</returns>
        Task<bool> UpdateHotel(Hotel hotel, int hotelNo);

        /// <summary>
        /// Sletter et hotel fra databasen
        /// </summary>
        /// <param name="hotelNo">Nummeret på det hotel der skal slettes</param>
        /// <returns>Det hotel der er slettet fra databasen, returnerer null hvis hotellet ikke findes</returns>
        Task<Hotel> DeleteHotel(int hotelNo);

        /// <summary>
        /// Henter alle hoteller fra databasen hvori navnet indgår
        /// </summary>
        /// <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        /// <returns>De fundne hoteller</returns>
        Task<List<Hotel>> GetHotelsByName(string name);
    }

}