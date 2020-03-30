using Tetris.Tizen.Wearable.Views;
using Tetris.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]

namespace Tetris.Tizen.Wearable.Views
{
    /// <summary>
    /// Application navigation class
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        /// <summary>
        /// Creates application main page and set it as active
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new MainPage();
        }

        /// <summary>
        /// Navigates to page with instruction
        /// </summary>
        public void GoToInstructionPage()
        {
            Application.Current.MainPage = new InstructionPage();
        }

        /// <summary>
        /// Navigates to page with game
        /// </summary>
        public void GoToGamePage()
        {
            Application.Current.MainPage = new GamePage();
        }
    }
}
