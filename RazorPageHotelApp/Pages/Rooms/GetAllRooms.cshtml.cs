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
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty] public SortChoices SortChoice { get; set; } = SortChoices.RoomNumberAsc;
        [BindProperty] public char TypeFilter { get; set; }
        public List<Room> Rooms { get; private set; }
        public List<Hotel> Hotels { get; private set; }
        //public List<> RoomsWithHotelNames { get; private set; }

        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public GetAllRoomsModel(IRoomService rService, IHotelService hService)
        {
            _roomService = rService;
            _hotelService = hService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = await _roomService.GetAllRooms();
            Hotels = await _hotelService.GetAllHotels();
            return Page();
        }

        public async Task<IActionResult> OnPostSortByRoomNumberAscAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            Rooms = (from room in Rooms orderby room.RoomNo select room).ToList();
            SortChoice = SortChoices.RoomNumberAsc;

            return Page();
        }
        public async Task<IActionResult> OnPostSortByRoomNumberDesAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            Rooms = (from room in Rooms orderby room.RoomNo descending select room).ToList();
            SortChoice = SortChoices.RoomNumberDes;

            return Page();
        }
        public async Task<IActionResult> OnPostSortByHotelNumberAscAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            Rooms = (from room in Rooms orderby room.HotelNo select room).ToList();
            SortChoice = SortChoices.HotelNumberAsc;

            return Page();
        }
        public async Task<IActionResult> OnPostSortByHotelNumberDesAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            Rooms = (from room in Rooms orderby room.HotelNo descending select room).ToList();
            SortChoice = SortChoices.HotelNumberDes;

            return Page();
        }
        public async Task<IActionResult> OnPostSortByHotelNameAscAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            Rooms = (from room in Rooms orderby room.HotelNo descending select room).ToList();
            SortChoice = SortChoices.HotelNumberDes;

            return Page();
        }
        public async Task<IActionResult> OnPostSortByHotelNameDesAsync()
        {
            Rooms = await _roomService.GetAllRoomsFilterByType(TypeFilter);
            var roomsNew = (from room1 in Rooms
                            select new
                            {
                                room1.HotelNo,
                                HotelName = (from room2 in Rooms
                                             join hotel in _hotelService.GetAllHotels().Result on room2.HotelNo equals hotel.HotelNo
                                             select hotel.Name),

                            }).ToList();
            SortChoice = SortChoices.HotelNumberDes;

            return Page();
        }
    }
}