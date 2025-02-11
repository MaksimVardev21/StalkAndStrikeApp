using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Data;
using StalkAndStrikeApp.Models;
using Microsoft.Extensions.Logging;

namespace StalkAndStrikeApp.Controllers
{
    [Authorize]
    public class HunterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HunterController> _logger;

        public HunterController(ApplicationDbContext context, ILogger<HunterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new HunterManagementViewModel
            {
                Squads = _context.Squads.ToList(),
                Hunters = _context.Hunters.Include(h => h.Dogs).Include(h => h.Squad).ToList(),
                Dogs = _context.Dogs.Include(d => d.Hunter).ToList() // Ensure Dogs are included
            };

            _logger.LogInformation("Loaded Hunters: {@Hunters}", viewModel.Hunters);
            _logger.LogInformation("Loaded Dogs: {@Dogs}", viewModel.Dogs);

            return View(viewModel);
        }

        // GET: Hunter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hunter = await _context.Hunters.FindAsync(id);
            if (hunter == null) return NotFound();

            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name", hunter.SquadId);
            return View(hunter);
        }

        // POST: Hunter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LicenseNumber,SquadId")] Hunter hunter)
        {
            if (id != hunter.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hunter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HunterExists(hunter.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index");
            }

            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name", hunter.SquadId);
            return View(hunter);
        }

        private bool HunterExists(int id)
        {
            return _context.Hunters.Any(e => e.Id == id);
        }


        // GET: Hunter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hunter = await _context.Hunters
                .Include(h => h.Squad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hunter == null) return NotFound();

            return View(hunter);
        }

        // POST: Hunter/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hunter = await _context.Hunters.FindAsync(id);
            _context.Hunters.Remove(hunter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult CreateSquad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSquad(Squad squad)
        {
            if (ModelState.IsValid)
            {
                _context.Squads.Add(squad);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(squad);
        }

        public IActionResult CreateHunter()
        {
            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateHunter(Hunter hunter)
        {
            if (ModelState.IsValid)
            {
                _context.Hunters.Add(hunter);
                _context.SaveChanges();
                _logger.LogInformation("Hunter added: {@Hunter}", hunter);
                return RedirectToAction("Index");
            }

            _logger.LogWarning("Invalid model state for Hunter: {@ModelState}", ModelState);
            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name");
            return View(hunter);
        }

        public IActionResult CreateDog()
        {
            ViewBag.Hunters = new SelectList(_context.Hunters, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateDog(Dog dog)
        {
            if (ModelState.IsValid)
            {
                _context.Dogs.Add(dog);
                _context.SaveChanges();
                _logger.LogInformation("Dog added: {@Dog}", dog);
                return RedirectToAction("Index");
            }

            _logger.LogWarning("Invalid model state for Dog: {@ModelState}", ModelState);
            ViewBag.Hunters = new SelectList(_context.Hunters, "Id", "Name");
            return View(dog);
        }
    }
}