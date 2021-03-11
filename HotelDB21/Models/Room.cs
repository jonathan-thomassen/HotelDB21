namespace HotelDBConsole21.Models
{
    public class Room
    {
        public int RoomNr { get; set; }
        public char Types { get; set; }
        public double Price { get; set; }
        public int HotelNr { get; set; }

        public Room()
        {

        }

        public Room(int nr, char types, double price)
        {
            RoomNr = nr;
            Types = types;
            Price = price;
        }

        public Room(int nr, char types, double price, int hotelNr) : this(nr, types, price)
        {
            HotelNr = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr}, Types = {Types}, Price = {Price}";
        }
    }
}