using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class HomeController : Controller
    {
        private TeamLogic _teamLogic;

        /// <summary>
        /// Initialize logic classes
        /// </summary>
        private void InitializeLogic()
        {
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
        }

        /// <summary>
        /// Index page
        /// </summary>
        public IActionResult Index()
        {
            InitializeLogic();
            HttpContext.Session.Clear();
            return View(new LeaderboardViewModel(_teamLogic.GetTeams("Score"), "Score"));
        }

        
    }
}

