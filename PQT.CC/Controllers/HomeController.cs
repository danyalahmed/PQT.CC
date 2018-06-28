using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQT.CC.Models;
using System.Diagnostics;

namespace PQT.CC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About() => View();

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? id)
        {
            return View(new ErrorViewModel { ErrorCode = id, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
