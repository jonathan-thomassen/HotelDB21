using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class HotelService : Connection, IHotelService
    {
        private const string QueryString = "select * from Hotel";
        private const string QueryStringFromId = "select * from Hotel where Hotel_No = @ID";
        private const string QueryStringFromName = "select * from Hotel where Name like @Name";
        private const string InsertSql = "insert into Hotel values (@Name, @Address)";
        private const string DeleteSql = "delete from Hotel where Hotel_No = @ID";
        private const string UpdateSql = "update Hotel set Name = @Name, Address = @Address where Hotel_No = @ID";

        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotels = new List<Hotel>();

            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(QueryString, connection);

            try
            {
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var hotelNo = reader.GetInt32(0);
                    var hotelName = reader.GetString(1);
                    var hotelAddress = reader.GetString(2);
                    var hotel = new Hotel(hotelNo, hotelName, hotelAddress);
                    hotels.Add(hotel);
                }
                await connection.CloseAsync();
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

        public async Task<Hotel> GetHotelFromId(int hotelNo)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(QueryStringFromId, connection);

            try
            {
                command.Parameters.AddWithValue("@ID", hotelNo);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                Hotel hotel;
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
                await connection.CloseAsync();

                return hotel;
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

        public async Task<bool> CreateHotel(Hotel hotel)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(InsertSql, connection);

            try
            {
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public async Task<bool> UpdateHotel(Hotel hotel, int hotelNo)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(UpdateSql, connection);

            try
            {
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);
                command.Parameters.AddWithValue("@ID", hotelNo);

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

        public async Task<Hotel> DeleteHotel(int hotelNo)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(DeleteSql, connection);

            try
            {
                command.Parameters.AddWithValue("@ID", hotelNo);

                var hotel = GetHotelFromId(hotelNo).Result;

                await connection.OpenAsync();
                var commandStatus = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                return commandStatus > 0 ? hotel : null;
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

        public async Task<List<Hotel>> GetHotelsByName(string name)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand(QueryStringFromName, connection);

            try
            {
                var hotels = new List<Hotel>();

                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var hotelNo = reader.GetInt32(0);
                    var hotelName = reader.GetString(1);
                    var hotelAddress = reader.GetString(2);
                    var hotel = new Hotel(hotelNo, hotelName, hotelAddress);
                    hotels.Add(hotel);
                }

                await connection.CloseAsync();

                return hotels.Count >= 1 ? hotels : null;
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

        public HotelService(IConfiguration configuration) : base(configuration)
        {
        }

        public HotelService(string connectionString) : base(connectionString)
        {
        }
    }
}