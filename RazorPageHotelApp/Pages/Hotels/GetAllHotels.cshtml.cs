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
        [BindProperty] public SortChoices SortChoice { get; set; } = SortChoices.NumberAsc;
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

        public async Task<IActionResult> OnPostSortByNumberAscAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.HotelNo select hotel).ToList();
            SortChoice = SortChoices.NumberAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByNumberDesAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.HotelNo descending select hotel).ToList();
            SortChoice = SortChoices.NumberDes;
            
            return Page();
        }

        public async Task<IActionResult> OnPostSortByNameAscAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.Name select hotel).ToList();
            SortChoice = SortChoices.NameAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByNameDesAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.Name descending select hotel).ToList();
            SortChoice = SortChoices.NameDes;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByAddressAscAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.Address select hotel).ToList();
            SortChoice = SortChoices.AddressAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByAddressDesAsync()
        {
            Hotels = await _hotelService.GetHotelsByName(FilterCriteria);
            Hotels = (from hotel in Hotels orderby hotel.Address descending select hotel).ToList();
            SortChoice = SortChoices.AddressDes;

            return Page();
        }
    }
}