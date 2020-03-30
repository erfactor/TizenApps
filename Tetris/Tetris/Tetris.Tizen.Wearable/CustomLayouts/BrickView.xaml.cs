using Xamarin.Forms;

namespace Tetris.Tizen.Wearable.CustomLayouts
{
    /// <summary>
    /// Class representing colorable brick with a spark (light reflex on surface of a brick)
    /// </summary>
    public partial class BrickView : ContentView
    {
        private Color _brickColor;

        /// <summary>
        /// Initializes new BrickView class instance
        /// </summary>
        public BrickView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Brick's color
        /// </summary>
        public Color BrickColor
        {
            get => _brickColor;
            private set
            {
                _brickColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Allows to set the Color of brick
        /// </summary>
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(BrickView),
            null,
            propertyChanged: OnColorChanged);

        /// <summary>
        /// Gets or sets the brick's color
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Handles execution of OnColorChanged event
        /// </summary>
        /// <param name="bindable">BindableObject which raised the event</param>
        /// <param name="oldvalue">Old value of property</param>
        /// <param name="newvalue">New value of property</param>
        private static void OnColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BrickView myself)
            {
                Color color = (Color)newvalue;
                myself._spark.IsVisible = color == Color.Transparent ? false : true;
                myself.BrickColor = color;
            }
        }
    }
}