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
            var viewModel = GetViewModelFromSession(true);
            viewModel.Game.CurrentRound = new Round(viewModel.Game);

            PlaceViewModelInSession(viewModel, true);

            return Json(Url.Action("PlayGame", "Game"));
        }

        [HttpPatch]
        public IActionResult ChangeScore(bool increase)
        {
            var viewModel = GetViewModelFromSession(true);

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

                PlaceViewModelInSession(viewModel, true);
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

            PlaceViewModelInSession(viewModel, false);

            return new NoContentResult();
        }

        /// <summary>
        /// Place gameviewmodel in session 
        /// </summary>
        /// <param name="_round">Is there a round to store in the session?</param>
        private void PlaceViewModelInSession(GameViewModel inputViewModel, bool _round)
        {
            var teamList = inputViewModel.Game.TeamList;
            var wordList = inputViewModel.Game.Wordlist.Words;
            var round = new Round();
            if (_round)
            {
                round = inputViewModel.Game.CurrentRound;
                HttpContext.Session.SetString(nameof(Round), JsonConvert.SerializeObject(round));
                inputViewModel.Game.CurrentRound = null;
            }
            HttpContext.Session.SetString("teamlist", JsonConvert.SerializeObject(teamList));
            HttpContext.Session.SetString(nameof(Wordlist), JsonConvert.SerializeObject(wordList));
            inputViewModel.Game.TeamList = null;
            inputViewModel.Game.Wordlist = null;

            HttpContext.Session.SetString(nameof(GameViewModel), JsonConvert.SerializeObject(inputViewModel));

            inputViewModel.Game.TeamList = teamList;
            inputViewModel.Game.Wordlist = new Wordlist(wordList);
            if (_round)
            {
                try
                {
                    inputViewModel.Game.CurrentRound = new Round();
                    inputViewModel.Game.CurrentRound.Team = round.Team;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Retrieve gameviewmodel from session
        /// </summary>        
        /// Heel veel test code
        private GameViewModel GetViewModelFromSession(bool _round)
        {
            var teamList = JsonConvert.DeserializeObject<List<Team>>(HttpContext.Session.GetString("teamlist"));
            var wordList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString(nameof(Wordlist)));
            var viewModel = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(nameof(GameViewModel)));

            viewModel.Game.TeamList = teamList;
            try
            {
                viewModel.Game.Wordlist = new Wordlist(wordList);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            var round = new Round();
            try
            {
                round = JsonConvert.DeserializeObject<Round>(HttpContext.Session.GetString(nameof(Round)));
                Debug.WriteLine(HttpContext.Session.GetString(nameof(Round)));
                Debug.WriteLine(round.ToString());
                //Gaat fout
                viewModel.Game.CurrentRound = new Round();
                viewModel.Game.CurrentRound.Team = round.Team;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return viewModel;
        }
    }
}