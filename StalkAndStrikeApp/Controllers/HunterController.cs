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
    public class HunterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HunterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hunter
        public async Task<IActionResult> Index()
        {
            var hunters = _context.Hunters.Include(h => h.Squad);
            return View(await hunters.ToListAsync());
        }

        // GET: Hunter/Create
        public IActionResult Create(int? squadId)
        {
            ViewBag.SquadList = new SelectList(_context.Squads, "Id", "Name", squadId);
            return View();
        }

        // POST: Hunter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LicenseNumber,SquadId")] Hunter hunter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hunter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SquadId"] = new SelectList(_context.Squads, "Id", "Name", hunter.SquadId);
            return View(hunter);
        }
    }
}
