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

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            var hoi = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                hoi.Add($"hoi {i}");
            }

            return hoi;
        }

        [HttpPatch]
        public IActionResult ChangeScore(bool increase)
        {
            //Probleem: probeert een round object te maken. Dit gaat niet omdat er nog geen game object is om een ronde mee te maken
            var vm = JsonConvert.DeserializeObject<GameViewModel>(HttpContext.Session.GetString(nameof(GameViewModel)));

            if (increase)
            {
                _teamLogic.IncreaseScore(vm.Game.CurrentRound.Team);

                //Viewmodel in sessie plaatsen
                HttpContext.Session.SetString(key: nameof(GameViewModel), value: JsonConvert.SerializeObject(vm));
                return new NoContentResult();
            }
            _teamLogic.DecreaseScore(vm.Game.CurrentRound.Team);

            //Viewmodel in sessie plaatsen
            HttpContext.Session.SetString(key: nameof(GameViewModel), value: JsonConvert.SerializeObject(vm));
            return new NoContentResult();
        }
    }
}