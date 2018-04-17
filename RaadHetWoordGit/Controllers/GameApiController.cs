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
        private readonly GameLogic _gameLogic;
        private readonly WordListLogic _wordListLogic;

        public GameApiController()
        {
            _teamInGameLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _gameLogic = new GameLogic(new GameRepository(new GameMemoryContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
        }

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            var products = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                products.Add($"hoi {i}");
            }

            return products;
        }

        [HttpPatch]
        public IActionResult ChangeScore(bool increase)
        {
            var viewModel = GetViewModelFromSession(true);

            if (increase)
            {
                _teamInGameLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);
                _teamLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);

                PlaceViewModelInSession(viewModel, true);
                return new NoContentResult();
            }
            _teamInGameLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);
            _teamLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);

            PlaceViewModelInSession(viewModel, false);

            return new NoContentResult();
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
    }
}