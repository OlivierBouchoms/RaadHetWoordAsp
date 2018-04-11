using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
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
            bool increase = (new Random().Next() % 2 == 0);
            if (increase)
            {
                _teamLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);
            }
            else
            {
                _teamLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);
            }
            return new NoContentResult();
        }
    }
}