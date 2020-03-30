using Xamarin.Forms;
using Tetris.Views;

namespace Tetris
{
    /// <summary>
    /// Tetris Xamarin.Forms application for Tizen Wearable profile
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        private static IAppStateService _appStateService;

        /// <summary>
        /// Handles creation phase of the application
        /// Loads Xamarin.Forms application
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Application entry point
        /// Initializes Xamarin.Forms application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            _appStateService = DependencyService.Get<IAppStateService>();
            app.Run(args);
        }

        /// <summary>
        /// Executed when app is paused
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            _appStateService.Pause();
        }

        /// <summary>
        /// Executed when game is resumed
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            _appStateService.Resume();
        }

        /// <summary>
        /// Executed when game is terminated
        /// </summary>
        protected override void OnTerminate()
        {
            base.OnTerminate();
            _appStateService.Terminate();
        }

    }

}