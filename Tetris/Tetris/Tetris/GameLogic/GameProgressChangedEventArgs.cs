using System;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Represents arguments of event triggered by game progress change
    /// </summary>
    public class GameProgressChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Level that player is on
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Progress on level
        /// </summary>
        public double Progress { get; private set; }

        /// <summary>
        /// Initalizes GameProgressChangedEventArgs class instance
        /// </summary>
        /// <param name="level">Level that player is on</param>
        /// <param name="progress">Progress on level</param>
        public GameProgressChangedEventArgs(int level, double progress)
        {
            Level = level;
            Progress = progress;
        }
    }
}
