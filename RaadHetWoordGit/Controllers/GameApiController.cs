using System;
using System.Collections.Generic;
using System.Diagnostics;
using Data;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    [Produces("application/json")]
    [Route("api/GameApi")]
    public class GameApiController : Controller
    {
        private readonly TeamInGameLogic _teamInGameLogic;
        private readonly TeamLogic _teamLogic;

        public GameApiController()
        {
            _teamInGameLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
        }

        [HttpPost]
        public IActionResult NextRound()
        {
            var viewModel = GetViewModelFromSession();
            viewModel.Game.CurrentRound = new Round(viewModel.Game);

            PlaceViewModelInSession(viewModel);

            return Json(Url.Action("PlayGame", "Game"));
        }

        [HttpPatch]
        public IActionResult ChangeScore(bool increase)
        {
            var viewModel = GetViewModelFromSession();

            if (increase)
            {
                _teamInGameLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);
                _teamLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);

                for (int i = 0; i < 2; i++)
                {
                    if (viewModel.Game.CurrentRound.Team.Name == viewModel.Game.TeamList[i].Name)
                    {
                        viewModel.Game.TeamList[i] = viewModel.Game.CurrentRound.Team;
                    }
                }

                PlaceViewModelInSession(viewModel);
                return new NoContentResult();
            }
            _teamInGameLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);
            _teamLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);

            for (int i = 0; i < 2; i++)
            {
                if (viewModel.Game.CurrentRound.Team.Name == viewModel.Game.TeamList[i].Name)
                {
                    viewModel.Game.TeamList[i] = viewModel.Game.CurrentRound.Team;
                }
            }

            PlaceViewModelInSession(viewModel);

            return new NoContentResult();
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
    }
}