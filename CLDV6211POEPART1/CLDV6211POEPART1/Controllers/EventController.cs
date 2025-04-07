using CLDV6211POEPART1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CLDV6211POEPART1.Controllers
{
    public class EventController : Controller
    {
        private readonly POEDBcontext _context;

        public EventController(POEDBcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
            
        {
            var evente = await _context.Event.ToListAsync();
            return View(evente);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //Actions taken when interacting with create
        public async Task<IActionResult> Create(Event evente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evente);

        }
//Actions taken when interacting with details
        public async Task<IActionResult> Details(int? EventID){
            var evente = await _context.Event.FirstOrDefaultAsync(m => m.EventID == EventID);
            if (evente == null){
                return NotFound();}
            return View(evente);
        }
        private bool EventExists(int id) {
            return _context.Event.Any(e => e.EventID == id);
        }
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }
            var evente = await _context.Event.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(evente);
        }
        //Actions taken when interacting with edit

        [HttpPost]
        

        public async Task<IActionResult> Edit(int id, Event evente){
            if (id != evente.EventID){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(evente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!EventExists(evente.EventID)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(evente);
        }

        //Actions taken when interacting with delete
        public async Task<IActionResult> Delete(int? id)
        {
            var evente = await _context.Event.FirstOrDefaultAsync(m => m.EventID == id);
            if (evente == null){
                return NotFound();
            }
            return View(evente);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id){
            var evente = await _context.Event.FindAsync(id);
            _context.Event.Remove(evente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
