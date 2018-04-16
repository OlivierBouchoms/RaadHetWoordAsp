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
        private TeamLogic _teamLogic;
        private GameLogic _gameLogic;
        private WordListLogic _wordListLogic;

        /// <summary>
        /// Opening the Index page for the first time.
        /// </summary>
        public ActionResult Index()
        {
            var viewModel = new GameViewModel();
            viewModel.TeamColumnClass = "hidden";
            viewModel.TeamFormClass = "visible";
            viewModel.WordlistClass = "hidden";

            return View(viewModel);
        }
        
        /// <summary>
        /// The Index page after teams and maxscore have been entered.
        /// </summary>
        [HttpPost]
        public ActionResult Index(GameViewModel viewModel)
        {
            if (!ValuesAreValid(viewModel))
            {
                viewModel.TeamOneSuccess = false;
                viewModel.TeamTwoSuccess = false;
                viewModel.TeamColumnClass = "hidden";
                viewModel.TeamFormClass = "visible";
                return View(viewModel);
            }

            InitializeLogic();

            List<Team> teams = new List<Team>(2)
            {
                new Team(viewModel.TeamOne),
                new Team(viewModel.TeamTwo)
            };

            viewModel.Game = new Game(MaxScore(viewModel.MaxScore), teams);
            viewModel.Game = _gameLogic.AddTeams(teams, viewModel.Game);

            viewModel.TeamOneSuccess = _teamLogic.AddTeam(teams[0]);
            viewModel.TeamTwoSuccess = _teamLogic.AddTeam(teams[1]);

            viewModel.TeamFormClass = "hidden";
            viewModel.TeamColumnClass = "visible";

            var vm = new GameViewModel();
            vm = viewModel;

            PlaceViewModelInSession(vm, false);

            return View(viewModel);
        }

        /// <summary>
        /// Initialize logic classes
        /// </summary>
        private void InitializeLogic()
        {
            _gameLogic = new GameLogic(new GameRepository(new GameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
        }

        /// <summary>
        /// The page to play a game
        /// </summary>
        [HttpPost]
        public ActionResult PlayGame()
        {
            InitializeLogic();

            var viewModel = GetViewModelFromSession(false);

//            List<Team> teams = new List<Team>(2)
//            {
//                new Team(viewModel.TeamOne),
//                new Team(viewModel.TeamTwo)
//            };
//
//            viewModel.Game = _gameLogic.AddTeams(teams, viewModel.Game); 

            viewModel.Game.CurrentRound = new Round(viewModel.Game);
            viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords()));

            viewModel.WordlistClass = "visible";

            //Viewmodel in sessie plaatsen
            PlaceViewModelInSession(viewModel, true);
            HttpContext.Session.SetString(nameof(GameViewModel), JsonConvert.SerializeObject(viewModel));

            return View(viewModel);
        }

        /// <summary>
        /// Place gameviewmodel in session 
        /// </summary>
        private void PlaceViewModelInSession(GameViewModel inputViewModel, bool _round)
        {
            var teamList = inputViewModel.Game.TeamList;
            var wordList = inputViewModel.Game.Wordlist;
            if (_round)
            {
                HttpContext.Session.SetString(key: nameof(Round), value: JsonConvert.SerializeObject(inputViewModel.Game.CurrentRound));
                inputViewModel.Game.CurrentRound = null;
            }
            HttpContext.Session.SetString(key: "TeamList", value: JsonConvert.SerializeObject(inputViewModel.Game.TeamList));
            HttpContext.Session.SetString(key: nameof(Wordlist), value: JsonConvert.SerializeObject(inputViewModel.Game.Wordlist));

            inputViewModel.Game.TeamList = null;
            inputViewModel.Game.Wordlist = null;

            HttpContext.Session.SetString(key: nameof(GameViewModel), value: JsonConvert.SerializeObject(inputViewModel));

            inputViewModel.Game.TeamList = teamList;
            inputViewModel.Game.Wordlist = wordList;
        }

        /// <summary>
        /// Retrieve gameviewmodel from session
        /// </summary>
        /// <returns></returns>
        private GameViewModel GetViewModelFromSession(bool _round)
        {
            var teamList = new List<Team>(2);
            var wordList = new Wordlist();
            var viewModel = new GameViewModel();
            teamList = JsonConvert.DeserializeObject<List<Team>>(HttpContext.Session.GetString("TeamList"));
            wordList = JsonConvert.DeserializeObject<Wordlist>(HttpContext.Session.GetString(nameof(Wordlist)));

            viewModel = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(nameof(GameViewModel)));
            viewModel.Game.TeamList = teamList;
            viewModel.Game.Wordlist = wordList;

            if (_round)
            {
                var round = new Round();
                round = JsonConvert.DeserializeObject<Round>(HttpContext.Session.GetString(nameof(Round)));
                viewModel.Game.CurrentRound = round;
            }

            return viewModel;
        }

        /// <summary>
        /// Trim the strings and check for duplicates/empty values
        /// </summary>
        private bool ValuesAreValid(GameViewModel viewModel)
        {
            viewModel = TrimStrings(viewModel);
            if (ValuesAreNull(viewModel))
            {
                return false;
            }

            if (String.Equals(viewModel.TeamOne.ToLower(), viewModel.TeamTwo.ToLower()))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check for empty/null values
        /// </summary>
        private bool ValuesAreNull(GameViewModel viewModel)
        {
            if (String.IsNullOrWhiteSpace(viewModel.TeamOne) || String.IsNullOrWhiteSpace(viewModel.TeamTwo))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove whitespace from strings
        /// </summary>
        private GameViewModel TrimStrings(GameViewModel viewModel)
        {
            viewModel.TeamOne = viewModel.TeamOne.Trim();
            viewModel.TeamTwo = viewModel.TeamTwo.Trim();
            return viewModel;
        }

        /// <summary>
        /// Validates the MaxScore int to prevent a too high, too low or null value.
        /// </summary>
        private int MaxScore(int input)
        {
            if (input < 5)
            {
                return 5;
            }
            
            if (input > 100)
            {
                return 100;
            }
            return input;
        }

    }
}