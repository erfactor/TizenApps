using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pedometer.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Pedometer
{
    /// <summary>
    /// Main class of Pedometer
    /// Inherits from Xamarin.Forms.Application
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes Pedometer class instance
        /// </summary>
        public App()
        {
            MainPage = new WelcomePage();
        }
    }
}