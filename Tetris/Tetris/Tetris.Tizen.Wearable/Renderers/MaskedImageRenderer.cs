using Tetris.Tizen.Wearable.CustomLayouts;
using Tetris.Tizen.Wearable.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(MaskedImage), typeof(MaskedImageRenderer))]

namespace Tetris.Tizen.Wearable.Renderers
{
    /// <summary>
    /// Renderer for <see cref="MaskedImage"/>
    /// </summary>
    public class MaskedImageRenderer : ImageRenderer
    {
        /// <summary>
        /// Handles execution of OnElementChanged event
        /// </summary>
        /// <param name="e">Changed element</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            UpdateColor();
        }

        /// <summary>
        /// Handles execution of OnElementPropertyChanged event
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event arguments</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            UpdateColor();
        }

        /// <summary>
        /// Updates image color
        /// </summary>
        private void UpdateColor()
        {
            var colorImage = Element as MaskedImage;
            if (Control == null || colorImage == null)
            {
                return;
            }

            Control.Color = colorImage.ColorMask.ToNative();
        }
    }
}