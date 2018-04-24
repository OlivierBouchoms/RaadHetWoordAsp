using System;
using System.Threading.Tasks;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace RaadHetWoordGit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}

