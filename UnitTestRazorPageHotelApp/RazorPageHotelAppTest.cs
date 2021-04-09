using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotelApp.Models;
using RazorPageHotelApp.Services;

namespace UnitTestRazorPageHotelApp
{
    [TestClass]
    public class UnitTestHotel
    {

        // denne klasse tester HotelService

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB2021;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void TestAddHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(ConnectionString);
            List<Hotel> hotels = hotelService.GetAllHotels().Result;

            //Act
            int numbersOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(1001, "TestHotel", "Testvej");
            bool ok = hotelService.CreateHotel(newHotel).Result;
            hotels = hotelService.GetAllHotels().Result;

            int numbersOfHotelsAfter = hotels.Count;
            Hotel h = hotelService.DeleteHotel(newHotel.HotelNo).Result;

            //Assert
            Assert.AreEqual(numbersOfHotelsBefore + 1, numbersOfHotelsAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNo, newHotel.HotelNo);

        }
    }

}
