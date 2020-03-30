using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tetris.GameLogic;
using Xamarin.Forms;

namespace Tetris.ViewModels
{
    /// <summary>
    /// Provides game abstraction
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        private const int TimeBeforeStartOfAnimation = 600;
        private const int LevelLabelLifeTime = 1300;

        private bool _progress1;
        private bool _progress2;
        private bool _progress3;
        private bool _gameOver;
        private bool _showCurtain;
        private int _level = 1;
        private string _leveltext;
        private List<Brick> _bricks;
        private List<Brick> _nextShape;
        private readonly Brick[,] _bricksTab;
        private readonly GridManager _gridManager;

        /// <summary>
        /// Number of grid columns
        /// </summary>
        public int ColumnCount { get; } = 10;

        /// <summary>
        /// Number of grid rows
        /// </summary>
        public int RowCount { get; } = 16;

        /// <summary>
        /// Indicates whether player lost the game
        /// </summary>
        public bool GameOver
        {
            get => _gameOver;
            set => SetProperty(ref _gameOver, value);
        }

        /// <summary>
        /// When set to true, it starts the curtain slide animation
        /// </summary>
        public bool ShowCurtain
        {
            get => _showCurtain;
            set => SetProperty(ref _showCurtain, value);
        }

        /// <summary>
        /// ItemsSource for grid view
        /// </summary>
        public List<Brick> Bricks
        {
            get => _bricks;
            set => SetProperty(ref _bricks, value);
        }

        /// <summary>
        /// Represents next shape to appear on screen
        /// </summary>
        public List<Brick> NextShape
        {
            get => _nextShape;
            set => SetProperty(ref _nextShape, value);
        }

        /// <summary>
        /// Indicates that one line was cleared
        /// </summary>
        public bool Progress1
        {
            get => _progress1;
            set => SetProperty(ref _progress1, value);
        }

        /// <summary>
        /// Indicates that two lines were cleared
        /// </summary>
        public bool Progress2
        {
            get => _progress2;
            set => SetProperty(ref _progress2, value);
        }

        /// <summary>
        /// Indicates that three lines were cleared
        /// </summary>
        public bool Progress3
        {
            get => _progress3;
            set => SetProperty(ref _progress3, value);
        }

        /// <summary>
        /// Game level
        /// </summary>
        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        /// <summary>
        /// Text for label representing level name on screen
        /// </summary>
        public string LevelText
        {
            get => _leveltext;
            set => SetProperty(ref _leveltext, value);
        }

        /// <summary>
        /// Action to take when screen is tapped
        /// </summary>
        public Command OnScreenTapCommand { get; }

        /// <summary>
        /// Action to take when play button is clicked
        /// </summary>
        public Command PlayCommand { get; }

        /// <summary>
        /// Initializes GameViewModel class instance
        /// </summary>
        public GameViewModel()
        {
            OnScreenTapCommand = new Command(ExecuteOnScreenTapCommand);
            PlayCommand = new Command(ExecutePlayCommand);
            _bricksTab = new Brick[ColumnCount, RowCount];
            _bricks = new List<Brick>();
            _nextShape = new List<Brick>();

            CreateBrickObjects();

            _gridManager = new GridManager(ColumnCount, RowCount, _bricksTab, NextShape);
            _gridManager.GameProgressChanged += GridManager_GameProgressChanged;

            ShowLevelLabelAndCurtain(1);
        }

        /// <summary>
        /// Creates brick objects for main game and next shape preview
        /// </summary>
        private void CreateBrickObjects()
        {
            for (int i = 0; i < 8; i++)
            {
                _nextShape.Add(new Brick(GridManager.EmptyColor));
            }

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    Brick brick = new Brick(GridManager.EmptyColor);
                    _bricksTab[j, i] = brick;
                    _bricks.Add(brick);
                }
            }
        }

        /// <summary>
        /// Action executed when game progress changes
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event arguments</param>
        private void GridManager_GameProgressChanged(object sender, GameProgressChangedEventArgs e)
        {
            if (e.Level > Level)
            {
                ShowLevelLabelAndCurtain(e.Level);
            }

            switch (e.Progress)
            {
                case double x when(x < 0.01):
                    Progress1 = Progress2 = Progress3 = false;
                    break;
                case double x when(x < 0.4):
                    Progress1 = true;
                    break;
                case double x when(x < 0.7):
                    Progress2 = true;
                    break;
                default:
                    Progress3 = true;
                    break;
            }

            Level = e.Level;
            // Player lost condition
            if (Level == 1 && Progress1 == false)
            {
                GameOver = true;
            }
        }

        /// <summary>
        /// Shows and hides level label and starts curtain slide animation
        /// </summary>
        /// <param name="level">Which level number to show on screen</param>
        public async void ShowLevelLabelAndCurtain(int level)
        {
            LevelText = $"level {level}";
            await Task.Delay(TimeSpan.FromMilliseconds(TimeBeforeStartOfAnimation));
            ShowCurtain = true;
            await Task.Delay(TimeSpan.FromMilliseconds(LevelLabelLifeTime));
            LevelText = "";
            ShowCurtain = false;
        }

        /// <summary>
        /// Action executed when screen is tapped
        /// </summary>
        public void ExecuteOnScreenTapCommand()
        {
            _gridManager.RotateShape();
        }

        /// <summary>
        /// Action executed when play button is clicked
        /// </summary>
        public void ExecutePlayCommand()
        {
            GameOver = false;
            ShowLevelLabelAndCurtain(1);
            _gridManager.StartNewGame();
        }

        /// <summary>
        /// Moves active shape
        /// </summary>
        /// <param name="right">Move direction</param>
        public void Rotate(bool right)
        {
            _gridManager.MoveShape(right ? Direction.Right : Direction.Left);
        }
    }
}