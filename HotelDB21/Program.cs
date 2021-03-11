using System;

namespace HotelDBConsole21
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var showMenu = true;

            while (showMenu)
            {
                showMenu = MainMenu.Menu();
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
