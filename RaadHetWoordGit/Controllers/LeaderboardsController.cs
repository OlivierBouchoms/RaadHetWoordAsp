using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class LeaderboardsController : Controller
    {
        private TeamLogic _teamLogic;
        private TeamInGameLogic _teamInGameLogic;
        private GameLogic _gameLogic;
        private WordListLogic _wordListLogic;
       
        public IActionResult Index()
        {
            InitializeLogic();
            
            return View(new LeaderboardViewModel(_teamLogic.GetTeams(null)));
        }

        public IActionResult Teams(string orderby)
        {
            InitializeLogic();

            return PartialView("TeamsPartial", new LeaderboardViewModel(_teamLogic.GetTeams(orderby)));
        }

        /// <summary>
        /// Initialize logic classes
        /// </summary>
        private void InitializeLogic()
        {
            _gameLogic = new GameLogic(new GameRepository(new GameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _teamInGameLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
        }
    }
}