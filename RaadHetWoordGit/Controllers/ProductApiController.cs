using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;
using RaadHetWoordGit.ViewModels;

namespace RaadHetWoordGit.Controllers
{
    [Route("api/[controller]")]
    public class ProductApiController : Controller
    {
        private readonly string _connectionString;

        public ProductApiController()
        {
            _connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAPITutorial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        /// <summary>
        /// Gets all products.
        /// url: api/product
        /// </summary>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return GetProducts();
        }

        /// <summary>
        /// Gets a product by the id that is inputted.
        /// url: api/product/{id}
        /// </summary>
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var productList = GetProducts();
            var item = productList.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            Product product;
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

            if (InsertProduct(product))
            {
                return new NoContentResult();
            }

            return BadRequest("Database failed");
        }

        /// <summary>
        /// Increase or decrease value, depending on parameter from viewModel
        /// </summary>
        [HttpPatch]
        public IActionResult Change(ProductViewModel viewModel)
        {
            var product = GetProducts().FirstOrDefault(p => p.Id == viewModel.id);
            if (product == null)
            {
                return NotFound();
            }

            if (ChangePrice(viewModel))
            {
                return new NoContentResult();
            }

            return BadRequest("Database failed");
        }

        /// <summary>
        /// Delete a product from the database.
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(ProductViewModel viewModel)
        {
            var product = GetProducts().FirstOrDefault(p => p.Id == viewModel.id);
            if (product == null)
            {
                return NotFound();
            }

            if (RemoveProduct(viewModel.id))
            {
                return new NoContentResult();
            }

            return BadRequest("Database failed");
        }

        //Database:

        /// <summary>
        /// Returns a list of all products in the database.
        /// </summary>
        private List<Product> GetProducts()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return new List<Product>();
            }

            var products = new List<Product>();
            var query = "Select * From [Product]";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product(Convert.ToInt32(row["Id"].ToString()), row["Name"].ToString(), Convert.ToInt32(row["Sales"].ToString())));
            }

            sqlConnection.Close();

            return products;
        }

        /// <summary>
        /// Insert a product into the database.
        /// </summary>
        private bool InsertProduct(Product product)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            var query = $"INSERT INTO [dbo].[Product] ([Name], [Sales]) VALUES ('{product.Name}', {product.Sales})";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                return true;
            }

            sqlConnection.Close();

            return false;
        }

        /// <summary>
        /// Removes a product from the database.
        /// </summary>
        private bool RemoveProduct(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            var query = $"Delete from [Product] Where [Id] ={id}";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                return true;
            }

            sqlConnection.Close();

            return false;
        }

        public bool ChangePrice(ProductViewModel viewModel)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            string query = null;
            if (viewModel.increase)
            {
                query = $"UPDATE [Product] SET [Sales] = [Sales] + 1 WHERE [Id] = {viewModel.id}";
            }

            if (!viewModel.increase)
            {
                query = $"UPDATE [Product] SET [Sales] = [Sales] - 1 WHERE [Id] = {viewModel.id}";
            }

            var sqlCommand = new SqlCommand(query, sqlConnection);
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                return true;
            }

            sqlConnection.Close();

            return false;
        }

    }
}
