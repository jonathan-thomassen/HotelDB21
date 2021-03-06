using System;
using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class RoomService : Connection, IRoomService
    {
        // indsæt sql strings
        private string _queryStringAllRooms = "select * from Room order by Hotel_No asc, Room_No asc";
        private string _queryStringAllRoomsFromHotelId = "select * from Room where Hotel_No = @HotelID";
        private string _queryStringRoomFromRoomId = "select * from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private string _insertSql = "insert into Room values (@RoomID, @HotelID, @Type, @Price)";
        private string _deleteSql = "delete from Room where Hotel_No = @HotelID and Room_No = @RoomID";
        private string _updateSql = "update Room set Types = @Types, Price = @Price where Hotel_No = @HotelID and Room_No = @RoomID";

        public List<Room> GetAllRooms()
        {
            var rooms = new List<Room>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringAllRooms, connection);

            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var roomNo = reader.GetInt32(0);
                var hotelNo = reader.GetInt32(1);
                var type = Convert.ToChar(reader.GetString(2));
                var price = reader.GetDouble(3);
                var room = new Room(roomNo, type, price, hotelNo);
                rooms.Add(room);
            }
            connection.Close();

            return rooms;
        }

        public List<Room> GetAllRoomsFromHotelId(int hotelNo)
        {
            var rooms = new List<Room>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringAllRoomsFromHotelId, connection);
            command.Parameters.AddWithValue("@HotelID", hotelNo);

            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var roomNo = reader.GetInt32(0);
                var type = reader.GetString(2);
                var price = reader.GetDouble(3);
                var room = new Room(roomNo, type[0], price, hotelNo);
                rooms.Add(room);
            }
            connection.Close();

            return rooms;
        }

        public Room GetRoomFromRoomId(int roomNo, int hotelNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringRoomFromRoomId, connection);
            command.Parameters.AddWithValue("@HotelID", hotelNo);
            command.Parameters.AddWithValue("@RoomID", roomNo);
            Room room;

            connection.Open();
            var reader = command.ExecuteReader();
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
            connection.Close();

            return room;
        }

        public bool CreateRoom(int hotelNo, Room room)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_insertSql, connection);

            command.Parameters.AddWithValue("@RoomID", room.RoomNr);
            command.Parameters.AddWithValue("@HotelID", hotelNo);
            command.Parameters.AddWithValue("@Type", room.Types);
            command.Parameters.AddWithValue("@Price", room.Price);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public bool UpdateRoom(Room room, int roomNo, int hotelNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_updateSql, connection);

            command.Parameters.AddWithValue("@Price", room.Price);
            command.Parameters.AddWithValue("@Types", room.Types);
            command.Parameters.AddWithValue("@HotelID", hotelNo);
            command.Parameters.AddWithValue("@RoomID", roomNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public Room DeleteRoom(int roomNo, int hotelNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_deleteSql, connection);

            command.Parameters.AddWithValue("@HotelID", hotelNo);
            command.Parameters.AddWithValue("@RoomID", roomNo);

            var room = GetRoomFromRoomId(roomNo, hotelNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return room;

            return null;
        }
    }
}
