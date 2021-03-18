using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IHotelService
    {
        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        /// <returns>Liste af hoteller</returns>
        Task<List<Hotel>> GetAllHotels();

        /// <summary>
        /// Henter et specifik hotel fra database 
        /// </summary>
        /// <param name="hotelNo">Udpeger det hotel der ønskes fra databasen</param>
        /// <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        Task<Hotel> GetHotelFromId(int hotelNo);

        /// <summary>
        /// Indsætter et nyt hotel i databasen
        /// </summary>
        /// <param name="hotel">hotellet der skal indsættes</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateHotel(Hotel hotel);

        /// <summary>
        /// Opdaterer en hotel i databasen
        /// </summary>
        /// <param name="hotel">De nye værdier til hotellet</param>
        /// <param name="hotelNo">Nummer på den hotel der skal opdateres</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateHotel(Hotel hotel, int hotelNo);

        /// <summary>
        /// Sletter et hotel fra databasen
        /// </summary>
        /// <param name="hotelNo">Nummer på det hotel der skal slettes</param>
        /// <returns>Det hotel der er slettet fra databasen, returnere null hvis hotellet ikke findes</returns>
        Task<Hotel> DeleteHotel(int hotelNo);

        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        /// <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        /// <returns></returns>
        Task<List<Hotel>> GetHotelsByName(string name);
    }

}