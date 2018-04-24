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
            ThrowException();
            return View();
        }

        private void ThrowException()
        {
            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
        }
    }
}
