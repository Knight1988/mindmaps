using MindMap.DataAccess;
using MindMap.Interface;

namespace MindMap.Bussiness
{
    public static class MainTitleBussiness
    {
        private static readonly IMainTitleDataAccess DataAccess = new MainTitleDataAccess();
        /// <summary>
        /// Get the main title value
        /// </summary>
        public static string GetMainTitle()
        {
            return DataAccess.GetMainTitle();
        }

        /// <summary>
        /// Set the main title value
        /// </summary>
        public static void SetMainTitle(string value)
        {
            DataAccess.SetMainTitle(value);
        }
    }
}