using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class LeaderboardsController : Controller
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
            
            return View(new LeaderboardViewModel(_teamLogic.GetTeams("Score"), "Score"));
        }

        /// <summary>
        /// Partial view for leaderboard
        /// </summary>
        /// <param name="orderby">Order by score, wins or winloss</param>
        public IActionResult Teams(string orderby)
        {
            InitializeLogic();

            return PartialView("TeamsPartial", new LeaderboardViewModel(_teamLogic.GetTeams(orderby), orderby));
        }

        
    }
}