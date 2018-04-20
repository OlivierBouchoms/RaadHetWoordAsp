using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;

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
            
            return View();
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