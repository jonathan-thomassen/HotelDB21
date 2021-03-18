using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Guests
{
    public class GetAllGuestsModel : PageModel
    {
        [BindProperty] public string FilterCriteria { get; set; }
        [BindProperty] public string SortCriteria { get; set; }
        [BindProperty] public bool SortAscending { get; set; } = true;
        public List<Guest> Guests { get; private set; }

        private readonly IGuestService _guestService;

        public GetAllGuestsModel(IGuestService gService)
        {
            _guestService = gService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guests = await _guestService.GetAllGuests();
            return Page();
        }

        public async Task<IActionResult> OnPostFilterSortAsync()
        {
            Guests = await _guestService.GetGuestsByName(FilterCriteria);

            switch (SortCriteria)
            {
                case "Number":
                    Guests = (from guest in Guests orderby guest.GuestNo select guest).ToList();
                    break;
                case "Name":
                    Guests = (from guest in Guests orderby guest.Name select guest).ToList();
                    break;
                case "Address":
                    Guests = (from guest in Guests orderby guest.Address select guest).ToList();
                    break;
            }

            if (!SortAscending)
                Guests.Reverse();

            return Page();
        }
    }
}