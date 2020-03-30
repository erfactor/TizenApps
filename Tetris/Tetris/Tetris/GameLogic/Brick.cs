using Xamarin.Forms;
using Tetris.ViewModels;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Class representing brick
    /// </summary>
    public class Brick : ViewModelBase
    {
        private Color _color;
        private string _text = string.Empty;

        /// <summary>
        /// Initializes Brick class instance
        /// </summary>
        /// <param name="color">Brick's color</param>
        public Brick(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Color of a Brick
        /// </summary>
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        /// <summary>
        /// Text displayed on Brick
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
    }
}
