using Xamarin.Forms;

namespace Tetris.Tizen.Wearable.CustomLayouts
{
    /// <summary>
    /// Image with color mask control
    /// </summary>
    public class MaskedImage : Image
    {
        /// <summary>
        /// Backing store for the ColorMask bindable property
        /// </summary>
        public static readonly BindableProperty ColorMaskProperty =
            BindableProperty.Create(nameof(ColorMask), typeof(Color), typeof(MaskedImage), Color.Transparent);

        /// <summary>
        /// Gets or sets image color mask
        /// </summary>
        public Color ColorMask
        {
            get => (Color)GetValue(ColorMaskProperty);
            set => SetValue(ColorMaskProperty, value);
        }
    }
}