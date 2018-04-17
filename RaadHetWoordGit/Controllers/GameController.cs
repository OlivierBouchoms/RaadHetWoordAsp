﻿using System;
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
        private TeamInGameLogic _teamInGameLogic;
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
                viewModel.WarningClass = "visible";
                ViewData["Warning"] = "Let op!";
                ViewData["ErrorText"] = "Namen zijn niet correct ingevoerd.";
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
            viewModel.WarningClass = "hidden";

            PlaceViewModelInSession(viewModel, false);

            return View(viewModel);
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

        /// <summary>
        /// The page to play a game
        /// </summary>
        [HttpPost]
        public ActionResult PlayGame()
        {
            InitializeLogic();

            var viewModel = GetViewModelFromSession(false);

            viewModel.Game.CurrentRound = new Round(viewModel.Game);
            viewModel.Game.TeamList[Round.playerindex - 1] = _teamInGameLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex - 1]);

            _teamLogic.IncreaseTurns(viewModel.Game.TeamList[Round.playerindex - 1]);

            try
            {
                //Throw an exception if Wordlist is empty
                if (viewModel.Game.Wordlist.Words != null);
            }
            catch
            {
                viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords()));
                viewModel.WordlistClass = "visible";
            }

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
            var round = new Round();
            if (_round)
            {
                round = inputViewModel.Game.CurrentRound;
                HttpContext.Session.SetString(nameof(Round), JsonConvert.SerializeObject(round));
                inputViewModel.Game.CurrentRound = null;
            }
            HttpContext.Session.SetString("TeamList", JsonConvert.SerializeObject(teamList));
            HttpContext.Session.SetString(nameof(Wordlist), JsonConvert.SerializeObject(wordList));

            inputViewModel.Game.TeamList = null;
            inputViewModel.Game.Wordlist = null;

            HttpContext.Session.SetString(nameof(GameViewModel), JsonConvert.SerializeObject(inputViewModel));

            inputViewModel.Game.TeamList = teamList;
            inputViewModel.Game.Wordlist = wordList;
            if (_round)
            {
                inputViewModel.Game.CurrentRound = round;
            }
        }

        /// <summary>
        /// Retrieve gameviewmodel from session
        /// </summary>
        /// <returns></returns>
        private GameViewModel GetViewModelFromSession(bool _round)
        {
            var teamList = JsonConvert.DeserializeObject<List<Team>>(HttpContext.Session.GetString("TeamList"));
            var wordList = JsonConvert.DeserializeObject<Wordlist>(HttpContext.Session.GetString(nameof(Wordlist)));
            var viewModel = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(nameof(GameViewModel)));

            viewModel.Game.TeamList = teamList;
            viewModel.Game.Wordlist = wordList;

            if (_round)
            {
                var round = JsonConvert.DeserializeObject<Round>(HttpContext.Session.GetString(nameof(Round)));
                viewModel.Game.CurrentRound = round;
            }

            return viewModel;
        }

        /// <summary>
        /// Trim the strings and check for duplicates/empty values
        /// </summary>
        private bool ValuesAreValid(GameViewModel viewModel)
        {
            if (ValuesAreNull(viewModel))
            {
                return false;
            }

            viewModel = TrimStrings(viewModel);

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