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
    public class HuntsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HuntsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hunts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hunt.ToListAsync());
        }

        // GET: Hunts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hunt = await _context.Hunt
                .FirstOrDefaultAsync(m => m.HuntId == id);
            if (hunt == null)
            {
                return NotFound();
            }

            return View(hunt);
        }

        // GET: Hunts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hunts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HuntId,UserId,Location,Date,GameTracked")] Hunt hunt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hunt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hunt);
        }

        // GET: Hunts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hunt = await _context.Hunt.FindAsync(id);
            if (hunt == null)
            {
                return NotFound();
            }
            return View(hunt);
        }

        // POST: Hunts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HuntId,UserId,Location,Date,GameTracked")] Hunt hunt)
        {
            if (id != hunt.HuntId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hunt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuntExists(hunt.HuntId))
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
            return View(hunt);
        }

        // GET: Hunts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hunt = await _context.Hunt
                .FirstOrDefaultAsync(m => m.HuntId == id);
            if (hunt == null)
            {
                return NotFound();
            }

            return View(hunt);
        }

        // POST: Hunts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hunt = await _context.Hunt.FindAsync(id);
            if (hunt != null)
            {
                _context.Hunt.Remove(hunt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuntExists(int id)
        {
            return _context.Hunt.Any(e => e.HuntId == id);
        }
    }
}
