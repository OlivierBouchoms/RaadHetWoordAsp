using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace Data
{
    internal class DataBase
    {
        /// <summary>
        /// SqlConnection object that is used in all the MSSQL contexts.
        /// </summary>
        /// <returns>A SqlConnection object</returns>
        internal static SqlConnection MsSql => new SqlConnection(DataBaseResources.MsSqlConnection);

        /// <summary>
        /// SqlConnection object for logging all exceptions
        /// </summary>
        internal static SQLiteConnection SqLite => new SQLiteConnection(GenerateSqLiteConnectionString());

        private static string FileName => DataBaseResources.SqLiteFile;

        private static string GenerateSqLiteConnectionString()
        {
            return $"Data Source={FileName};Version=3";
        }
    }
}
