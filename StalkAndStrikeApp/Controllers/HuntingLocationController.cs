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
    public class HuntingLocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HuntingLocationController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            // Check for potential nulls
            var locations = _context.HuntingLocations.ToList();
            return View(locations);
        }

        // GET: HuntingLocation/Details/5
        public ActionResult Details(int id)
        {
            var location = _context.HuntingLocations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // GET: HuntingLocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HuntingLocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HuntingLocation location)
        {
            if (ModelState.IsValid)
            {
                _context.HuntingLocations.Add(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: HuntingLocation/Edit/5
        public ActionResult Edit(int id)
        {
            var location = _context.HuntingLocations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // POST: HuntingLocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HuntingLocation location)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(location).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: HuntingLocation/Delete/5
        public ActionResult Delete(int id)
        {
            var location = _context.HuntingLocations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // POST: HuntingLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var location = _context.HuntingLocations.Find(id);
            _context.HuntingLocations.Remove(location);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}