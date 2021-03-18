using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class RoomServiceAsync : Connection, IRoomServiceAsync
    {
        // indsæt sql strings
        private const string QueryStringAllRooms = "select * from Room order by Hotel_No asc, Room_No asc";
        private const string QueryStringAllRoomsFromHotelId = "select * from Room where Hotel_No = @HotelID";
        private const string QueryStringRoomFromRoomId = "select * from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private const string InsertSql = "insert into Room values (@RoomID, @HotelID, @Type, @Price)";
        private const string DeleteSql = "delete from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private const string UpdateSql = "update Room set Types = @Types, Price = @Price where Hotel_No = @HotelID and Room_No = @RoomID";

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = new List<Room>();

                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(QueryStringAllRooms, connection);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var roomNo = reader.GetInt32(0);
                    var hotelNo = reader.GetInt32(1);
                    var type = Convert.ToChar(reader.GetString(2));
                    var price = reader.GetDouble(3);
                    var room = new Room(roomNo, type, price, hotelNo);
                    rooms.Add(room);
                }
                await connection.CloseAsync();

                return rooms;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return null;
            }

        }

        public async Task<List<Room>> GetAllRoomsFromHotelIdAsync(int hotelNo)
        {
            try
            {
                var rooms = new List<Room>();

                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(QueryStringAllRoomsFromHotelId, connection);
                command.Parameters.AddWithValue("@HotelID", hotelNo);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var roomNo = reader.GetInt32(0);
                    var type = reader.GetString(2);
                    var price = reader.GetDouble(3);
                    var room = new Room(roomNo, type[0], price, hotelNo);
                    rooms.Add(room);
                }

                await connection.CloseAsync();

                return rooms;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return null;
            }
        }

        public async Task<Room> GetRoomFromRoomIdAsync(int roomNo, int hotelNo)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(QueryStringRoomFromRoomId, connection);
                command.Parameters.AddWithValue("@HotelID", hotelNo);
                command.Parameters.AddWithValue("@RoomID", roomNo);
                Room room;

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var type = reader.GetString(2);
                    var price = reader.GetDouble(3);
                    room = new Room(roomNo, type[0], price, hotelNo);
                }
                else
                {
                    room = null;
                }

                await connection.CloseAsync();

                return room;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return null;
            }
        }

        public async Task<bool> CreateRoomAsync(int hotelNo, Room room)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(InsertSql, connection);

                command.Parameters.AddWithValue("@RoomID", room.RoomNr);
                command.Parameters.AddWithValue("@HotelID", hotelNo);
                command.Parameters.AddWithValue("@Type", room.Types);
                command.Parameters.AddWithValue("@Price", room.Price);

                await connection.OpenAsync();
                var commandStatus = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                return commandStatus > 0;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNo, int hotelNo)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(UpdateSql, connection);

                command.Parameters.AddWithValue("@Price", room.Price);
                command.Parameters.AddWithValue("@Types", room.Types);
                command.Parameters.AddWithValue("@HotelID", hotelNo);
                command.Parameters.AddWithValue("@RoomID", roomNo);

                await connection.OpenAsync();
                var commandStatus = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                return commandStatus > 0;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return false;
            }
        }

        public async Task<Room> DeleteRoomAsync(int roomNo, int hotelNo)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(DeleteSql, connection);

                command.Parameters.AddWithValue("@HotelID", hotelNo);
                command.Parameters.AddWithValue("@RoomID", roomNo);

                var room = GetRoomFromRoomIdAsync(roomNo, hotelNo).Result;

                await connection.OpenAsync();
                var commandStatus = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                return commandStatus > 0 ? room : null;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine($"{sqlException.Message}");
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
                return null;
            }
        }
    }
}
