using System.Collections.Generic;
using MindMap.Entity;

namespace MindMap.Interface
{
    public interface ICategoryDataAccess
    {
        /// <summary>
        ///     Create document
        /// </summary>
        /// <param name="category">the document data</param>
        void Insert(Category category);

        /// <summary>
        ///     Update the document
        /// </summary>
        /// <param name="category"></param>
        void Update(Category category);

        /// <summary>
        ///     Delete the document
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetAllCategories();
    }
}