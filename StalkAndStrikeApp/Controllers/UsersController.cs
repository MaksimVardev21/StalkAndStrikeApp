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
    public class UsersController : Controller
    {
        // Fake in-memory storage for simplicity
        private static List<User> users = new List<User>();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Invalid credentials.";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            users.Add(user);
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            
            return RedirectToAction("Login");
        }
    }

}
