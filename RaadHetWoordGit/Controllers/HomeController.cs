using System;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

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
                ExceptionSqLiteContext.LogException(e);
            }
            return View();
        }
    }
}
