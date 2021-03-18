using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class BookingService : Connection, IBookingService
    {
        private string _queryStringAllGuests = "select * from Booking";
        private string _insertSql = "insert into Booking values (@BookingID, @HotelID, @GuestID, @DateFrom, @DateTo, @RoomID)";
        private string _deleteSql = "delete from Booking where Booking_No = @BookingID";
        //private string _updateSql = "update Booking set Name = @Name, Address = @Address where Guest_No = @GuestID";

        public List<Booking> GetAllBookings()
        {
            var bookings = new List<Booking>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringAllGuests, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var bookingNo = reader.GetInt32(0);
                var hotelNo = reader.GetInt32(1);
                var guestNo = reader.GetInt32(2);
                var dateFrom = reader.GetDateTime(3);
                var dateTo = reader.GetDateTime(4);
                var roomNo = reader.GetInt32(5);
                var booking = new Booking(bookingNo, hotelNo, guestNo, dateFrom, dateTo, roomNo);
                bookings.Add(booking);
            }
            connection.Close();

            return bookings;
        }

        public bool CreateBooking(Booking booking)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_insertSql, connection);

            command.Parameters.AddWithValue("@BookingID", booking.BookingNo);
            command.Parameters.AddWithValue("@HotelID", booking.HotelNo);
            command.Parameters.AddWithValue("@GuestID", booking.GuestNo);
            command.Parameters.AddWithValue("@DateFrom", booking.DateFrom);
            command.Parameters.AddWithValue("@DateTo", booking.DateTo);
            command.Parameters.AddWithValue("@RoomID", booking.RoomNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        //public Booking DeleteBooking(int bookingNo)
        //{
        //    using var connection = new SqlConnection(ConnectionString);
        //    using var command = new SqlCommand(_deleteSql, connection);

        //    command.Parameters.AddWithValue("@BookingID", bookingNo);

        //    var booking = GetBookingFromId(bookingNo);

        //    connection.Open();
        //    var commandStatus = command.ExecuteNonQuery();
        //    connection.Close();

        //    if (commandStatus > 0)
        //        return booking;

        //    return null;
        //}
    }
}
