using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///     Summary description for MindMapDataAccess
/// </summary>
public static class MindMapDataAccess
{
    private static SqlConnection NewConnection()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
    }

    public static void Insert(MindMapData mindMapData)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "INSERT INTO [MindMapData] (Id, UserId) VALUES (@Id, @UserId)";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = mindMapData.Id;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = mindMapData.UserId;
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void Update(MindMapData mindMapData)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "UPDATE [MindMapData] SET UserId = @UserId WHERE Id = @Id";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = mindMapData.Id;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = mindMapData.UserId;
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void Delete(Guid id)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "DELETE FROM [MindMapData] WHERE Id = @Id";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static bool Exist(Guid id)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "SELECT COUNT(*) FROM [MindMapData] WHERE Id = @Id";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
            connection.Open();
            return Convert.ToBoolean(cmd.ExecuteScalar());
        }
    }

    public static IEnumerable<MindMapData> GetList(int userId)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "SELECT Id, UserId FROM [MindMapData] WHERE UserId = @UserId";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            connection.Open();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var mindMapData = new MindMapData((Guid) r["Id"], Convert.ToInt32(r["UserId"]));

                    yield return mindMapData;
                }
            }
        }
    }
}