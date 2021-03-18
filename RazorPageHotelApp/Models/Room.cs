namespace RazorPageHotelApp.Models
{
    public class Room
    {
        public int RoomNo { get; set; }
        public char Types { get; set; }
        public double Price { get; set; }
        public int HotelNo { get; set; }

        public Room()
        {
        }

        public Room(int no, char types, double price)
        {
            RoomNo = no;
            Types = types;
            Price = price;
        }

        public Room(int no, char types, double price, int hotelNo) : this(no, types, price)
        {
            HotelNo = hotelNo;
        }

        public override string ToString()
        {
            return $"Room = {RoomNo}, Types = {Types}, Price = {Price}";
        }
    }
}