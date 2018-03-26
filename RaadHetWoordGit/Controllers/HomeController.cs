﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(HomeIndexViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
