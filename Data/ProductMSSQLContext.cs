using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class ProductMSSQLContext
    {
        private string _connectionstring;

        public ProductMSSQLContext()
        {
            _connectionstring =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAPITutorial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        }
        /// <summary>
        /// Returns a list of all products in the database.
        /// </summary>
        private List<Product> GetProducts()
        {
            var sqlConnection = new SqlConnection(_connectionstring);
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
            var sqlConnection = new SqlConnection(_connectionstring);
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
            var sqlConnection = DataBase._SqlConn;
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
