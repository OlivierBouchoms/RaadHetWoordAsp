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

            const string procedure = "GetWordlists";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                wordlists.Add(sqlDataReader.GetString(0));
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            return wordlists;
        }

        public List<string> GetAllWords()
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "GetWords";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                words.Add(sqlDataReader.GetString(0));
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            return words;
        }

        public List<string> GetWordsFromWordlist(string title)
        {
            var words = new List<string>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string commandText = "GetWordsFromWordlist";
            var sqlCommand = new SqlCommand(commandText, sqlConnection) {CommandType = CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("title", SqlDbType.NChar).Value = title;
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                words.Add(sqlDataReader.GetString(0));
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            return words;
        }
    }
}