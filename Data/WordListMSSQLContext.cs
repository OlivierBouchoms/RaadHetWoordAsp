using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class WordListMSSQLContext : IWordListContext
    {
        public List<string> GetWordlists()
        {
            var wordlists = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string query = "Select [Name] FROM [WordCategory] ORDER BY [Name] ASC";
            var sqlCommand = new SqlCommand(query, sqlConnection);
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                wordlists.Add(sqlDataReader.GetString(0));
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlDataReader.Dispose();
            return wordlists;
        }

        public List<string> GetAllWords()
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string query = "SELECT [text] FROM Word";
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

        public List<string> GetWordsFromWordlist(string title)
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string commandText = "SELECT [text] " +
                           "FROM[word] " +
                           "INNER JOIN[word_and_wordcategorie] " +
                           "ON [word].[idword] = [word_and_wordcategorie].[wordid] " +
                           "INNER JOIN[wordcategory] " +
                           "ON[word_and_wordcategorie].[wordcategoryid] = " +
                           "[wordcategory].[idwordcategory] " +
                           "WHERE[wordcategory].[name] = @title";
            var dataTable = new DataTable();
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.Add("title", SqlDbType.VarChar).Value = title;
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