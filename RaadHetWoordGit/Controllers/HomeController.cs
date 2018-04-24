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
            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                new ExceptionSqLiteContext().LogException(e);
            }
            return View();
        }
    }
}

