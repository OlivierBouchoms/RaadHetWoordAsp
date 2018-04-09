using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Kan wellicht weg
        /// </summary>
        [HttpPost]
        public ActionResult Index(ProductViewModel viewModel)
        {
            return View();
        }
    }
}
