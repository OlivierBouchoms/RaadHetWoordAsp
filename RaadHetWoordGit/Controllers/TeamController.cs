using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class TeamController : Controller
    {
        private readonly ChecksLogic _checksLogic;
        private readonly TeamLogic _teamLogic;

        public TeamController()
        {
            _checksLogic = new ChecksLogic();
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
        }

        public ViewResult Index(int id)
        {
            var viewModel = new TeamViewModel();
            viewModel.Team = _teamLogic.GetTeam(id);
            viewModel.WinLossClass = _checksLogic.GetWinLossClass(viewModel.Team.WinLoss); 
            return View(viewModel);
        }


    }
}