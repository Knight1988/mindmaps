using System;
using System.Collections.Generic;
using MindMap.Entity;

namespace MindMap.Interface
{
    public interface IDocumentDataAccess
    {
        /// <summary>
        ///     Create document
        /// </summary>
        /// <param name="document">the document data</param>
        void Insert(Document document);

        /// <summary>
        ///     Update the document
        /// </summary>
        /// <param name="document"></param>
        void Update(Document document);

        /// <summary>
        ///     Delete the document
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);

        /// <summary>
        /// Check if document is exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exist(Document id);

        /// <summary>
        /// Get all document
        /// </summary>
        /// <param name="userId">userId for permission</param>
        /// <returns></returns>
        IEnumerable<Document> GetAllDocuments(int userId);

        /// <summary>
        /// Get all documents in category
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <param name="userId">userId for permission</param>
        /// <returns></returns>
        IEnumerable<Document> GetDocumentInCategory(int categoryId, int userId);

        /// <summary>
        /// Get user private documents
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Document> GetUserPrivateDocuments(int userId);

        /// <summary>
        /// Get reference document
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Document GetReferenceDocument(Guid docId, int userId);

        /// <summary>
        /// Load the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Document Load(Guid id);
    }
}