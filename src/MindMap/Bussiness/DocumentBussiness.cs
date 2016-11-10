using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MindMap.DataAccess;
using MindMap.Entity;
using MindMap.Interface;
using Newtonsoft.Json;

namespace MindMap.Bussiness
{
    public static class DocumentBussiness
    {
        private static readonly IDocumentDataAccess DataAccess = new DocumentDataAccess();
        private static readonly IMainTitleDataAccess MainTitleDataAccess = new MainTitleDataAccess();

        /// <summary>
        /// Gets the document path.
        /// </summary>
        /// <param name="id">The identifier of document</param>
        /// <returns>System.String.</returns>
        public static string GetFilePath(Guid id)
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/mindmap/");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return Path.Combine(path, string.Format("{0}.json", id));
        }

        /// <summary>
        /// Loads the document.
        /// </summary>
        /// <param name="id">The identifier of document.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Document.</returns>
        public static Document Load(Guid id, int userId)
        {
            var doc =  DataAccess.Load(id);
            // Load the document data
            doc = doc.LoadDocumentData();
            // check permission
            doc.CanEdit = doc.UserId == userId;
            if (doc.CategoryId != null) doc.CanEdit = doc.CanEdit || PermissionBussiness.CanEdit(doc.CategoryId.Value, userId);
            return doc;
        }

        /// <summary>
        /// Loads the default document.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Document.</returns>
        public static Document LoadDefaultDocument(int userId)
        {
            var id = MainTitleDataAccess.GetDefaultDocumentId();
            return Load(id, userId);
        }

        /// <summary>
        /// Saves the document.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="doc">The document.</param>
        public static void Save(int userId, Document doc)
        {
            if (doc.UserId != userId && doc.CategoryId != null)
            {
                doc.UserId = userId;
                doc.ParentId = doc.Id;
                doc.Id = Guid.NewGuid();
            }
            var isExist = DataAccess.Exist(doc);
            // save data to json file
            var path = GetFilePath(doc.Id);
            File.WriteAllText(path, doc.ToJson());

            // save to database
            if (isExist)
                DataAccess.Update(doc);
            else
                DataAccess.Insert(doc);
        }

        /// <summary>
        /// Load document data from file
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns>Document.</returns>
        private static Document LoadDocumentData(this Document doc)
        {
            // get file path
            var path = GetFilePath(doc.Id);
            // get json data
            var json = File.ReadAllText(path);

            // get the result
            var result = JsonConvert.DeserializeObject<Document>(json);
            result.UserId = doc.UserId;
            result.CategoryId = doc.CategoryId;
            result.ParentId = doc.ParentId;

            return result;
        }

        /// <summary>
        ///     Check if document has data
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static bool HasData(Document doc)
        {
            // get file path
            var path = GetFilePath(doc.Id);

            // check if data file exist
            return File.Exists(path);
        }

        public static List<Document> GetDocumentInCategory(int categoryId, int userId)
        {
            return
                DataAccess.GetDocumentInCategory(categoryId, userId)
                    .Where(HasData)
                    //.Select(p => LoadDocumentData(p, false))
                    .ToList();
        }

        public static void Delete(Guid id, int userId)
        {
            // delete the mindmap
            DataAccess.Delete(id);
            // delete json file
            var path = GetFilePath(id);
            if (File.Exists(path)) File.Delete(path);
        }

        public static List<Document> GetUserPrivateDocument(int userId)
        {
            return
                DataAccess.GetUserPrivateDocuments(userId)
                    .Where(HasData)
                    //.Select(p => LoadDocumentData(p, false))
                    .Select(p =>
                    {
                        p.CanEdit = true;// enable edit for private documents
                        p.CanDelete = true;// enable delete for private documents
                        return p;
                    })
                    .ToList();
        }

        public static Document GetReferenceDocument(Guid docId, int userId)
        {
            var doc = DataAccess.GetReferenceDocument(docId, userId);
            if (doc == null) return null;
            //doc = doc.LoadDocumentData(false);
            return doc;
        }
    }
}