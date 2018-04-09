using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Data;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.KeyVault.Models;
using Models;
using Newtonsoft.Json;
using RaadHetWoordGit.ViewModels;

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

            return View(viewModel);
        }
        
        /// <summary>
        /// The Index page after teams and maxscore have been entered.
        /// </summary>
        [HttpPost]
        public ActionResult Index(GameViewModel viewModel)
        {
            if (ValuesAreNull(viewModel))
            {
                viewModel.TeamOneSuccess = false;
                viewModel.TeamTwoSuccess = false;
                viewModel.TeamColumnClass = "hidden";
                viewModel.TeamFormClass = "visible";
                return View(viewModel);
            }

            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));

            List<Team> teams = new List<Team>(2)
            {
                _teamLogic.FillWithData(new Team(viewModel.TeamOne)),
                _teamLogic.FillWithData(new Team(viewModel.TeamTwo))
            };

            viewModel.Game = new Game(MaxScore(viewModel.MaxScore), teams);
            viewModel.Game.Wordlist = new Wordlist(_wordListLogic.GetWords());

            //Teams in database plaatsen
            viewModel.TeamOneSuccess = _teamLogic.AddTeam(teams[0]);
            viewModel.TeamTwoSuccess = _teamLogic.AddTeam(teams[1]);

            viewModel.TeamFormClass = "hidden";
            viewModel.TeamColumnClass = "visible";

            return View(viewModel);
        }

        private bool ValuesAreNull(GameViewModel viewModel)
        {
            if (String.IsNullOrWhiteSpace(viewModel.TeamOne) || String.IsNullOrWhiteSpace(viewModel.TeamTwo))
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Validates the MaxScore int to prevent a too high, too low or null value.
        /// </summary>
        /// <param name="input">Input from view.</param>
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