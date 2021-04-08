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
    public class GetAllRoomsFromHotelModel : PageModel
    {
        [BindProperty] public SortChoices SortChoice { get; set; } = SortChoices.RoomNumberAsc;
        [BindProperty] public char TypeFilter { get; set; }
        public List<Room> Rooms { get; private set; }
        public Hotel Hotel { get; private set; }

        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public GetAllRoomsFromHotelModel(IHotelService hService, IRoomService rService)
        {
            _roomService = rService;
            _hotelService = hService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelId(id);
            return Page();
        }

        public async Task<IActionResult> OnPostSortByRoomNumberAscAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.RoomNo select room).ToList();
            SortChoice = SortChoices.RoomNumberAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByRoomNumberDesAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.RoomNo descending select room).ToList();
            SortChoice = SortChoices.RoomNumberDes;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByTypeAscAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.Types select room).ToList();
            SortChoice = SortChoices.TypeAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByTypeDesAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.Types descending select room).ToList();
            SortChoice = SortChoices.TypeDes;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByPriceAscAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.Price select room).ToList();
            SortChoice = SortChoices.PriceAsc;

            return Page();
        }

        public async Task<IActionResult> OnPostSortByPriceDesAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromId(id);
            Rooms = await _roomService.GetAllRoomsFromHotelIdFilterByType(id, TypeFilter);
            Rooms = (from room in Rooms orderby room.Price descending select room).ToList();
            SortChoice = SortChoices.PriceDes;

            return Page();
        }
    }
}