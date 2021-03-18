namespace RazorPageHotelApp.Models
{
    public class Hotel
    {
        public int HotelNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Hotel()
        {
        }

        public Hotel(int hotelNo, string name, string address)
        {
            HotelNo = hotelNo;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNo)}: {HotelNo}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}