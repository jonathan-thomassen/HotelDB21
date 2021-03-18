using System;

namespace RazorPageHotelApp.Models
{
    public class Booking
    {
        public int BookingNo { get; set; }
        public int HotelNo { get; set; }
        public int GuestNo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomNo { get; set; }

        public Booking(int bookingNo, int hotelNo, int guestNo, DateTime dateFrom, DateTime dateTo, int roomNo)
        {
            BookingNo = bookingNo;
            HotelNo = hotelNo;
            GuestNo = guestNo;
            DateFrom = dateFrom;
            DateTo = dateTo;
            RoomNo = roomNo;
        }

        public override string ToString()
        {
            return $"{nameof(BookingNo)}: {BookingNo}, {nameof(HotelNo)}: {HotelNo}, {nameof(GuestNo)}: {GuestNo}, {nameof(DateFrom)}: {DateFrom}, {nameof(DateTo)}: {DateTo}, {nameof(RoomNo)}: {RoomNo}";
        }
    }
}