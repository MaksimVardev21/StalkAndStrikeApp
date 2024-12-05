using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Data;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Controllers
{
    public class SquadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SquadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Squad
        public async Task<IActionResult> Index()
        {
            return View(await _context.Squads.ToListAsync());
        }

        // GET: Squad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var squad = await _context.Squads
                .Include(s => s.Hunters) // Load hunters in the squad
                .FirstOrDefaultAsync(m => m.Id == id);

            if (squad == null) return NotFound();

            return View(squad);
        }

        // GET: Squad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Squad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Squad squad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(squad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(squad);
        }

        // GET: Squad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var squad = await _context.Squads.FindAsync(id);
            if (squad == null) return NotFound();

            return View(squad);
        }

        // POST: Squad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Squad squad)
        {
            if (id != squad.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(squad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquadExists(squad.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(squad);
        }

        // GET: Squad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var squad = await _context.Squads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (squad == null) return NotFound();

            return View(squad);
        }

        // POST: Squad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var squad = await _context.Squads.FindAsync(id);
            _context.Squads.Remove(squad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SquadExists(int id)
        {
            return _context.Squads.Any(e => e.Id == id);
        }

        // Example in HunterController
        [HttpPost]
        public JsonResult AddHunter(Hunter hunter)
        {
            if (ModelState.IsValid)
            {
                _context.Hunters.Add(hunter);
                _context.SaveChanges();
                return Json(hunter); // Return the newly added hunter as JSON
            }
            return Json(null);
        }
    }
}
