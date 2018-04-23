using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Data
{
    public class DataBase
    {
        /// <summary>
        /// SqlConnection object that is used in all the MSSQL contexts.
        /// </summary>
        /// <returns>A SqlConnection object</returns>
        public static SqlConnection MsSql => new SqlConnection(DataBaseResources.MsSqlConnection);

        public static string FileName => DataBaseResources.SqLiteFile;

        /// <summary>
        /// SqlConnection object for logging all exceptions
        /// </summary>
        public static SQLiteConnection SqLite => new SQLiteConnection(GenerateSqLiteConnectionString());

        private static string GenerateSqLiteConnectionString()
        {
            string directory = Directory.GetCurrentDirectory().Replace("RaadHetWoordGit", "Data");
            return $"Data Source={directory}\\{FileName};Version=3";
        }
    }
}
