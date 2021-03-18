using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }

        private readonly IHotelService _hotelService;

        public CreateModel(IHotelService hService)
        {
            _hotelService = hService;
        }

        public IActionResult OnGet()
        {
            Hotel = new Hotel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _hotelService.CreateHotel(Hotel);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}
