using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerchedPeacockWebApplication.Data;
using PerchedPeacockWebApplication.Models;

namespace PerchedPeacockWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking(bool includeOpenBookings=true)
        {
            return await _context.Booking.ToListAsync();
        }

        [HttpGet]
        [Route("FreeParkingSlots")]
        public ActionResult<IEnumerable<ParkingLot>> GetFreeParkingSlots(Location location)
        {
            var allFreeSlots = GetAllFreeParkingSlots();
            var allFreeSlotsForALocation = new List<ParkingLot>();

            foreach(ParkingLot parkingLot in allFreeSlots)
            {
                if(parkingLot.LocationBuilding == location.LocationBuilding && parkingLot.LocationLocality == location.LocationLocality)
                    {
                    allFreeSlotsForALocation.Add(parkingLot);
                        }
                }
           
        return allFreeSlotsForALocation;

    }
        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //[Route("api/[controller]/updateBooking")]
        //public async Task<IActionResult> PutBooking(int id, Booking booking)
        //{
        //    if (id != booking.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(booking).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Bookings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(int parkingLotId)
        {
            Booking booking = new Booking();
            int locationId = FindLocationIdFromParkingLotId(parkingLotId);
            booking.LocationId = locationId;
            booking.InTime = DateTime.Now;
            booking.IsOccupied = true;
            booking.ParkingLotId = parkingLotId;
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        private int FindLocationIdFromParkingLotId(int parkingLotId)
        {
          var parkingLot=  _context.ParkingLot.Single(parkingLot => parkingLot.Id == parkingLotId);
          var location = _context.Location.Single(location => location.LocationBuilding == parkingLot.LocationBuilding && location.LocationLocality == parkingLot.LocationLocality);
            return location.Id;
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        [HttpGet]
        [Route("AllFreeParkingSlots")]
        public List<ParkingLot> GetAllFreeParkingSlots()
        {
            var allBookings = GetBooking();
            var freeSlots = new List<ParkingLot>();
            var allbookingLots = _context.ParkingLot.ToList();
            foreach (ParkingLot parkingLot in allbookingLots)
            {
                bool bookingFound = false;
                foreach (Booking booking in allBookings.Result.Value)
                {
                    if (booking.ParkingLotId == parkingLot.Id)
                    {
                        bookingFound = true;
                        break;
                    }

                }
                if (!bookingFound )
                {
                    freeSlots.Add(parkingLot);
                }
            }
            return freeSlots;
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }

        [HttpPut]
        [Route("CloseBooking/{id}")]
        public void CloseBooking(int bookingId)
        {
          var booking=  _context.Booking.Single(booking => booking.Id == bookingId);
            booking.OutTime = DateTime.Now;
            booking.IsOccupied = false;
        }
    }
}
