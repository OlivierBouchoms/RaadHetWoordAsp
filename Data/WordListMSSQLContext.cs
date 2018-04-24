using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class WordListMSSQLContext : IWordListContext
    {
        public List<string> GetWords()
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            string query = "SELECT [text] FROM Word";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                words.Add(sqlDataReader.GetString(0));
            }

            sqlDataReader.Close();
            sqlDataReader.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();
            return words;
        }

        public List<string> GetWordsFromWordlist(int id)
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            string query = "SELECT [idWord], [text] FROM Word where [idWord] IN " +
                           "(SELECT [WordID] FROM [Word_and_WordCategorie] where [WordCategoryID]=@id)";
            var dataTable = new DataTable();
            var sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add("id", SqlDbType.Int).Value = id;
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                words.Add(row["Text"].ToString());
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            return words;
        }
    }
}