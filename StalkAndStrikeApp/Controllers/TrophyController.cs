using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Data;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Controllers
{
    public class TrophyController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public TrophyController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // Example of loading data from the database
            var trophies = _context.Trophies.ToList(); // Assuming Trophies is a DbSet<Trophy>
            return View(trophies); // Passing the list of trophies to the view
        }

        public IActionResult Create(Trophy trophy, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                // Get the path to save the image in the wwwroot/Content/Images folder
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Content", "Images");

                // Ensure the directory exists
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Combine the path and file name
                string filePath = Path.Combine(uploadDir, Path.GetFileName(photo.FileName));

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                // Save the relative path to the database
                trophy.PhotoPath = "/Content/Images/" + photo.FileName;
            }

            // Save trophy to the database if the model state is valid
            if (ModelState.IsValid)
            {
                _context.Trophies.Add(trophy);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(trophy);
        }
    }
}
