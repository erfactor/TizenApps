namespace Tetris.Views
{
    /// <summary>
    /// Interface for navigating between pages
    /// </summary>
    public interface IPageNavigation
    {
        /// <summary>
        /// Creates application main page and set it as active
        /// </summary>
        void CreateMainPage();

        /// <summary>
        /// Navigates to page with instruction
        /// </summary>
        void GoToInstructionPage();

        /// <summary>
        /// Navigates to page with game
        /// </summary>
        void GoToGamePage();
    }
}
