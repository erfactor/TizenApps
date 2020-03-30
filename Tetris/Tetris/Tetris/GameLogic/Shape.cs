using Xamarin.Forms;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Represents the shape localization on grid
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// Represents location of top-left corner of shape matrix on grid
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// Represents location of shape cells on 4x4 grid
        /// </summary>
        public ShapeOnMatrix ShapeOnMatrix { get; set; }

        /// <summary>
        /// Represents cells that shape occupies on grid
        /// </summary>
        public Vector2[] ActiveShape { get; set; }

        /// <summary>
        /// Represents shape's color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Initializes Shape class instance
        /// </summary>
        public Shape()
        {
            Location = new Vector2(0,0);
            ShapeOnMatrix = null;
            ActiveShape = null;
            Color = Color.Default;
        }

        /// <summary>
        /// Updates active shape
        /// </summary>
        public void UpdateActiveShape()
        {
            ActiveShape = ShapeOnMatrix.GetShiftedCells(Location);
        }

        /// <summary>
        /// Gets cells that are not committed yet by UpdateActiveShape
        /// </summary>
        /// <returns>Cells of changed, but not updated shape</returns>
        public Vector2[] GetChangedCells()
        {
            return ShapeOnMatrix.GetShiftedCells(Location);
        }
    }
}
