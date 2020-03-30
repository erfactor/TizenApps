using System;
using Tizen.Sensor;

namespace Pedometer.Services
{
    /// <summary>
    /// Class representing service that notifies observers about changes in pedometer sensor data
    /// </summary>
    public class PedometerService
    {
        private Tizen.Sensor.Pedometer pedometer;
        private static PedometerService _this;

        /// <summary>
        /// Event connected with pedometer data change
        /// </summary>
        public event EventHandler<PedometerUpdatedEventArgs> PedometerUpdated;

        /// <summary>
        /// Provides singleton instance of PedometerService class
        /// </summary>
        public static PedometerService Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new PedometerService();
                }

                return _this;
            }
        }

        /// <summary>
        /// Initalizes PedometerService class instance
        /// </summary>
        private PedometerService()
        {
            pedometer = new Tizen.Sensor.Pedometer
            {
                Interval = 50,
            };

            pedometer.DataUpdated += Pedometer_DataUpdated;
            pedometer.Start();
            PedometerUpdated?.Invoke(this, new PedometerUpdatedEventArgs((int)pedometer.CalorieBurned, (int)pedometer.StepCount, (int)pedometer.LastSpeed, (int)pedometer.MovingDistance));

            var appTerminatedService = AppTerminatedService.Instance;
            appTerminatedService.Terminated += AppTerminatedService_Terminated;
        }

        /// <summary>
        /// Handles execution of Terminated event
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event Args</param>
        private void AppTerminatedService_Terminated(object sender, EventArgs e)
        {
            pedometer.Stop();
            pedometer.DataUpdated -= Pedometer_DataUpdated;
            pedometer.Dispose();
        }

        /// <summary>
        /// Handles execution of DataUpdated event
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event Args</param>
        private void Pedometer_DataUpdated(object sender, PedometerDataUpdatedEventArgs e)
        {
            PedometerUpdated?.Invoke(this, new PedometerUpdatedEventArgs((int)e.CalorieBurned, (int)e.StepCount, (int)e.LastSpeed, (int)e.MovingDistance));
        }
    }
}