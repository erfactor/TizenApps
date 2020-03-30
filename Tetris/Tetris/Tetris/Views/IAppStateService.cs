using System;

namespace Tetris.Views
{
    /// <summary>
    /// Interface representing app state
    /// </summary>
    public interface IAppStateService
    {
        /// <summary>
        /// Event invoked when app is paused
        /// </summary>
        event EventHandler Paused;

        /// <summary>
        /// Event invoked when app is resumed
        /// </summary>
        event EventHandler Resumed;

        /// <summary>
        /// Event invoked when app is terminated
        /// </summary>
        event EventHandler Terminated;

        /// <summary>
        /// Used to invoke Paused event
        /// </summary>
        void Pause();

        /// <summary>
        /// Used to invoke Resumed event
        /// </summary>
        void Resume();

        /// <summary>
        /// Used to invoke Terminated event
        /// </summary>
        void Terminate();
    }
}
