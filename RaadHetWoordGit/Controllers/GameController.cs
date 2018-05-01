using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RaadHetWoordGit.Controllers
{
    public class GameController : Controller
    {
        private ChecksLogic _checksLogic;
        private TeamLogic _teamLogic;
        private TeamInGameLogic _teamInGameLogic;
        private GameLogic _gameLogic;
        private WordListLogic _wordListLogic;

        /// <summary>
        /// Initialize logic classes
        /// </summary>
        private void InitializeLogic()
        {
            _checksLogic = new ChecksLogic();
            _gameLogic = new GameLogic(new GameRepository(new GameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _teamInGameLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
        }

        /// <summary>
        /// Opening the Index page for the first time.
        /// </summary>
        public ActionResult Index()
        {
            InitializeLogic();
            HttpContext.Session.Clear();

            var viewModel = new GameViewModel();

            viewModel.WordlistClass = "hidden";
            viewModel.Wordlists = _wordListLogic.GetWordlists();

            return View(viewModel);
        }
        
        /// <summary>
        /// The Index page after teams and maxscore have been entered.
        /// </summary>
        [HttpPost]
        public IActionResult Index(GameViewModel viewModel)
        {
            InitializeLogic();

            if (!_checksLogic.ValuesAreValid(viewModel.TeamOne, viewModel.TeamTwo))
            {
                viewModel.WarningClass = "visible";
                viewModel.Wordlists = _wordListLogic.GetWordlists();
                ViewData["Warning"] = "Let op!";
                ViewData["ErrorText"] = "Namen zijn niet correct ingevoerd.";
                return View(viewModel);
            }

            var teams = new List<Team>
            {
                new Team(viewModel.TeamOne),
                new Team(viewModel.TeamTwo)
            };

            viewModel.Game = new Game(_checksLogic.MaxScore(viewModel.MaxScore), teams);
            viewModel.Game = _gameLogic.AddTeams(teams, viewModel.Game);
            viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords(viewModel.Wordlist)));

            _teamLogic.AddTeam(teams[0]);
            _teamLogic.AddTeam(teams[1]);

            PlaceViewModelInSession(viewModel);

            return RedirectToAction("ScoreBoard", "Game");
        }

        /// <summary>
        /// The page to play a game
        /// </summary>
        [HttpPost]
        public ActionResult PlayGame()
        {
            InitializeLogic();

            var viewModel = GetViewModelFromSession();

            if (_gameLogic.GameIsOver(viewModel.Game))
            {
                var winner = _gameLogic.GetWinner(viewModel.Game);
                viewModel.Winner = winner.Name;
                _teamLogic.IncreaseWins(_gameLogic.GetWinner(viewModel.Game));
                _teamLogic.IncreaseLosses(_gameLogic.GetLoser(viewModel.Game));
                PlaceViewModelInSession(viewModel);
                return RedirectToAction("Summary", "Game");
            }

            viewModel.Game.CurrentRound = new Round(viewModel.Game);

            try
            {
                viewModel.Game.TeamList[Round.playerindex - 1] = _teamInGameLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex - 1]);
                _teamLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex - 1]);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
                viewModel.Game.TeamList[Round.playerindex] = _teamInGameLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex]);
                _teamLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex]);
            }

            if (viewModel.Game.Wordlist.Words.Count < 10)
            {
                viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords(viewModel.Wordlist)));
                viewModel.WordlistClass = "visible";
            }

            _wordListLogic.RemoveWords(viewModel.Game.Wordlist.Words);

            PlaceViewModelInSession(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// View the scoreboard
        /// </summary>
        public ActionResult ScoreBoard()
        {
            return View(GetTeamsFromSession());
        }

        /// <summary>
        /// View the summary when the game is over
        /// </summary>
        public ActionResult Summary()
        {
            var viewModel = GetViewModelFromSession();
            HttpContext.Session.Clear();
            return View(viewModel);
        }

        /// <summary>
        /// Place gameviewmodel in session 
        /// </summary>
        /// <param name="inputViewModel">Viewmodel to place in session</param>
        private void PlaceViewModelInSession(GameViewModel inputViewModel)
        {
            var teamList = inputViewModel.Game.TeamList;
            var wordList = inputViewModel.Game.Wordlist.Words;
            var round = inputViewModel.Game.CurrentRound;
            
            HttpContext.Session.SetString(nameof(Round), JsonConvert.SerializeObject(round));
            HttpContext.Session.SetString("teamlist", JsonConvert.SerializeObject(teamList));
            HttpContext.Session.SetString(nameof(Wordlist), JsonConvert.SerializeObject(wordList));

            inputViewModel.Game.CurrentRound = null;
            inputViewModel.Game.TeamList = null;
            inputViewModel.Game.Wordlist = null;

            HttpContext.Session.SetString(nameof(GameViewModel), JsonConvert.SerializeObject(inputViewModel));

            inputViewModel.Game.TeamList = teamList;
            inputViewModel.Game.Wordlist = new Wordlist(wordList);
            inputViewModel.Game.CurrentRound = round;
        }

        /// <summary>
        /// Retrieve gameviewmodel from session
        /// </summary>        
        private GameViewModel GetViewModelFromSession()
        {
            var viewModel = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(nameof(GameViewModel)));
        
            viewModel.Game.TeamList = JsonConvert.DeserializeObject<List<Team>>(HttpContext.Session.GetString("teamlist"));
            viewModel.Game.Wordlist = new Wordlist(JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString(nameof(Wordlist))));
            viewModel.Game.CurrentRound = JsonConvert.DeserializeObject<Round>(HttpContext.Session.GetString(nameof(Round)));

            return viewModel;
        }

        /// <summary>
        /// Retrieve gameviewmodel from session for scoreboard
        /// </summary>      
        private GameViewModel GetTeamsFromSession()
        {
            return new GameViewModel{Game = new Game(-1, JsonConvert.DeserializeObject<List<Team>>(HttpContext.Session.GetString("teamlist")))};
        }
    }
}