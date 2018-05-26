using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class LeaderboardsController : Controller
    {
        private readonly TeamLogic _teamLogic;
        
        /// <summary>
        /// Initialize logic classes
        /// </summary>
        public LeaderboardsController()
        {
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMssqlContext()));
        }

        /// <summary>
        /// Index page
        /// </summary>
        public ViewResult Index()
        {
            return View(new LeaderboardViewModel(_teamLogic.GetTeams("Score"), "Score"));
        }

        /// <summary>
        /// Partial view for leaderboard
        /// </summary>
        /// <param name="orderby">Order by score, wins or winloss</param>
        public PartialViewResult Teams(string orderby)
        {
            return PartialView("TeamsPartial", new LeaderboardViewModel(_teamLogic.GetTeams(orderby), orderby));
        }
    }
}