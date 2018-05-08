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
            var teams = _teamLogic.GetTeams("Score");
            teams.RemoveRange(10, teams.Count - 10);
            return View(new LeaderboardViewModel(teams, "Score"));
        }
    }
}

