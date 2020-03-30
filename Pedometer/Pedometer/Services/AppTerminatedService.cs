using System;

namespace Pedometer.Services
{
    /// <summary>
    /// Service for managing app state
    /// </summary>
    public sealed class AppTerminatedService
    {
        private static AppTerminatedService _this;

        /// <summary>
        /// Initializes AppTerminatedService class
        /// </summary>
        private AppTerminatedService()
        {
        }

        /// <summary>
        /// Provides singleton instance of AppTerminatedService class
        /// </summary>
        public static AppTerminatedService Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new AppTerminatedService();
                }

                return _this;
            }
        }

        /// <summary>
        /// Event invoked when app is terminated
        /// </summary>
        public event EventHandler Terminated;

        /// <summary>
        /// Invokes Terminated event
        /// </summary>
        public void Terminate()
        {
            Terminated?.Invoke(this, EventArgs.Empty);
        }
    }
}