using Xamarin.Forms;
using Pedometer.Views;

namespace Pedometer.ViewModels
{
    /// <summary>
    /// Provides welcome page view abstraction
    /// </summary>
    public class WelcomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Navigates to start page
        /// </summary>
        public Command GoToStartPageCommand { get; }

        /// <summary>
        /// Initializes WelcomeViewModel class instance
        /// </summary>
        public WelcomeViewModel()
        {
            GoToStartPageCommand = new Command(ExecuteGoToStartPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToStartPageCommand
        /// Navigates to start page
        /// </summary>
        private void ExecuteGoToStartPageCommand()
        {
            Application.Current.MainPage = new StartPage();
        }
    }
}