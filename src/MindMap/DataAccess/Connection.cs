using System.Configuration;
using System.Data.SqlClient;

namespace MindMap.DataAccess
{
    /// <summary>
    /// Summary description for Connection
    /// </summary>
    public static class Connection
    {
        public static SqlConnection NewConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        }
    }
}