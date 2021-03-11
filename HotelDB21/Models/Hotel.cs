namespace HotelDBConsole21.Models
{
    public class Hotel
    {
        public int HotelNr { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Hotel()
        {

        }

        public Hotel(int hotelNr, string name, string address)
        {
            HotelNr = hotelNr;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}