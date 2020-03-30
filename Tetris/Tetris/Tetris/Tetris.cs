using Tetris.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Tetris
{
    /// <summary>
    /// Main class of Tetris
    /// Inherits from Xamarin.Forms.Application
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes Tetris class instance
        /// </summary>
        public App()
        {
            DependencyService.Get<IPageNavigation>()
                .CreateMainPage();
        }
    }
}
