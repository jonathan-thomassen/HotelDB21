using HotelDBConsole21.Models;
using HotelDBConsole21.Services;
using System;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms
        public static void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("5) Opdater et hotel");
            Console.WriteLine("6) List alle værelser");
            Console.WriteLine("7) List alle værelser til et bestemt hotel");
            Console.WriteLine("8) Flere menupunkter kommer snart :) ");
            Console.WriteLine("Q) Afslut");
        }

        public static bool Menu()
        {
            ShowOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "3":
                    RemoveHotel();
                    return true;
                case "4":
                    ShowHotelFromId();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "Q":
                case "q": return false;
                default: return true;
            }
        }

        private static void UpdateHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs nyt hotelnavn");
            string hotelName = Console.ReadLine();
            Console.WriteLine("Indlæs ny hotel-adresse");
            string hotelAddress = Console.ReadLine();

            var hs = new HotelService();

            Console.WriteLine(hs.UpdateHotel(new Hotel(hotelNo, hotelName, hotelAddress), hotelNo)
                ? "Hotellet blev opdateret."
                : "Fejl! Hotellet blev ikke opdateret.");
        }

        private static void RemoveHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelService();
            var hotel = hs.DeleteHotel(hotelNo);

            Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Name} Address {hotel.Address} has been removed from the database.");
        }

        private static void ShowHotelFromId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelService();
            var hotel = hs.GetHotelFromId(hotelNo);
            
            Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Name} Address {hotel.Address}");
        }

        private static void CreateHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotelnavn");
            string hotelName = Console.ReadLine();
            Console.WriteLine("Indlæs hotel-adresse");
            string hotelAddress = Console.ReadLine();

            var hs = new HotelService();

            Console.WriteLine(hs.CreateHotel(new Hotel(hotelNo, hotelName, hotelAddress))
                ? "Hotellet blev oprettet."
                : "Fejl! Hotellet blev ikke oprettet.");
        }

        private static void ShowHotels()
        {
            Console.Clear();
            var hs = new HotelService();
            var hotels = hs.GetAllHotel();
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Name} Address {hotel.Address}");
            }
        }
    }
}
