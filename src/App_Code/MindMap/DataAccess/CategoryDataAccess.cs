using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MindMap.Entity;
using MindMap.Interface;

namespace MindMap.DataAccess
{
    /// <summary>
    /// Summary description for CategoryDataAccess
    /// </summary>
    public class CategoryDataAccess : ICategoryDataAccess
    {
        public void Insert(Category category)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "INSERT INTO [MindMapCategory]([UserId], [Name], [IsPublic])" +
                                       "VALUES (@UserId, @Name, @IsPublic)";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = category.UserId;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                cmd.Parameters.Add("@IsPublic", SqlDbType.Bit).Value = category.IsPublic;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Category category)
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = "UPDATE [MindMapCategory] " +
                                       "SET [Name] = @Name," +
                                       "[IsPublic] = @IsPublic " +
                                       "WHERE [Id] = @Id AND [UserId] = @UserId";
                var cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = category.Id;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = category.UserId;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                cmd.Parameters.Add("@IsPublic", SqlDbType.Bit).Value = category.IsPublic;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all public categories & user private category
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetPublicCategories()
        {
            using (var connection = Connection.NewConnection())
            {
                const string cmdText = @"
SELECT [t0].[Id], [t0].[Name], [t0].[IsPublic]
FROM [MindMapCategory] AS [t0]
WHERE ([t0].[IsPublic] = 1)
";
                var cmd = new SqlCommand(cmdText.Trim(), connection);
                connection.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var mindMapCategory = new Category
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            Name = r["Name"].ToString(),
                            IsPublic = Convert.ToBoolean(r["IsPublic"])
                        };

                        yield return mindMapCategory;
                    }
                }
            }
        }

        public bool CheckPermission(int categoryId, int userId)
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