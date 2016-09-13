using System;
using System.Collections.Generic;
using System.Linq;
using MindMap.DataAccess;
using MindMap.Entity;
using MindMap.Interface;

namespace MindMap.Bussiness
{
    /// <summary>
    /// Summary description for CategoryBussiness
    /// </summary>
    public class CategoryBussiness
    {
        private static readonly ICategoryDataAccess DataAccess = new CategoryDataAccess();

        /// <summary>
        ///     Get all public categories and user private docs
        /// </summary>
        /// <param name="userId">userId for permission</param>
        /// <returns></returns>
        public static List<Category> GetPublicCategories(int userId)
        {
            // get all public categories
            var categories = DataAccess.GetPublicCategories().ToList();

            // get all data in categories
            foreach (var category in categories)
            {
                // get documents
                var documents = DocumentBussiness.GetDocumentInCategory(category.Id, userId);
                category.Documents = documents;
            }

            // add private category
            categories.Add(GetPrivateCategory(userId));

            return categories;
        }

        /// <summary>
        /// Get private category
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Category GetPrivateCategory(int userId)
        {
            return new Category()
            {
                Id = 0,
                UserId = userId,
                IsPublic = true,
                Name = "Private",
                Documents = DocumentBussiness.GetUserPrivateDocument(userId)
            };
        }
    }
}