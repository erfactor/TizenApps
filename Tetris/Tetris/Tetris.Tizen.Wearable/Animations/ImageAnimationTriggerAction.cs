using Xamarin.Forms;

namespace Tetris.Tizen.Wearable.Animations
{
    /// <summary>
    /// Block curtain animation class
    /// </summary>
    class ImageAnimationTriggerAction : TriggerAction<Image>
    {
        /// <summary>
        /// Block curtain animation
        /// </summary>
        /// <param name="sender">Animated object</param>
        protected override void Invoke(Image sender)
        {
            if (sender.IsVisible)
            {
                sender.LayoutTo(new Rectangle(81, 360, 198, 1041), 3000, Easing.CubicInOut);
            }
        }
    }
}