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

        public IActionResult Index()
        {
            var viewModel = new HunterManagementViewModel
            {
                Squads = _context.Squads.ToList(),
                Hunters = _context.Hunters.Include(h => h.Dogs).Include(h => h.Squad).ToList(),
                Dogs = _context.Dogs.ToList()
            };

            return View(viewModel);
        }


        public IActionResult CreateHunter()  // This is the GET method
        {
            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name");
            return View();
        }

        [HttpPost]  // Ensure this method handles POST requests
        public IActionResult CreateHunter(Hunter hunter)
        {
            if (ModelState.IsValid)
            {
                _context.Hunters.Add(hunter);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Squads = new SelectList(_context.Squads, "Id", "Name");
            return View(hunter);
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
                return RedirectToAction("Index");
            }

            ViewBag.Hunters = new SelectList(_context.Hunters, "Id", "Name");
            return View(dog);
        }
    }
}