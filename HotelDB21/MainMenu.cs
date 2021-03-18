using HotelDBConsole21.Models;
using HotelDBConsole21.Services;
using System;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        public static void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List alle hoteller");
            Console.WriteLine("2) Opret nyt hotel");
            Console.WriteLine("3) Fjern hotel");
            Console.WriteLine("4) Søg efter hotel ud fra hotelnr");
            Console.WriteLine("5) Søg efter hotel(ler) med hotelnavn");
            Console.WriteLine("6) Opdatér et hotel");
            Console.WriteLine("7) List alle værelser");
            Console.WriteLine("8) List alle værelser tilhørende et bestemt hotel");
            Console.WriteLine("9) Søg efter værelse ud fra hotelnr og værelsenr");
            Console.WriteLine("10) Opret nyt værelse");
            Console.WriteLine("11) Fjern et værelse");
            Console.WriteLine("12) Opdatér et værelse");
            Console.WriteLine("13) List alle gæster");
            Console.WriteLine("14) List alle bookings");
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
                    ShowHotelsFromName();
                    return true;
                case "6":
                    UpdateHotel();
                    return true;
                case "7":
                    ShowAllRooms();
                    return true;
                case "8":
                    ShowAllRoomsFromHotelId();
                    return true;
                case "9":
                    ShowRoomFromRoomId();
                    return true;
                case "10":
                    CreateRoom();
                    return true;
                case "11":
                    RemoveRoom();
                    return true;
                case "12":
                    UpdateRoom();
                    return true;
                case "13":
                    ShowGuests();
                    return true;
                case "14":
                    ShowBookings();
                    return true;
                case "Q":
                case "q": return false;
                default: return true;
            }
        }

        private static void ShowBookings()
        {
            Console.Clear();
            var bs = new BookingService();
            var bookings = bs.GetAllBookings();

            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Bookingnr: {booking.BookingNo}, Gæstnr: {booking.GuestNo}, Hotelnr: {booking.HotelNo}," +
                                      $" Værelsenr: {booking.RoomNo}, Fra dato: {booking.DateFrom.ToShortDateString()}," +
                                      $" Til dato: {booking.DateTo.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("Ingen bookings i databasen.");
            }
        }

        private static void ShowGuests()
        {
            Console.Clear();
            var gs = new GuestService();
            var guests = gs.GetAllGuests();

            if (guests != null)
            {
                foreach (var guest in guests)
                {
                    Console.WriteLine($"Gæstnr: {guest.GuestNo}, navn: {guest.Name}, adresse: {guest.Address}");
                }
            }
            else
            {
                Console.WriteLine("Ingen gæster i databasen.");
            }
        }

        private static void ShowHotelsFromName()
        {
            Console.Clear();
            Console.WriteLine("Indlæs søgekriterie");
            var hotelName = Console.ReadLine();
            var hs = new HotelServiceAsync();
            var hotels = hs.GetHotelsByNameAsync(hotelName).Result;

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                {
                    Console.WriteLine($"Hotelnr: {hotel.HotelNr}, navn: {hotel.Name}, adresse: {hotel.Address}");
                }
            }
            else
            {
                Console.WriteLine("Ingen hoteller matchede dine søgekriterier.");
            }
        }

        private static void UpdateRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelServiceAsync();

            var hotel = hs.GetHotelFromIdAsync(hotelNo).Result;
            if (hotel != null)
            {
                Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Address}");
            }
            else
            {
                Console.WriteLine($"Hotelnummer {hotelNo} findes ikke i databasen.");
                return;
            }

            Console.WriteLine("Indlæs værelsenr");
            var roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs ny værelsetype");
            var roomType = Console.ReadLine();
            Console.WriteLine("Indlæs ny værelsepris");
            var roomPrice = Convert.ToDouble(Console.ReadLine());
            var rs = new RoomServiceAsync();

            Console.WriteLine(rs.UpdateRoomAsync(new Room(roomNo, roomType[0], roomPrice, hotelNo), roomNo, hotelNo).Result
                ? "Hotellet blev opdateret."
                : "Fejl! Hotellet blev ikke opdateret.");
        }

        private static void RemoveRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelService();

            var hotel = hs.GetHotelFromId(hotelNo);
            if (hotel != null)
            {
                Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Address}");
            }
            else
            {
                Console.WriteLine($"Hotelnummer {hotelNo} findes ikke i databasen.");
                return;
            }

            Console.WriteLine("Indlæs værelsenr");
            int roomNo = Convert.ToInt32(Console.ReadLine());
            var rs = new RoomServiceAsync();

            var room = rs.DeleteRoomAsync(roomNo, hotelNo).Result;

            Console.WriteLine(room != null
                ? $"Værelsenr: {room.RoomNr}, Type: {room.Types}, Pris: {room.Price} er blevet fjernet fra databasen."
                : $"Værelsenummer {roomNo} findes ikke i databasen.");
        }

        private static void CreateRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelServiceAsync();

            var hotel = hs.GetHotelFromIdAsync(hotelNo).Result;
            if (hotel != null)
            {
                Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Address}");
            }
            else
            {
                Console.WriteLine($"Hotelnummer {hotelNo} findes ikke i databasen.");
                return;
            }

            Console.WriteLine("Indlæs værelsenr");
            int roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs værelsetype");
            string roomType = Console.ReadLine();
            Console.WriteLine("Indlæs værelsepris");
            double roomPrice = Convert.ToDouble(Console.ReadLine());
            var rs = new RoomServiceAsync();

            Console.WriteLine(rs.CreateRoomAsync(hotelNo, new Room(roomNo, roomType[0], roomPrice)).Result
                ? "Værelset blev oprettet."
                : "Fejl! Værelset blev ikke oprettet.");
        }

        private static void ShowRoomFromRoomId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr:");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs værelsenr:");
            var roomNo = Convert.ToInt32(Console.ReadLine());

            var rs = new RoomServiceAsync();
            var hs = new HotelServiceAsync();
            var hotel = hs.GetHotelFromIdAsync(hotelNo).Result;
            var room = rs.GetRoomFromRoomIdAsync(roomNo, hotelNo).Result;

            Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Name}.");
            Console.WriteLine($"Værelse-Nr: {room.RoomNr}, Pris: {room.Price:C}, Type: {room.Types}.");
        }

        private static void ShowAllRoomsFromHotelId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var rs = new RoomServiceAsync();
            var hs = new HotelServiceAsync();
            var rooms = rs.GetAllRoomsFromHotelIdAsync(hotelNo).Result;
            var hotel = hs.GetHotelFromIdAsync(hotelNo).Result;

            Console.WriteLine($"Liste over alle værelser på {hotel.Name}:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"Værelse-Nr: {room.RoomNr}, Pris: {room.Price:C}, Type: {room.Types}.");
            }
        }

        private static void ShowAllRooms()
        {
            Console.Clear();
            var rs = new RoomServiceAsync();
            var rooms = rs.GetAllRoomsAsync().Result;
            foreach (var room in rooms)
            {
                Console.WriteLine($"Hotel-Nr: {room.HotelNr}, Værelse-Nr: {room.RoomNr}, Pris: {room.Price:C}, Type: {room.Types}.");
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

            var hs = new HotelServiceAsync();

            Console.WriteLine(hs.UpdateHotelAsync(new Hotel(hotelNo, hotelName, hotelAddress), hotelNo).Result
                ? "Hotellet blev opdateret."
                : "Fejl! Hotellet blev ikke opdateret.");
        }

        private static void RemoveHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelServiceAsync();
            var hotel = hs.DeleteHotelAsync(hotelNo).Result;

            Console.WriteLine(hotel != null
                ? $"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Address} er blevet fjernet fra databasen."
                : $"Hotelnummer {hotelNo} findes ikke i databasen.");
        }

        private static void ShowHotelFromId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelServiceAsync();
            var hotel = hs.GetHotelFromIdAsync(hotelNo).Result;

            Console.WriteLine(hotel != null
                ? $"HotelNr {hotel.HotelNr} Name {hotel.Name} Address {hotel.Address}"
                : $"Hotelnummer {hotelNo} findes ikke i databasen.");
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

            var hs = new HotelServiceAsync();

            Console.WriteLine(hs.CreateHotelAsync(new Hotel(hotelNo, hotelName, hotelAddress)).Result
                ? "Hotellet blev oprettet."
                : "Fejl! Hotellet blev ikke oprettet.");
        }

        private static void ShowHotels()
        {
            Console.Clear();
            var hs = new HotelServiceAsync();
            var hotels = hs.GetAllHotelsAsync().Result;
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"Hotelnr: {hotel.HotelNr}, navn: {hotel.Name}, adresse: {hotel.Address}");
            }
        }
    }
}
