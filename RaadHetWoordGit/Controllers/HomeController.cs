using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

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
