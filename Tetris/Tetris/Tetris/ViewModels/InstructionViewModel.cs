using Tetris.Views;
using Xamarin.Forms;

namespace Tetris.ViewModels
{
    /// <summary>
    /// Provides instruction page view abstraction
    /// </summary>
    public class InstructionViewModel : ViewModelBase
    {
        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService
        /// </summary>
        private readonly IPageNavigation _navigation;

        private string _text = "Use bezel to move shape and tap to rotate";

        /// <summary>
        /// Represents text displayed as an instruction to game
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        /// <summary>
        /// Navigates to page with Game
        /// </summary>
        public Command GoToGamePageCommand { get; }

        /// <summary>
        /// Initializes InstructionViewModel class instance
        /// </summary>
        public InstructionViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();
            GoToGamePageCommand = new Command(ExecuteGoToGamePageCommand);
        }

        /// <summary>
        /// Handles execution of GoToGamePageCommand
        /// Navigates to page with game
        /// </summary>
        private void ExecuteGoToGamePageCommand()
        {
            _navigation.GoToGamePage();
        }
    }
}