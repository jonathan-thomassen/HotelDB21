using System;
using System.Collections.Generic;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string _queryString = "select * from Hotel";
        private string _queryStringFromId = "select * from Hotel where Hotel_No = @ID";
        private string _queryStringFromName = "select * from Hotel where Name like @Name";
        private string _insertSql = "insert into Hotel values (@ID, @Name, @Address)";
        private string _deleteSql = "delete from Hotel where Hotel_No = @ID";
        private string _updateSql = "update Hotel set Name = @Name, Address = @Address where Hotel_No = @ID";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings

        public List<Hotel> GetAllHotel()
        {
            var hotels = new List<Hotel>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryString, connection);

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var hotelNr = reader.GetInt32(0);
                    var hotelNavn = reader.GetString(1);
                    var hotelAdr = reader.GetString(2);
                    var hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                    hotels.Add(hotel);
                }

                connection.Close();
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

            return hotels.Count >= 1 ? hotels : null;
        }

        public Hotel GetHotelFromId(int hotelNo)
        {
            var hotel = new Hotel();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringFromId, connection);
            command.Parameters.AddWithValue("@ID", hotelNo);

            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var hotelName = reader.GetString(1);
                var hotelAddress = reader.GetString(2);
                hotel = new Hotel(hotelNo, hotelName, hotelAddress);
            }
            else
            {
                hotel = null;
            }
            connection.Close();

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_insertSql, connection);

            command.Parameters.AddWithValue("@ID", hotel.HotelNr);
            command.Parameters.AddWithValue("@Name", hotel.Name);
            command.Parameters.AddWithValue("@Address", hotel.Address);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public bool UpdateHotel(Hotel hotel, int hotelNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_updateSql, connection);

            command.Parameters.AddWithValue("@Name", hotel.Name);
            command.Parameters.AddWithValue("@Address", hotel.Address);
            command.Parameters.AddWithValue("@ID", hotelNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return true;

            return false;
        }

        public Hotel DeleteHotel(int hotelNo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_deleteSql, connection);

            command.Parameters.AddWithValue("@ID", hotelNo);

            var hotel = GetHotelFromId(hotelNo);

            connection.Open();
            var commandStatus = command.ExecuteNonQuery();
            connection.Close();

            if (commandStatus > 0)
                return hotel;

            return null;
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            var hotels = new List<Hotel>();

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(_queryStringFromName, connection);
            command.Parameters.AddWithValue("@Name", "%" + name + "%");

            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var hotelNr = reader.GetInt32(0);
                var hotelName = reader.GetString(1);
                var hotelAdr = reader.GetString(2);
                var hotel = new Hotel(hotelNr, hotelName, hotelAdr);
                hotels.Add(hotel);
            }
            connection.Close();

            if (hotels.Count >= 1)
                return hotels;

            return null;
        }
    }
}
