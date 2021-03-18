using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class DeleteModel : PageModel
    {
        public Hotel Hotel { get; private set; }
        public int NoOfRooms { get; private set; }

        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        
        public DeleteModel(IHotelService hService, IRoomService rService)
        {
            _hotelService = hService;
            _roomService = rService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            NoOfRooms = _roomService.GetAllRoomsFromHotelId(id).Result.Count;
            return Page();
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Hotels/GetAllHotels");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _hotelService.DeleteHotel(id);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}
