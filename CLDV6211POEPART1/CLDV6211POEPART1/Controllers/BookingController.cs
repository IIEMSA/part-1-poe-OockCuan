using CLDV6211POEPART1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211POEPART1.Controllers
{
    public class BookingController : Controller
    {
        private readonly POEDBcontext _context;
        public BookingController(POEDBcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            var booking = await _context.Booking.ToListAsync();
            return View(booking);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);

        }

        public async Task<IActionResult> Details(int? BookingID)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingID == BookingID);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        //Actions taken when interacting with edit

        [HttpPost]


        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        //Actions taken when interacting with delete
        public async Task<IActionResult> Delete(int? id)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
