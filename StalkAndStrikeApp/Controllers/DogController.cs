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
    public class DogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dog
        public async Task<IActionResult> Index()
        {
            var dogs = _context.Dogs.Include(d => d.Hunter);
            return View(await dogs.ToListAsync());
        }

        // GET: Dog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dog = await _context.Dogs
                .Include(d => d.Hunter) // Load associated Hunter
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dog == null) return NotFound();

            return View(dog);
        }

        // GET: Dog/Create
        public IActionResult Create(int? hunterId)
        {
            ViewBag.HunterList = new SelectList(_context.Hunters, "Id", "Name", hunterId);
            return View();
        }

        // POST: Dog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Breed,HunterId")] Dog dog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HunterId"] = new SelectList(_context.Hunters, "Id", "Name", dog.HunterId);
            return View(dog);
        }

        // GET: Dog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dog = await _context.Dogs.FindAsync(id);
            if (dog == null) return NotFound();

            ViewData["HunterId"] = new SelectList(_context.Hunters, "Id", "Name", dog.HunterId); // List of hunters for selection
            return View(dog);
        }

        // POST: Dog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Breed,HunterId")] Dog dog)
        {
            if (id != dog.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogExists(dog.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HunterId"] = new SelectList(_context.Hunters, "Id", "Name", dog.HunterId);
            return View(dog);
        }

        // GET: Dog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dog = await _context.Dogs
                .Include(d => d.Hunter) // Load associated Hunter
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dog == null) return NotFound();

            return View(dog);
        }

        // POST: Dog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogExists(int id)
        {
            return _context.Dogs.Any(e => e.Id == id);
        }
    }
}
