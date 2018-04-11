using System;
using System.Collections.Generic;
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
        private readonly TeamInGameLogic _teamLogic;

        public GameApiController()
        {
            _teamLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
        }

        [HttpPatch]
        public IActionResult ChangeScore(GameViewModel viewModel)
        {
            var vm = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(""));
            viewModel.Game = vm.Game;

            var increase = viewModel.Increase;
            if (increase)
            {
                _teamLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);
                return new NoContentResult();
            }
            else
            {
                _teamLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);
                return new NoContentResult();
            }
        }
    }
}