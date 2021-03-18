namespace RazorPageHotelApp.Models
{
    public class Guest
    {
        public int GuestNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Guest()
        {
        }

        public Guest(int guestNo, string name, string address)
        {
            GuestNo = guestNo;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(GuestNo)}: {GuestNo}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}