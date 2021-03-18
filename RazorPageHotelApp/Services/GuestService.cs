using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class GuestService : Connection, IGuestService
    {
        private string _queryStringAllGuests = "select * from Guest";
        private string _queryStringFromId = "select * from Guest where Guest_No = @ID";
        private string _queryStringFromName = "select * from Guest where Name like @Name";
        private string _insertSql = "insert into Guest values (@Name, @Address)";
        private string _deleteSql = "delete from Guest where Guest_No = @GuestID";
        private string _updateSql = "update Guest set Name = @Name, Address = @Address where Guest_No = @GuestID";

        public async Task<List<Guest>> GetAllGuests()
        {
            var guests = new List<Guest>();

            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_queryStringAllGuests, connection);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var guestNo = reader.GetInt32(0);
                var name = reader.GetString(1);
                var address = reader.GetString(2);
                var guest = new Guest(guestNo, name, address);
                guests.Add(guest);
            }
            await connection.CloseAsync();

            return guests;
        }

        public async Task<bool> CreateGuest(Guest guest)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_insertSql, connection);
            command.Parameters.AddWithValue("@Name", guest.Name);
            command.Parameters.AddWithValue("@Address", guest.Address);

            await connection.OpenAsync();
            var commandStatus = await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            return commandStatus > 0;
        }

        public async Task<bool> UpdateGuest(Guest guest, int guestNo)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_updateSql, connection);

            command.Parameters.AddWithValue("@Name", guest.Name);
            command.Parameters.AddWithValue("@Address", guest.Address);
            command.Parameters.AddWithValue("@ID", guestNo);

            await connection.OpenAsync();
            var commandStatus = await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            return commandStatus > 0;
        }

        public async Task<Guest> DeleteGuest(int guestNo)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_deleteSql, connection);
            command.Parameters.AddWithValue("@ID", guestNo);

            var guest = await GetGuestFromId(guestNo);

            await connection.OpenAsync();
            var commandStatus = await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            return commandStatus > 0 ? guest : null;
        }

        public async Task<Guest> GetGuestFromId(int guestNo)
        {
            Guest guest;

            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_queryStringFromId, connection);
            command.Parameters.AddWithValue("@ID", guestNo);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
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
            await connection.CloseAsync();

            return guest;
        }

        public async Task<List<Guest>> GetGuestsByName(string name)
        {
            var guests = new List<Guest>();

            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(_queryStringFromName, connection);
            command.Parameters.AddWithValue("@Name", "%" + name + "%");

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var guestNo = reader.GetInt32(0);
                var guestName = reader.GetString(1);
                var guestAdr = reader.GetString(2);
                var guest = new Guest(guestNo, guestName, guestAdr);
                guests.Add(guest);
            }
            await connection.CloseAsync();

            return guests.Count >= 1 ? guests : null;
        }

        public GuestService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
