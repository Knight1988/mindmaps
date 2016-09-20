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
    public static class CategoryBussiness
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
            var categories = DataAccess.GetAllCategories().ToList();

            // get all data in categories
            foreach (var category in categories)
            {
                // get view permission
                category.CanView = PermissionBussiness.CanView(category, userId);

                // do not get documents if don't have view permission
                if (!category.CanView)
                {
                    category.Documents = new List<Document>();
                    continue;
                }

                // get documents
                var documents = DocumentBussiness.GetDocumentInCategory(category.Id, userId);

                // check permission
                var canEdit = PermissionBussiness.CanEdit(category.Id, userId);

                // replace reference documents
                if (canEdit) documents = documents.GetReferenceDocuments(userId).ToList();

                category.Documents = documents;
            }

            // add private category
            categories.Add(GetPrivateCategory(userId));

            return categories;
        }

        private static IEnumerable<Document> GetReferenceDocuments(this List<Document> documents, int userId)
        {
            foreach (var doc in documents)
            {
                // check if has refDoc
                var refDoc = DocumentBussiness.GetReferenceDocument(doc.Id, userId);
                if (refDoc != null)
                {
                    // return refDoc if found
                    refDoc.CanEdit = true;
                    refDoc.CanDelete = false;
                    refDoc.CanRestore = true;
                    yield return refDoc;
                }
                else
                {
                    // return origin doc
                    doc.CanEdit = true;
                    doc.CanDelete = false;
                    doc.CanRestore = false;
                    yield return doc;
                }
            }
        }

        /// <summary>
        /// Get private category
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Category GetPrivateCategory(int userId)
        {
            var category = new Category()
            {
                Id = 0,
                UserId = userId,
                IsPublic = false,
                CanEdit = true,
                Name = "Private"
            };

            category.CanView = PermissionBussiness.CanView(category, userId);
            category.Documents = category.CanView ? DocumentBussiness.GetUserPrivateDocument(userId) : new List<Document>();

            return category;
        }
    }
}