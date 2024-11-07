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
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        // GET: Schedule
        public ActionResult Index()
        {
            var schedules = _context.Schedules.Include("Location").ToList();
            return View(schedules);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_context.HuntingLocations, "Id", "Name");
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(_context.HuntingLocations, "Id", "Name", schedule.LocationId);
            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int id)
        {
            var schedule = _context.Schedules.Include("Location").FirstOrDefault(s => s.Id == id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var schedule = _context.Schedules.Find(id);
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
