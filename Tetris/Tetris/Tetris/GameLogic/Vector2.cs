namespace Tetris.GameLogic
{
    /// <summary>
    /// Vector representing two int coordinates
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Initalizes Vector2 struct instance
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}