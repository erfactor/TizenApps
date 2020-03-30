using System;
using System.Collections.Generic;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Class representing shape on a 4x4 matrix, allowing to rotate it
    /// </summary>
    public class ShapeOnMatrix
    {
        private const int MaxTypes = 7;

        private readonly int _type;
        private bool[,] _matrix = new bool[4, 4];

        /// <summary>
        /// Initalizes ShapeOnMatrix class instance
        /// </summary>
        /// <param name="type">type of shape</param>
        public ShapeOnMatrix(int type)
        {
            _type = type;
            if (type >= MaxTypes)
            {
                throw new Exception($"type not valid, choose a value between 0 and {MaxTypes - 1}");
            }

            ResetShape();
        }

        /// <summary>
        /// Generates one of six possible shapes
        /// </summary>
        public void ResetShape()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _matrix[i, j] = false;
                }
            }

            switch (_type)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        _matrix[2, i] = true;
                    }

                    break;
                case 1:
                    for (int i = 1; i <= 2; i++)
                    {
                        for (int j = 1; j <= 2; j++)
                        {
                            _matrix[i, j] = true;
                        }
                    }

                    break;
                case 2:
                    _matrix[2, 0] = _matrix[1, 1] = _matrix[2, 1] = _matrix[1, 2] = true;
                    break;
                case 3:
                    _matrix[1, 0] = _matrix[1, 1] = _matrix[2, 1] = _matrix[2, 2] = true;
                    break;
                case 4:
                    _matrix[1, 0] = _matrix[1, 1] = _matrix[1, 2] = _matrix[2, 1] = true;
                    break;
                case 5:
                    _matrix[1, 1] = _matrix[2, 1] = _matrix[2, 2] = _matrix[2, 3] = true;
                    break;
                case 6:
                    _matrix[1, 1] = _matrix[1, 2] = _matrix[1, 3] = _matrix[2, 1] = true;
                    break;
            }
        }

        /// <summary>
        /// Returns cells representing currently active shape according to its shape and grid coordinates
        /// </summary>
        /// <param name="location">position of top-left corner of matrix on grid</param>
        /// <returns>Cells of currently active shape</returns>
        public Vector2[] GetShiftedCells(Vector2 location)
        {
            Vector2[] tab = new Vector2[4];
            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_matrix[i, j])
                    {
                        tab[k++] = new Vector2(location.X + i, location.Y + j);
                    }
                }
            }

            return tab;
        }

        /// <summary>
        /// Rotates shape in clockwise direction
        /// </summary>
        public void RotateRight()
        {
            bool[,] rotatedMatrix = new bool[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rotatedMatrix[3 - j, i] = _matrix[i, j];
                }
            }

            _matrix = rotatedMatrix;
        }

        /// <summary>
        /// Rotates shape in anti-clockwise direction
        /// </summary>
        public void RotateLeft()
        {
            bool[,] rotatedMatrix = new bool[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rotatedMatrix[j, 3 - i] = _matrix[i, j];
                }
            }

            _matrix = rotatedMatrix;
        }

        /// <summary>
        /// Returns list that represents shape in next turn
        /// </summary>
        /// <returns>List representing shape in next turn</returns>
        public List<bool> GetListForPreview()
        {
            List<bool> Result = new List<bool>();
            for (int j = 0; j < 4; j++)
            {
                for (int i = 1; i < 3; i++)
                {
                    Result.Add(_matrix[i, j]);
                }
            }

            return Result;
        }
    }
}