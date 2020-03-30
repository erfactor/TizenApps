using Pedometer.Services;

namespace Pedometer
{
    /// <summary>
    /// Pedometer Xamarin.Forms application for Tizen Wearable profile
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
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
            app.Run(args);
        }

        /// <summary>
        /// Executes when app is terminated
        /// </summary>
        protected override void OnTerminate()
        {
            var service = AppTerminatedService.Instance;
            service.Terminate();
            base.OnTerminate();
        }
    }
}