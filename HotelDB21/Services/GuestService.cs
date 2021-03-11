using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class GuestService : Connection, IGuestService
    {
        private string _queryStringAllGuests = "select * from Guest";
        private string _insertSql = "insert into Guest values (@GuestID, @Name, @Address)";
        private string _deleteSql = "delete from Guest where Guest_No = @GuestID";
        private string _updateSql = "update Guest set Name = @Name, Address = @Address where Guest_No = @GuestID";

        public List<Guest> GetAllGuests()
        {
            var guests = new List<Guest>();

            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(_queryStringAllGuests, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var guestNo = reader.GetInt32(0);
                var name = reader.GetString(1);
                var adress = reader.GetString(2);
                var guest = new Guest(guestNo, name, adress);
                guests.Add(guest);
            }
            connection.Close();

            return guests;
        }
    }
}
