using _8LMCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace _8LMBackend.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            using (var context = new dashboard_developmentContext())
            {
                var users = context.Campaign.ToListAsync().Result;

            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
