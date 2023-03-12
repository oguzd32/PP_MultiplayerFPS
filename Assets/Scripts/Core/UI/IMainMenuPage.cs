namespace Core.UI
{
    public interface IMainMenuPage
    {
        /// <summary>
        /// Deactivates this page
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Activates this page
        /// </summary>
        void Show();
    }
}