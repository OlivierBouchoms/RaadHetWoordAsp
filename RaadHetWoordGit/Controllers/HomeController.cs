using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamLogic _teamLogic;

        /// <summary>
        /// Initialize logic classes
        /// </summary>
        public HomeController()
        {
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
        }

        /// <summary>
        /// Index page
        /// </summary>
        public ViewResult Index()
        {
            HttpContext.Session.Clear();
            return View(new LeaderboardViewModel(_teamLogic.GetTopTeams(), "Score"));
        }

        /// <summary>
        /// About page
        /// </summary>
        /// <returns></returns>
        public ViewResult About()
        {
            return View();
        }

        /// <summary>
        /// Error page
        /// </summary>
        /// <returns></returns
        [Route("/Error")]
        public ViewResult Error()
        {
            HttpContext.Session.Clear();
            return View();
        }

    }
}

