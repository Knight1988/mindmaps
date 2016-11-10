using System;
using System.Data;
using System.Data.SqlClient;
using MindMap.Interface;

namespace MindMap.DataAccess
{
    public class MainTitleDataAccess : IMainTitleDataAccess
    {
        /// <summary>
        /// Get the main title value
        /// </summary>
        public string GetMainTitle()
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"SELECT [MainTitle] FROM [MindMapMainTitle] AS [t0]";
                var cmd = new SqlCommand(cmdText, connection);
                connection.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        /// <summary>
        /// Set the main title value
        /// </summary>
        /// <param name="value"></param>
        public void SetMainTitle(string value)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "UPDATE [MindMapMainTitle] SET MainTitle = @MainTitle";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@MainTitle", SqlDbType.NVarChar).Value = value;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Guid GetDefaultDocumentId()
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"SELECT [DefaultDocumentId] FROM [MindMapMainTitle] AS [t0]";
                var cmd = new SqlCommand(cmdText, connection);
                connection.Open();
                return (Guid)cmd.ExecuteScalar();
            }
        }
    }
}