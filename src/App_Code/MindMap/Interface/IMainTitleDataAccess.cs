namespace MindMap.Interface
{
    public interface IMainTitleDataAccess
    {
        /// <summary>
        /// Get the main title value
        /// </summary>
        string GetMainTitle();

        /// <summary>
        /// Set the main title value
        /// </summary>
        /// <param name="value"></param>
        void SetMainTitle(string value);
    }
}