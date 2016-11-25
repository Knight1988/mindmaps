using System.Linq;
using MindMap.DataAccess;
using MindMap.Entity;
using MindMap.Interface;

namespace MindMap.Bussiness
{
    public static class PermissionBussiness
    {
        private static readonly IPermissionDataAccess DataAccess = new PermissionDataAccessDataAccess();

        /// <summary>
        ///  Check if user is vip
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsVip(int userId)
        {
            // TODO: check if user is vip
            return new []{1, 2}.Any(p => p == userId);
        }

        /// <summary>
        /// Check if user can edit category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CanEdit(int categoryId, int userId)
        {
            return DataAccess.CanEdit(categoryId, userId);
        }

        /// <summary>
        /// Check if user can view the category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CanView(Category category, int userId)
        {
            return IsVip(userId) || category.IsPublic;
        }
    }
}