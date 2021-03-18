using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class GuestService : Connection, IGuestService
    {
        private string _queryStringAllGuests = "select * from Guest";
        private string _queryStringFromId = "select * from Guest where Guest_No = @ID";
        private string _queryStringFromName = "select * from Guest where Name like @Name";
        private string _insertSql = "insert into Guest values (@GuestID, @Name, @Address)";
        private string _deleteSql = "delete from Guest where Guest_No = @GuestID";
        private string _updateSql = "update Guest set Name = @Name, Address = @Address where Guest_No = @GuestID";

        public List<Guest> GetAllGuests()
        {
            var guests = new List<Guest>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringAllGuests, connection);
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

        public bool CreateGuest(Guest guest)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_insertSql, connection);

            command.Parameters.AddWithValue("@GuestID", guest.GuestNo);
            command.Parameters.AddWithValue("@Name", guest.Name);
            command.Parameters.AddWithValue("@Address", guest.Address);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public bool UpdateGuest(Guest guest, int guestNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_updateSql, connection);

            command.Parameters.AddWithValue("@Name", guest.Name);
            command.Parameters.AddWithValue("@Address", guest.Address);
            command.Parameters.AddWithValue("@ID", guestNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public Guest DeleteGuest(int guestNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_deleteSql, connection);

            command.Parameters.AddWithValue("@ID", guestNo);

            var guest = GetGuestFromId(guestNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return guest;

            return null;
        }

        public Guest GetGuestFromId(int guestNo)
        {
            var guest = new Guest();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringFromId, connection);
            command.Parameters.AddWithValue("@ID", guestNo);

            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var guestName = reader.GetString(1);
                var guestAddress = reader.GetString(2);
                guest = new Guest(guestNo, guestName, guestAddress);
            }
            else
            {
                guest = null;
            }
            connection.Close();

            return guest;
        }

        public List<Guest> GetGuestsByName(string name)
        {
            var guests = new List<Guest>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringFromName, connection);
            command.Parameters.AddWithValue("@Name", "%" + name + "%");

            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var guestNo = reader.GetInt32(0);
                var guestName = reader.GetString(1);
                var guestAdr = reader.GetString(2);
                var guest = new Guest(guestNo, guestName, guestAdr);
                guests.Add(guest);
            }
            connection.Close();

            if (guests.Count >= 1)
                return guests;

            return null;
        }
    }
}
