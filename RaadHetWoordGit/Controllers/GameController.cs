using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class GameController : Controller
    {
        private TeamLogic _teamLogic;
        private GameLogic _gameLogic;
        private WordListLogic _wordListLogic;

        public ActionResult Index()
        {
            var viewModel = new GameViewModel();
            viewModel.TeamColumnClass = "hidden";
            viewModel.TeamFormClass = "Container";

            return View(viewModel);
        }
         
        [HttpPost]
        public ActionResult Index(GameViewModel viewModel)
        {
            //Postback
            //Als een url wordt ingetikt wordt een html pagina gereturned

            viewModel.TeamColumnClass = "Container";
            viewModel.TeamFormClass = "hidden";
            if (ValuesAreNull(viewModel))
            {
                viewModel.TeamOneSuccess = false;
                viewModel.TeamTwoSuccess = false;
                return View(viewModel);
            }

            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
            
            List<Team> teams = new List<Team>
            {
                new Team(viewModel.TeamOne),
                new Team(viewModel.TeamTwo)
            };
            
            viewModel.Game = new Game(MaxScore(viewModel.MaxScore), teams);
            viewModel.Game.Wordlist = new Wordlist(_wordListLogic.GetWords());

            //Teams in database plaatsen
            viewModel.TeamOneSuccess = _teamLogic.AddTeam(teams[0]);
            viewModel.TeamTwoSuccess = _teamLogic.AddTeam(teams[1]);

            return View(viewModel);
        }

//Game serializen
//        var gameJsonSerializeObject = JsonConvert.SerializeObject(viewModel.Game);
//        HttpContext.Session.SetString("gameobject", gameJsonSerializeObject);

//        public ActionResult PlayGame(GameViewModel viewModel)
//        {
//            //object maken
//            var gameJsonSerializeObject = HttpContext.Session.GetString("gameobject");
//            viewModel.Game = JsonConvert.DeserializeObject<Game>(gameJsonSerializeObject);
//            return View(viewModel);
//        }

        private bool ValuesAreNull(GameViewModel viewModel)
        {
            if (String.IsNullOrWhiteSpace(viewModel.TeamOne) || String.IsNullOrWhiteSpace(viewModel.TeamTwo))
            {
                return true;
            }

            return false;

        }

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