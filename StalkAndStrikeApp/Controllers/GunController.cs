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
    public class GunController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GunController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gun
        public async Task<IActionResult> Index()
        {
            // Fetch Guns and include Category
            var guns = await _context.Gun.Include(g => g.Category).ToListAsync();
            return View(guns);
        }
    }
}
