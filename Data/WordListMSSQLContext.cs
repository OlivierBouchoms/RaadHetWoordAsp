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
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return new List<string>();
            }

            string query = "SELECT [text] FROM Word";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                words.Add(sqlDataReader.GetString(0));
            }

            sqlConnection.Close();
            return words;
        }

        public List<string> GetWordsFromWordlist()
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return new List<string>();
            }

            string query = "SELECT [idWord], [text] FROM Word where [idWord] IN " +
                           "(SELECT [WordID] FROM [Word_and_WordCategorie] where [WordCategoryID]='0')";
            var dataTable = new DataTable();
            var sqlCommand = new SqlCommand(query, sqlConnection);
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                words.Add(row["Text"].ToString());
            }

            sqlConnection.Close();
            sqlConnection = null;
            return words;
        }
    }
}