namespace MindMap.Interface
{
    public interface IPermissionDataAccess
    {
        /// <summary>
        ///  Check if user can edit this category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CanEdit(int categoryId, int userId);
    }
}