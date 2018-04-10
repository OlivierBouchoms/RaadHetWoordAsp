using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Data
{
    public class WordListMSSQLContext : IWordListContext
    {
        public List<string> GetWordsFromWordlist()
        {
            var words = new List<string>();
            var sqlConnection = DataBase._SqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return null;
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