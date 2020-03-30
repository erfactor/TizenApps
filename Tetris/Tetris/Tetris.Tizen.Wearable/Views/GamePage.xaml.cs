using Tizen.Wearable.CircularUI.Forms;
using Tetris.ViewModels;

namespace Tetris.Tizen.Wearable.Views
{
    /// <summary>
    /// Page with game
    /// </summary>
    public partial class GamePage : CirclePage
    {
        /// <summary>
        /// Initalizes GamePage class instance
        /// </summary>
        public GamePage()
        {
            InitializeComponent();
            GameViewModel viewModel = BindingContext as GameViewModel;
            RotaryFocusObject = new RotateCaster(viewModel);
        }
    }
}