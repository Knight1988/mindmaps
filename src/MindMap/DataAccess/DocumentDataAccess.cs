using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MindMap.Entity;
using MindMap.Interface;

namespace MindMap.DataAccess
{
    /// <summary>
    ///     Summary description for DocumentDataAccess
    /// </summary>
    public class DocumentDataAccess : IDocumentDataAccess
    {
        public void Insert(Document document)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "INSERT INTO [MindMapDocument] (Id, UserId, ParentId) VALUES (@Id, @UserId, @ParentId)";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = document.Id;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = document.UserId;
                cmd.Parameters.Add("@ParentId", SqlDbType.UniqueIdentifier).Value = (object)document.ParentId ?? DBNull.Value;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Document document)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "UPDATE [MindMapDocument] SET UserId = @UserId WHERE Id = @Id";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = document.Id;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = document.UserId;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "DELETE FROM [MindMapDocument] WHERE Id = @Id";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool Exist(Document doc)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT COUNT(*) AS [value]
FROM [MindMapDocument] AS [t0]
WHERE ([t0].[Id] = @Id) AND ([t0].[UserId] = @UserId)
";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = doc.Id;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = doc.UserId;
                connection.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public IEnumerable<Document> GetAllDocuments(int userId)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "SELECT Id, CategoryId, UserId FROM [MindMapDocument]";
                var cmd = new SqlCommand(cmdText, connection);
                connection.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var mindMapData = new Document((Guid) r["Id"], Convert.ToInt32(r["CategoryId"]),
                            Convert.ToInt32(r["UserId"]));

                        yield return mindMapData;
                    }
                }
            }
        }

        public IEnumerable<Document> GetDocumentInCategory(int categoryId, int userId)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT [t0].[Id], [t0].[UserId], [t0].[CategoryId], [t0].[ParentId]
FROM [MindMapDocument] AS [t0]
WHERE [t0].[CategoryId] = @CategoryId";
                var cmd = new SqlCommand(cmdText.Trim(), connection);
                cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                connection.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var mindMapData = new Document((Guid) r["Id"], Convert.ToInt32(r["CategoryId"]),
                            Convert.ToInt32(r["UserId"]));

                        yield return mindMapData;
                    }
                }
            }
        }

        public IEnumerable<Document> GetUserPrivateDocuments(int userId)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT [t0].[Id], [t0].[UserId], [t0].[CategoryId], [t0].[ParentId]
FROM [MindMapDocument] AS [t0]
WHERE ([t0].[CategoryId] IS NULL) AND ([t0].[UserId] = @UserId) AND ([t0].[ParentId] IS NULL)
";
                var cmd = new SqlCommand(cmdText.Trim(), connection);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                connection.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var doc = ParseDocument(r);
                        yield return doc;
                    }
                }
            }
        }

        public Document GetReferenceDocument(Guid docId, int userId)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT [t0].[Id], [t0].[UserId], [t0].[CategoryId], [t0].[ParentId]
FROM [MindMapDocument] AS [t0]
WHERE ([t0].[UserId] = @UserId) AND ([t0].[ParentId] = @ParentId)
";
                var cmd = new SqlCommand(cmdText.Trim(), connection);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@ParentId", SqlDbType.UniqueIdentifier).Value = docId;
                connection.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return ParseDocument(r);
                    }
                    return null;
                }
            }
        }

        private Document ParseDocument(IDataReader r)
        {
            return new Document()
            {
                Id = (Guid)r["Id"],
                CategoryId = r["CategoryId"] as int?,
                UserId = Convert.ToInt32(r["UserId"]),
                ParentId = r["ParentId"] as Guid?
            };
        }
    }
}