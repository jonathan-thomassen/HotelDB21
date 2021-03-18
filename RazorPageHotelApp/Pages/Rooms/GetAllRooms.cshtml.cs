using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty] public string SortCriteria { get; set; }
        [BindProperty] public bool SortAscending { get; set; } = true;
        public List<Room> Rooms { get; private set; }
        public Hotel Hotel { get; private set; }

        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public GetAllRoomsModel(IHotelService hService, IRoomService rService)
        {
            _roomService = rService;
            _hotelService = hService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = await _roomService.GetAllRooms();
            return Page();
        }

        public async Task<IActionResult> OnGetMyRoomsAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelId(id);
            return Page();
        }

        public async Task<IActionResult> OnPostSortAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelId(id);

            switch (SortCriteria)
            {
                case "Number":
                    Rooms = (from room in Rooms orderby room.RoomNo select room).ToList();
                    break;
                case "Type":
                    Rooms = (from room in Rooms orderby room.Types select room).ToList();
                    break;
                case "Price":
                    Rooms = (from room in Rooms orderby room.Price select room).ToList();
                    break;
            }

            if (!SortAscending)
                Rooms.Reverse();

            return Page();
        }
    }
}