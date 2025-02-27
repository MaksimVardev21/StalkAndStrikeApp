using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Data;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Controllers
{
  public class HuntedPlaceController : Controller
  {
    private readonly ApplicationDbContext _context;

    public HuntedPlaceController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: HuntedPlace/Index
    public async Task<IActionResult> Index()
    {
      var huntedPlaces = await _context.HuntedPlace.ToListAsync();
      return View(huntedPlaces);
    }

    // GET: HuntedPlace/Add
    public IActionResult Add()
    {
      return View();
    }

    // POST: HuntedPlace/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(HuntedPlaceViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // Save the image
      string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
      string filePath = Path.Combine(uploadsFolder, uniqueFileName);

      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
        await model.ImageFile.CopyToAsync(fileStream);
      }

      // Save data to database
      var huntedPlace = new HuntedPlace
      {
        ImagePath = "/uploads/" + uniqueFileName,
        Latitude = model.Latitude,
        Longitude = model.Longitude
      };

      _context.HuntedPlace.Add(huntedPlace);
      await _context.SaveChangesAsync();

      return RedirectToAction("Index");
    }

    // GET: HuntedPlace/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      var huntedPlace = await _context.HuntedPlace.FindAsync(id);
      if (huntedPlace == null)
      {
        return NotFound();
      }

      _context.HuntedPlace.Remove(huntedPlace);
      await _context.SaveChangesAsync();

      return RedirectToAction("Index");
    }
  }
}
