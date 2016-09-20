using System;
using System.Data;
using System.Data.SqlClient;
using MindMap.Interface;

namespace MindMap.DataAccess
{
    public class PermissionDataAccessDataAccess : IPermissionDataAccess
    {
        public bool CanEdit(int categoryId, int userId)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT COUNT(*) AS [value]
FROM [MindMapPermission] AS [t0]
WHERE ([t0].[CategoryId] = @CategoryId) AND ([t0].[UserId] = @UserId)
";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                connection.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}