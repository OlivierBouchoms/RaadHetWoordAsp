﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MicrosoftWebAPITutorial.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly string _connectionString;

        public ProductController()
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
            GC.Collect();
            return new ObjectResult(item);
        }

        /// <summary>
        /// Adds a product to the memory context.
        /// </summary>
        [HttpPost]
        public IActionResult Create(string name, int sales)
        {
            Product product = new Product();
            try
            {
                product = new Product(name, sales);
            }
            catch (Exception e)
            {
                product = null;
                Debug.WriteLine(e.Message);
            }
            if (product == null)
            {
                return BadRequest();
            }

            InsertProduct(product);

            product = null;
            GC.Collect();
            return new ObjectResult(GetProducts().Last());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            RemoveProduct(id);
            GC.Collect();
            return new NoContentResult();
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
                return null;
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

    }
}