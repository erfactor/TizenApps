using Tetris.Views;
using Xamarin.Forms;

namespace Tetris.ViewModels
{
    /// <summary>
    /// Provides main page view abstraction
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Navigates to page with Instruction
        /// </summary>
        public Command GoToInstructionPageCommand { get; }

        /// <summary>
        /// Initializes MainViewModel class instance
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();
            GoToInstructionPageCommand = new Command(ExecuteGoToInstructionPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToInstructionPageCommand
        /// Navigates to page with instruction
        /// </summary>
        private void ExecuteGoToInstructionPageCommand()
        {
            _navigation.GoToInstructionPage();
        }

    }
}