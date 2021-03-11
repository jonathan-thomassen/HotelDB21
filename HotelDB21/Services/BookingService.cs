using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class BookingService : Connection, IBookingService
    {
        private string _queryStringAllGuests = "select * from Booking";
        private string _insertSql = "insert into Guest values (@GuestID, @Name, @Address)";
        private string _deleteSql = "delete from Guest where Guest_No = @GuestID";
        private string _updateSql = "update Guest set Name = @Name, Address = @Address where Guest_No = @GuestID";

        public List<Booking> GetAllBookings()
        {
            var bookings = new List<Booking>();

            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(_queryStringAllGuests, connection);
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
    }
}
