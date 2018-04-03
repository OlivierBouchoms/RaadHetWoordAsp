using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    public class ProductController : Controller
    {
        string _connectionstring =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAPITutorial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ProductViewModel viewModel)
        {
//            InsertProduct(new Product(viewModel.name, viewModel.value));
            return View();
        }
    }
}
