using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IGuestService
    {
        /// <summary>
        /// Henter alle gæster fra databasen
        /// </summary>
        /// <returns>Liste af gæster</returns>
        Task<List<Guest>> GetAllGuests();

        ///// <summary>
        ///// Henter en specifik gæst fra database 
        ///// </summary>
        ///// <param name="hotelNr">Udpeger det hotel der ønskes fra databasen</param>
        ///// <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        Task<Guest> GetGuestFromId(int guestNo);

        ///// <summary>
        ///// Indsætter et nyt hotel i databasen
        ///// </summary>
        ///// <param name="hotel">hotellet der skal indsættes</param>
        ///// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateGuest(Guest guest);

        ///// <summary>
        ///// Opdaterer en hotel i databasen
        ///// </summary>
        ///// <param name="hotel">De nye værdier til hotellet</param>
        ///// <param name="hotelNr">Nummer på den hotel der skal opdateres</param>
        ///// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateGuest(Guest guest, int guestNo);

        ///// <summary>
        ///// Sletter et hotel fra databasen
        ///// </summary>
        ///// <param name="hotelNr">Nummer på det hotel der skal slettes</param>
        ///// <returns>Det hotel der er slettet fra databasen, returnere null hvis hotellet ikke findes</returns>
        Task<Guest> DeleteGuest(int guestNo);

        ///// <summary>
        ///// henter alle hoteller fra databasen
        ///// </summary>
        ///// <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        ///// <returns></returns>
        Task<List<Guest>> GetGuestsByName(string name);
    }
}
