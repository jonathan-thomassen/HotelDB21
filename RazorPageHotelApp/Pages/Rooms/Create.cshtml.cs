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
    public class CreateModel : PageModel
    {
        public Hotel Hotel { get; private set; }
        [BindProperty] public Room Room { get; set; }

        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public CreateModel(IHotelService hService, IRoomService rService)
        {
            _hotelService = hService;
            _roomService = rService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Room = new Room();
            Room.HotelNo = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _roomService.CreateRoom(id, Room);
            return RedirectToPage("/Rooms/GetAllRooms", "MyRooms", new {id});
        }
    }
}
