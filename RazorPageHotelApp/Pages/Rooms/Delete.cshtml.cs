using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        public Hotel Hotel { get; private set; }
        public Room Room { get; private set; }

        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public DeleteModel(IHotelService hService, IRoomService rService)
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

        public IActionResult OnPostCancel(int id)
        {
            return RedirectToPage("/Rooms/GetAllRoomsFromHotel", "SortByRoomNumberAsc", new { id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int[] id)
        {
            await _roomService.DeleteRoom(id[1], id[0]);
            return RedirectToPage("/Rooms/GetAllRoomsFromHotel", "SortByRoomNumberAsc", new { id = id[0] });
        }
    }
}