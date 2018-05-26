using Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamLogic _teamLogic;

        public TeamController()
        {
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMssqlContext()));
        }

        public ViewResult Index(int id)
        {
            var viewModel = new TeamViewModel();
            viewModel.Team = _teamLogic.GetTeam(id);
            return View(viewModel);
        }
    }
}