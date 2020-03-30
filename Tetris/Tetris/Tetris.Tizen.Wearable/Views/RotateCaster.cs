using Tizen.Wearable.CircularUI.Forms;
using Tetris.ViewModels;

namespace Tetris.Tizen.Wearable.Views
{
    /// <summary>
    /// Class used to cast rotate action to viewmodel
    /// </summary>
    public class RotateCaster : IRotaryEventReceiver
    {
        private readonly GameViewModel _viewModel;

        /// <summary>
        /// Initializes RotateCaster class instance
        /// </summary>
        /// <param name="viewModel">Viewmodel</param>
        public RotateCaster(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Function invoked when bezel is rotated
        /// </summary>
        /// <param name="args">Rotate Event Arguments</param>
        public void Rotate(RotaryEventArgs args)
        {
            _viewModel?.Rotate(args.IsClockwise ? true : false);
        }
    }
}
