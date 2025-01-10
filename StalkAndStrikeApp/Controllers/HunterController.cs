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
                Dogs = _context.Dogs.ToList()
            };

            return View(viewModel);
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