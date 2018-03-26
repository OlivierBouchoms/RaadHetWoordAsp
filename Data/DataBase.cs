using System.Data.SqlClient;

namespace Data
{
    public class DataBase
    {
        public static SqlConnection _SqlConn
        {
            get
            {
                return new SqlConnection(DataBaseResources.ConnectionString);
            }
        }

    }
}
