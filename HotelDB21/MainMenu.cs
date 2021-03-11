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
            Console.WriteLine("12) Opdater et værelse");
            Console.WriteLine("13) List alle gæster");
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
                case "Q":
                case "q": return false;
                default: return true;
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
            var hs = new HotelService();
            var hotels = hs.GetHotelsByName(hotelName);

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
            var roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs ny værelsetype");
            var roomType = Console.ReadLine();
            Console.WriteLine("Indlæs ny værelsepris");
            var roomPrice = Convert.ToDouble(Console.ReadLine());
            var rs = new RoomService();

            Console.WriteLine(rs.UpdateRoom(new Room(roomNo, roomType[0], roomPrice, hotelNo), roomNo, hotelNo)
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
            var rs = new RoomService();

            var room = rs.DeleteRoom(roomNo, hotelNo);

            if (room != null)
            {
                Console.WriteLine($"Værelsenr: {room.RoomNr}, Type: {room.Types}, Pris: {room.Price} er blevet fjernet fra databasen.");
            }
            else
            {
                Console.WriteLine($"Værelsenummer {roomNo} findes ikke i databasen.");
            }
        }

        private static void CreateRoom()
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
            Console.WriteLine("Indlæs værelsetype");
            string roomType = Console.ReadLine();
            Console.WriteLine("Indlæs værelsepris");
            double roomPrice = Convert.ToDouble(Console.ReadLine());
            var rs = new RoomService();

            Console.WriteLine(rs.CreateRoom(hotelNo, new Room(roomNo, roomType[0], roomPrice))
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

            var rs = new RoomService();
            var hs = new HotelService();
            var hotel = hs.GetHotelFromId(hotelNo);
            var room = rs.GetRoomFromRoomId(roomNo, hotelNo);

            Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Name}.");
            Console.WriteLine($"Værelse-Nr: {room.RoomNr}, Pris: {room.Price:C}, Type: {room.Types}.");
        }

        private static void ShowAllRoomsFromHotelId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var rs = new RoomService();
            var hs = new HotelService();
            var rooms = rs.GetAllRoomsFromHotelId(hotelNo);
            var hotel = hs.GetHotelFromId(hotelNo);

            Console.WriteLine($"Liste over alle værelser på {hotel.Name}:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"Værelse-Nr: {room.RoomNr}, Pris: {room.Price:C}, Type: {room.Types}.");
            }
        }

        private static void ShowAllRooms()
        {
            Console.Clear();
            var rs = new RoomService();
            var rooms = rs.GetAllRooms();
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

            if (hotel != null)
            {
                Console.WriteLine($"Hotel-Nr: {hotel.HotelNr}, Navn: {hotel.Name}, Adresse: {hotel.Address} er blevet fjernet fra databasen.");
            }
            else
            {
                Console.WriteLine($"Hotelnummer {hotelNo} findes ikke i databasen.");
            }
        }

        private static void ShowHotelFromId()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            var hotelNo = Convert.ToInt32(Console.ReadLine());
            var hs = new HotelService();
            var hotel = hs.GetHotelFromId(hotelNo);

            if (hotel != null)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Name} Address {hotel.Address}");
            }
            else
            {
                Console.WriteLine($"Hotelnummer {hotelNo} findes ikke i databasen.");
            }
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
                Console.WriteLine($"Hotelnr: {hotel.HotelNr}, navn: {hotel.Name}, adresse: {hotel.Address}");
            }
        }
    }
}
