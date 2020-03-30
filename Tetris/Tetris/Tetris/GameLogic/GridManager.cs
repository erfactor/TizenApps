using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tetris.Views;
using Xamarin.Forms;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Class used to perform game logic
    /// </summary>
    public class GridManager
    {
        private const int DefaultTime = 500;
        private const int MaxScore = 3;
        private const int TimeBeforeGameStart = 4000;
        private static readonly Color SparkColor = Color.FromHex("#CBFFEE");
        private static readonly Random random = new Random();
        private static readonly Color EmptyCellColor = Color.Transparent;

        private readonly int _width;
        private readonly int _height;
        private readonly Brick[,] _bricks;
        private readonly List<Brick> _nextShape;
        private readonly Brick _propBrick = new Brick(EmptyCellColor);

        private NonRepeatableGenerator<Color> _colorGenerator;
        private NonRepeatableGenerator<ShapeOnMatrix> _shapeGenerator;
        private Shape _shape = new Shape();
        private Vector2[] _inAccessibleBlocks;
        private ShapeOnMatrix _futureShapeOnMatrix;
        private Color _futureColor;
        private bool _isGameRunning = true;
        private bool _newShapeNeeded = true;
        private int _level = 1;
        private int _currentscore = 0;
        private TimeSpan _timeBetweenFrames = TimeSpan.FromMilliseconds(DefaultTime);
        private DateTime _lastMoveTime = DateTime.Now;
        private DateTime _currentMoveTime;
        private TimeSpan _additionalTimeToSleep;

        /// <summary>
        /// Progress of level represented as a value between 0 and 1
        /// </summary>
        public double LevelProgress => (_currentscore / (double)MaxScore);

        /// <summary>
        /// Represents color of an empty cell
        /// </summary>
        public static Color EmptyColor => EmptyCellColor;

        /// <summary>
        /// Event invoked when game progress changes
        /// </summary>
        public event EventHandler<GameProgressChangedEventArgs> GameProgressChanged;

        /// <summary>
        /// Initializes new GridManager instance
        /// </summary>
        /// <param name="width">Number of columns of board</param>
        /// <param name="height">Number of rows of board</param>
        /// <param name="bricks">2D-table containing bricks</param>
        /// <param name="nextShape">List of bricks that represent shape that will show in next turn</param>
        public GridManager(int width, int height, Brick[,] bricks, List<Brick> nextShape)
        {
            _width = width;
            _height = height;
            _bricks = bricks;
            _nextShape = nextShape;

            SetupColorGenerator();
            SetupShapeGenerator();
            SetupFutureShape();
            SetupInaccessibleBlocks();

            var appStateService = DependencyService.Get<IAppStateService>();
            appStateService.Paused += AppStateManager_Paused;
            appStateService.Resumed += AppStateManager_Resumed;

            _additionalTimeToSleep = TimeSpan.FromMilliseconds(TimeBeforeGameStart);
            Task.Run(() => GameLoop());
        }

        /// <summary>
        /// Sets up blocks that cannot be accessed
        /// </summary>
        private void SetupInaccessibleBlocks()
        {
            _inAccessibleBlocks = new Vector2[]
                {
                 new Vector2(0, _height - 1),
                 new Vector2(1, _height - 1),
                 new Vector2(_width - 2, _height - 1),
                 new Vector2(_width - 1, _height - 1)
                };
            _inAccessibleBlocks.ForEach(vec => _bricks[vec.X, vec.Y].Color = Color.Transparent);
        }

        /// <summary>
        /// Main game loop, updates what is visible on screen
        /// </summary>
        private void GameLoop()
        {
            while (true)
            {
                Thread.Sleep(_timeBetweenFrames);

                if (_isGameRunning)
                {
                    Thread.Sleep(_additionalTimeToSleep);
                    _additionalTimeToSleep = TimeSpan.Zero;
                    Update();
                }
            }
        }

        /// <summary>
        /// Event handler executed when app is resumed
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event Arguments</param>
        private void AppStateManager_Resumed(object sender, EventArgs e)
        {
            _isGameRunning = true;
        }

        /// <summary>
        /// Event handler executed when app is paused
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event Arguments</param>
        private void AppStateManager_Paused(object sender, EventArgs e)
        {
            _isGameRunning = false;
        }

        /// <summary>
        /// Sets up future shape to appear on screen
        /// </summary>
        public void SetupFutureShape()
        {
            _futureShapeOnMatrix = _shapeGenerator.GetNextElement();
            _futureColor = _colorGenerator.GetNextElement();
        }

        /// <summary>
        /// Rotates shape
        /// </summary>
        /// <param name="right">Bool indicating whether to rotate clockwise or anti-clockwise</param>
        public void RotateShape(bool right = true)
        {
            if (_newShapeNeeded)
            {
                return;
            }

            if (right)
            {
                _shape.ShapeOnMatrix.RotateRight();
            }
            else
            {
                _shape.ShapeOnMatrix.RotateLeft();
            }

            var newplace = GetPlaceForShapeAfterRotation();

            if (newplace == null)
            {
                if (!right)
                {
                    _shape.ShapeOnMatrix.RotateRight();
                }
                else
                {
                    _shape.ShapeOnMatrix.RotateLeft();
                }

                return;
            }

            BlackenShape();
            _shape.ActiveShape = newplace;
            ColorShape();
        }

        /// <summary>
        /// Starts new game after player loss
        /// </summary>
        public void StartNewGame()
        {
            _additionalTimeToSleep = TimeSpan.FromMilliseconds(TimeBeforeGameStart);
            ResetScreen();
            _isGameRunning = true;
        }

        /// <summary>
        /// Checks whether after rotation there will be any collision with other blocks
        /// </summary>
        /// <returns>If rotation is possible returns new place for shape, otherwise returns null</returns>
        private Vector2[] GetPlaceForShapeAfterRotation()
        {
            var newplace = _shape.GetChangedCells();
            if (newplace.Any(vec => vec.Y >= _height) || !IsPlaceFree(newplace))
            {
                return null;
            }

            int minleft = newplace.Min(vec => vec.X);
            if (minleft < 0)
            {
                newplace = newplace.Select(vec => new Vector2(vec.X - minleft, vec.Y)).ToArray();
                if (IsPlaceFree(newplace))
                {
                    _shape.Location = new Vector2(_shape.Location.X - minleft, _shape.Location.Y);
                    return _shape.GetChangedCells();
                }
                else
                {
                    return null;
                }
            }

            int maxright = newplace.Max(vec => vec.X);
            if (maxright > _width - 1)
            {
                int shift = maxright - (_width - 1);
                newplace = newplace.Select(vec => new Vector2(vec.X - shift, vec.Y)).ToArray();
                if (IsPlaceFree(newplace))
                {
                    _shape.Location = new Vector2(_shape.Location.X - shift, _shape.Location.Y);
                    return _shape.GetChangedCells();
                }

                return null;
            }

            return _shape.GetChangedCells();
        }

        /// <summary>
        /// Checks whether specified cells are free to occupy
        /// </summary>
        /// <param name="cells">Cells to check if they are free</param>
        /// <returns>Bool indicating if cells are free to occupy</returns>
        private bool IsPlaceFree(Vector2[] cells)
        {
            if (cells.Intersect(_inAccessibleBlocks).Any())
            {
                return false;
            }

            int x, y;

            for (int i = 0; i < cells.Length; i++)
            {
                x = cells[i].X;
                y = cells[i].Y;

                if (y < 0)
                {
                    return false;
                }

                if (x < 0 || x >= _width || y >= _height || _shape.ActiveShape.Contains(new Vector2(x, y)))
                {
                    continue;
                }

                if (GetBrickAtPosition(x, y).Color != EmptyCellColor)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Main game function, performs game logic
        /// </summary>
        public void Update()
        {
            if (_currentscore >= MaxScore)
            {
                _timeBetweenFrames = _level >= 4 ?
                    _timeBetweenFrames.Add(TimeSpan.FromMilliseconds(-35))
                    :
                    _timeBetweenFrames.Add(TimeSpan.FromMilliseconds(-100));

                _currentscore = 0;
                _level += 1;
                GameProgressChanged?.Invoke(this, new GameProgressChangedEventArgs(_level, LevelProgress));
                Thread.Sleep(TimeSpan.FromMilliseconds((int)(TimeBeforeGameStart * 0.52)));
                ResetScreen();
                Thread.Sleep(TimeSpan.FromMilliseconds((int)(TimeBeforeGameStart * 0.37)));
                return;
            }

            if (_newShapeNeeded)
            {
                _shape.Location = new Vector2(3, 0);
                _shape.ShapeOnMatrix = _futureShapeOnMatrix;
                _shape.Color = _futureColor;
                _shape.UpdateActiveShape();

                if (_shape.ActiveShape.Any(vec => GetBrickAtPosition(vec.X, vec.Y).Color != EmptyCellColor))
                {
                    _isGameRunning = false;
                    _level = 1;
                    _currentscore = 0;
                    GameProgressChanged?.Invoke(this, new GameProgressChangedEventArgs(_level, LevelProgress));
                    _timeBetweenFrames = TimeSpan.FromMilliseconds(DefaultTime);
                    return;
                }

                ColorShape();
                SetupFutureShape();
                _futureShapeOnMatrix.ResetShape();
                var preview = _futureShapeOnMatrix.GetListForPreview();

                for (int i = 0; i < preview.Count; i++)
                {
                    _nextShape[i].Color = preview[i] ? _futureColor : EmptyCellColor;
                }

                _newShapeNeeded = false;
                return;
            }

            MoveShape(Direction.Bottom);
        }

        /// <summary>
        /// Moves shape in specified direction
        /// </summary>
        /// <param name="direction">Direction in which to move shape</param>
        public void MoveShape(Direction direction)
        {
            if (_newShapeNeeded)
            {
                return;
            }

            _currentMoveTime = DateTime.Now;
            var span = _currentMoveTime - _lastMoveTime;
            _lastMoveTime = _currentMoveTime;
            if (span.TotalMilliseconds <= 4)
            {
                return;
            }

            if (CheckForCollision(direction))
            {
                return;
            }

            BlackenShape();
            switch (direction)
            {
                case Direction.Left:
                    _shape.Location = new Vector2(_shape.Location.X - 1, _shape.Location.Y);
                    break;

                case Direction.Right:
                    _shape.Location = new Vector2(_shape.Location.X + 1, _shape.Location.Y);
                    break;

                case Direction.Bottom:
                    _shape.Location = new Vector2(_shape.Location.X, _shape.Location.Y + 1);
                    break;
            }

            _shape.UpdateActiveShape();
            ColorShape();
        }

        /// <summary>
        /// Checks whether move in specifed direction will trigger any collisions
        /// </summary>
        /// <param name="direction">Move direction</param>
        /// <returns>Bool indicating whether move is possible</returns>
        private bool CheckForCollision(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (_shape.ActiveShape.Any(vec => vec.X == 0)
                        || (!IsPlaceFree(_shape.ActiveShape.Select(vec => new Vector2(vec.X - 1, vec.Y)).ToArray())))
                    {
                        return true;
                    }

                    break;

                case Direction.Right:
                    if (_shape.ActiveShape.Any(vec => vec.X == _width - 1)
                        || (!IsPlaceFree(_shape.ActiveShape.Select(vec => new Vector2(vec.X + 1, vec.Y)).ToArray())))
                    {
                        return true;
                    }

                    break;

                case Direction.Bottom:
                    if (_shape.ActiveShape.Any(vec => vec.Y == _height - 1
                        || ((vec.X < 2 || vec.X >= _width - 2) && vec.Y == _height - 2))
                        || (!IsPlaceFree(_shape.ActiveShape.Select(vec => new Vector2(vec.X, vec.Y + 1)).ToArray())))
                    {
                        var heights = _shape.ActiveShape.Select(vec => vec.Y).Distinct();

                        foreach (int y in heights)
                        {
                            CheckClearGridRow(y);
                        }

                        _newShapeNeeded = true;
                        return true;
                    }

                    break;
            }

            return false;
        }

        /// <summary>
        /// Performs row clear if it is needed
        /// </summary>
        /// <param name="y">Row to check</param>
        private void CheckClearGridRow(int y)
        {
            if (_currentscore >= 3)
            {
                return;
            }

            int start = y == _height - 1 ? 2 : 0;
            int end = y == _height - 1 ? _width - 2 : _width;

            for (int i = start; i < end; i++)
            {
                if (GetBrickAtPosition(i, y).Color == EmptyCellColor)
                {
                    return;
                }
            }

            int half = _width / 2;
            int numOfColorsInGradient = 10;
            Color[][] colorTab = new Color[_width][];

            for (int i = start; i < end; i++)
            {
                colorTab[i] = GenerateGradient(GetBrickAtPosition(i,y).Color, SparkColor, numOfColorsInGradient - 2);
            }

            for (int j = 0; j < numOfColorsInGradient; j++)
            {
                for (int i = start; i < end; i++)
                {
                    GetBrickAtPosition(i, y).Color = colorTab[i][j];
                }

                Thread.Sleep(50);
            }

            for (int i = start; i < end; i++)
            {
                colorTab[i] = GenerateGradient(SparkColor, EmptyCellColor, numOfColorsInGradient - 2);
            }

            for (int j = 0; j < numOfColorsInGradient; j++)
            {
                for (int i = start; i < end; i++)
                {
                    GetBrickAtPosition(i, y).Color = colorTab[i][j];
                }

                Thread.Sleep(35);
            }

            for (int j = y; j > 0; j--)
            {
                for (int i = start; i < end; i++)
                {
                    GetBrickAtPosition(i, j).Color = GetBrickAtPosition(i, j - 1).Color;
                }
            }

            _currentscore += 1;
            GameProgressChanged?.Invoke(this, new GameProgressChangedEventArgs(_level, LevelProgress));

            if (y == _height - 1)
            {
                CheckClearGridRow(_height - 1);
            }
        }

        /// <summary>
        /// Makes cells occupied by active shape black
        /// </summary>
        private void BlackenShape()
        {
            foreach (var vec in _shape.ActiveShape)
            {
                GetBrickAtPosition(vec.X, vec.Y).Color = EmptyCellColor;
            }
        }

        /// <summary>
        /// Colours cells occupied by active shape
        /// </summary>
        private void ColorShape()
        {
            foreach (var vec in _shape.ActiveShape)
            {
                GetBrickAtPosition(vec.X, vec.Y).Color = _shape.Color;
            }
        }

        /// <summary>
        /// Gets Brick at the specified row and column of a grid
        /// </summary>
        /// <param name="x">Column of BoxView</param>
        /// <param name="y">Row of BoxView</param>
        /// <returns>Brick at the specified row and column</returns>
        public Brick GetBrickAtPosition(int x, int y)
        {
            return (y == _height - 1 && (x < 2 || x >= _width - 2)) ? _propBrick : _bricks[x, y];
        }

        /// <summary>
        /// Makes all cells black
        /// </summary>
        public void ResetScreen()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    GetBrickAtPosition(i, j).Color = EmptyCellColor;
                }
            }
        }

        /// <summary>
        /// Sets up ColorGenerator
        /// </summary>
        private void SetupColorGenerator()
        {
            var Colors = new Color[]
            {
                Color.FromHex("#9b9300"),
                Color.FromHex("#ffa835"),
                Color.FromHex("#fe7235"),
                Color.FromHex("#01c3ff"),
                Color.FromHex("#0077ff"),
            };
            _colorGenerator = new NonRepeatableGenerator<Color>(Colors, 3);
        }

        /// <summary>
        /// Sets up ShapeGenerator
        /// </summary>
        private void SetupShapeGenerator()
        {
            var Shapes = new List<ShapeOnMatrix>();

            for (int i = 0; i < 7; i++)
            {
                Shapes.Add(new ShapeOnMatrix(i));
            }

            _shapeGenerator = new NonRepeatableGenerator<ShapeOnMatrix>(Shapes.ToArray(), 5);
        }

        /// <summary>
        /// Generates gradient between two colors
        /// </summary>
        /// <param name="first">First color of gradient</param>
        /// <param name="second">Second color of gradient</param>
        /// <param name="steps">Number of colors between first and second</param>
        /// <returns>Table of gradient colors</returns>
        public Color[] GenerateGradient(Color first, Color second, int steps)
        {
            double R_Start = first.R;
            double G_Start = first.G;
            double B_Start = first.B;
            double A_Start = first.A;
            double R_Span = second.R - first.R;
            double G_Span = second.G - first.G;
            double B_Span = second.B - first.B;
            double A_Span = second.A - first.A;
            int full = steps + 1;
            Color[] Result = new Color[steps + 2];
            Result[0] = first;
            Result[steps + 1] = second;

            for (int i = 1; i < steps + 1; i++)
            {
                Result[i] = Color.FromRgba(R_Start + ((double)i / full) * R_Span,
                                           G_Start + ((double)i / full) * G_Span,
                                           B_Start + ((double)i / full) * B_Span,
                                           A_Start + ((double)i / full) * A_Span);
            }

            return Result;
        }
    }
}