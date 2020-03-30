using Xamarin.Forms;
using Pedometer.Views;
using Pedometer.Privilege;

namespace Pedometer.ViewModels
{
    /// <summary>
    /// Provides start page view abstraction
    /// </summary>
    public class StartViewModel : ViewModelBase
    {
        /// <summary>
        /// Navigates to main page
        /// </summary>
        public Command GoToMainPageCommand { get; }

        /// <summary>
        /// Initializes StartViewModel class instance
        /// </summary>
        public StartViewModel()
        {
            GoToMainPageCommand = new Command(ExecuteGoToMainPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToMainPageCommand
        /// Navigates to main page
        /// </summary>
        private void ExecuteGoToMainPageCommand()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}