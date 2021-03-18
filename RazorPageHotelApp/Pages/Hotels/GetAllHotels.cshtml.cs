using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty] public string FilterCriteria { get; set; }
        [BindProperty] public string SortCriteria { get; set; }
        [BindProperty] public bool SortAscending { get; set; } = true;
        public List<Hotel> Hotels { get; private set; }

        private readonly IHotelService _hotelService;

        public GetAllHotelsModel(IHotelService hService)
        {
            _hotelService = hService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Hotels = await _hotelService.GetAllHotels();
            return Page();
        }

        public async Task<IActionResult> OnPostFilterSortAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);

            switch (SortCriteria)
            {
                case "Number":
                    Hotels = (from hotel in Hotels orderby hotel.HotelNo select hotel).ToList();
                    break;
                case "Name":
                    Hotels = (from hotel in Hotels orderby hotel.Name select hotel).ToList();
                    break;
                case "Address":
                    Hotels = (from hotel in Hotels orderby hotel.Address select hotel).ToList();
                    break;
            }

            if (!SortAscending)
                Hotels.Reverse();

            return Page();
        }
    }
}