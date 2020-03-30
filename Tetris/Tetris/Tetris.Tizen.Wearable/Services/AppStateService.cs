using System;
using Tetris.Views;
using Tetris.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppStateService))]

namespace Tetris.Tizen.Wearable.Services
{
    /// <summary>
    /// Service for managing app state
    /// </summary>
    public sealed class AppStateService : IAppStateService
    {
        /// <summary>
        /// Event invoked when app is paused
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Event invoked when app is resumed
        /// </summary>
        public event EventHandler Resumed;

        /// <summary>
        /// Event invoked when app is terminated
        /// </summary>
        public event EventHandler Terminated;

        /// <summary>
        /// Used to invoke Paused event
        /// </summary>
        public void Pause()
        {
            Paused?.Invoke(this,EventArgs.Empty);
        }

        /// <summary>
        /// Used to invoke Resumed event
        /// </summary>
        public void Resume()
        {
            Resumed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Used to invoke Terminated event
        /// </summary>
        public void Terminate()
        {
            Terminated?.Invoke(this, EventArgs.Empty);
        }
    }
}