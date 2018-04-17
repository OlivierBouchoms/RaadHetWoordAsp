using System.Data.SqlClient;

namespace Data
{
    public class DataBase
    {
        /// <summary>
        /// SqlConnection object that is used in all the MSSQL contexts.
        /// </summary>
        /// <returns>A SqlConnection object</returns>
        public static SqlConnection MsSql => new SqlConnection(DataBaseResources.MsSqlConnection);
    }
}
