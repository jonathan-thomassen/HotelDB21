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
    public class EditModel : PageModel
    {
        [BindProperty] public Hotel Hotel { get; set; }

        private readonly IHotelService _hotelService;

        public EditModel(IHotelService hService)
        {
            _hotelService = hService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _hotelService.UpdateHotel(Hotel, id);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}