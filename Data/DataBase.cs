using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace Data
{
    public class DataBase
    {
        /// <summary>
        /// SqlConnection object that is used in all the MSSQL contexts.
        /// </summary>
        /// <returns>A SqlConnection object</returns>
        public static SqlConnection MsSql => new SqlConnection(DataBaseResources.MsSqlConnection);

        /// <summary>
        /// SqlConnection object for logging all exceptions
        /// </summary>
        public static SQLiteConnection SqLite => new SQLiteConnection(GeneratesqLiteConnectionString());

        private static string GeneratesqLiteConnectionString()
        {
            string path = System.IO.Path.GetFullPath(@"..\..\");
            return $"Data Source={path};Version=3";
        }
    }
}
