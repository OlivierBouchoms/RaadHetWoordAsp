using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    [Route("api/[controller]")]
    public class ProductApiController : Controller
    {
        private readonly string _connectionString;
        private ProductMSSQLContext context;

        public ProductApiController()
        {
            _connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAPITutorial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            context = new ProductMSSQLContext();
        }

        /// <summary>
        /// Gets all products.
        /// url: api/product
        /// </summary>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return context.GetProducts();
        }

        /// <summary>
        /// Gets a product by the id that is inputted.
        /// url: api/product/{id}
        /// </summary>
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var productList = context.GetProducts();
            var item = productList.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

//        /// <summary>
//        /// Change the sales
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [ActionName("ChangeScore")]
//        public IActionResult ChangeScore(ChangeViewModel viewModel)
//        {
//            if (viewModel.cmd == "Increase")
//            {
//                context.IncreaseSale(viewModel.id);
//            }
//            else
//            {
//                context.DecreaseSale(viewModel.id);
//            }
//            return null;
//        }

        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            var product = new Product();
            try
            {
                product = new Product(viewModel.name, viewModel.sales);
            }
            catch
            {
                product = null;
            }
            if (product == null)
            {
                return BadRequest();
            }

            context.InsertProduct(product);

            product = null;
            return new ObjectResult(context.GetProducts().Last());
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            context.RemoveProduct(id);

            return new NoContentResult();
        }

        
    }
}
