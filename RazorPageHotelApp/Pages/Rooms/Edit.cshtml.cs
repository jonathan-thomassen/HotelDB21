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
    public class EditModel : PageModel
    {
        public Hotel Hotel { get; private set; }
        [BindProperty] public Room Room { get; set; }

        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public EditModel(IHotelService hService, IRoomService rService)
        {
            _hotelService = hService;
            _roomService = rService;
        }

        public async Task<IActionResult> OnGetAsync(int[] id)
        {
            Hotel = await _hotelService.GetHotelFromId(id[0]);
            Room = await _roomService.GetRoomFromRoomId(id[1], id[0]);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] id)
        {
            await _roomService.UpdateRoom(Room, id[1], id[0]);
            return RedirectToPage("/Rooms/GetAllRooms", "MyRooms", new { id = id[0] });
        }
    }
}
