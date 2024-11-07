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
    public class TrophyController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        // Inject IWebHostEnvironment to access the web root path
        public TrophyController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
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
