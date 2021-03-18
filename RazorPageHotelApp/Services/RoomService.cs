using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class RoomService : Connection, IRoomService
    {
        private const string QueryStringAllRooms = "select * from Room order by Hotel_No asc, Room_No asc";
        private const string QueryStringAllRoomsFromHotelId = "select * from Room where Hotel_No = @HotelID";
        private const string QueryStringRoomFromRoomId = "select * from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private const string InsertSql = "insert into Room values (@RoomID, @HotelID, @Type, @Price)";
        private const string DeleteSql = "delete from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private const string UpdateSql = "update Room set Types = @Types, Price = @Price where Hotel_No = @HotelID and Room_No = @RoomID";

        public async Task<List<Room>> GetAllRooms()
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

        public async Task<List<Room>> GetAllRoomsFromHotelId(int hotelNo)
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

        public async Task<Room> GetRoomFromRoomId(int roomNo, int hotelNo)
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

        public async Task<bool> CreateRoom(int hotelNo, Room room)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(InsertSql, connection);

                command.Parameters.AddWithValue("@RoomID", room.RoomNo);
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

        public async Task<bool> UpdateRoom(Room room, int roomNo, int hotelNo)
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

        public async Task<Room> DeleteRoom(int roomNo, int hotelNo)
        {
            try
            {
                await using var connection = new SqlConnection(ConnectionString);
                await using var command = new SqlCommand(DeleteSql, connection);

                command.Parameters.AddWithValue("@HotelID", hotelNo);
                command.Parameters.AddWithValue("@RoomID", roomNo);

                var room = GetRoomFromRoomId(roomNo, hotelNo).Result;

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

        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
