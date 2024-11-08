using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StalkAndStrikeApp.Controllers
{
    public class ManageControllercs : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
    }
}
