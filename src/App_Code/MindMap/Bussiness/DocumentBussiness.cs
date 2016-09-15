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
    /// <summary>
    ///     Summary description for DocumentBussiness
    /// </summary>
    public static class DocumentBussiness
    {
        private static readonly IDocumentDataAccess DataAccess = new DocumentDataAccess();

        public static string GetFilePath(Guid id)
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/App_Data/mindmap/{0}.json", id));
        }

        /// <summary>
        ///     Save mindmap data to file & database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
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
        ///     Load document from file
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userId">userId to check editable</param>
        /// <returns></returns>
        private static Document LoadDocumentData(this Document doc, int userId)
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
        ///     Check if document has data, delete from db if not exist
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static bool HasData(Document doc)
        {
            // get file path
            var path = GetFilePath(doc.Id);

            // check if data file exist
            if (File.Exists(path)) return true;

            // delete the data
            Delete(doc.Id, doc.UserId);
            return false;
        }

        public static List<Document> GetDocumentInCategory(int categoryId, int userId)
        {
            return
                DataAccess.GetDocumentInCategory(categoryId, userId)
                    .Where(HasData)
                    .Select(p => LoadDocumentData(p, userId))
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
                    .Select(p => LoadDocumentData(p, userId))
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
            doc = doc.LoadDocumentData(userId);
            return doc;
        }
    }
}