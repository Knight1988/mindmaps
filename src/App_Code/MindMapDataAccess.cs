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

    public static void Save(MindMapData mindMapData)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "INSERT INTO [MindMapData] (Id, UserId, Title) VALUES (@Id, @UserId, @Title)";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = mindMapData.Id;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = mindMapData.UserId;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = mindMapData.Title;
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static MindMapData Load(Guid id)
    {
        using (var connection = NewConnection())
        {
            var cmdText = "SELECT Id, UserId, Data FROM [MindMapData] WHERE Id = @Id";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
            connection.Open();
            using (var r = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                return r.Read()
                    ? new MindMapData((Guid) r["Id"], Convert.ToInt32(r["UserId"]), r["Title"].ToString())
                    : null;
            }
        }
    }

    public static IEnumerable<MindMapData> GetList(int userId)
    {
        using (var connection = NewConnection())
        {
            const string cmdText = "SELECT Id, UserId, Data FROM [MindMapData] WHERE UserId = @UserId";
            var cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            connection.Open();
            using (var r = cmd.ExecuteReader())
            {
                if (r.Read())
                {
                    var mindMapData = new MindMapData((Guid) r["Id"], Convert.ToInt32(r["UserId"]),
                        r["Title"].ToString());

                    yield return mindMapData;
                }
            }
        }
    }
}