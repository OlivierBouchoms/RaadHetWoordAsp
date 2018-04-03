﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Models;

namespace Data
{
    public class ProductMSSQLContext
    {
        /// <summary>
        /// Returns a list of all products in the database.
        /// </summary>
        public List<Product> GetProducts()
        {
            var sqlConnection = DataBase._ProductSqlConn;
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
        public bool InsertProduct(Product product)
        {
            var sqlConnection = DataBase._ProductSqlConn;
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
        /// Increase product sales with SQL Injection protection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IncreaseSale(int id)
        {
            var sqlConnection = DataBase._ProductSqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            var commandText = "UPDATE [dbo].[Product] SET [Sales] = [Sales] + 1 Where [Id] = @id";
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.Add("ID", SqlDbType.Int).Value = id;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                return true;
            }

            sqlConnection.Close();
            return false;
        }

        /// <summary>
        /// Decrease product sales with SQL Injection protection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DecreaseSale(int id)
        {
            var sqlConnection = DataBase._ProductSqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            var commandText = "UPDATE [dbo].[Product] SET [Sales] = [Sales] -1 1 Where [Id] = @id";
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.Add("id", SqlDbType.Int).Value = id;
            Debug.WriteLine(sqlCommand.CommandText);
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
        public bool RemoveProduct(int id)
        {
            var sqlConnection = DataBase._ProductSqlConn;
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
