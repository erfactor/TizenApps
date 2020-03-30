using System.Collections;
using Xamarin.Forms;

namespace Tetris.Tizen.Wearable.CustomLayouts
{
    /// <summary>
    /// Layout that arranges items into a grid, according to number of rows and columns specified
    /// </summary>
    public class GridView : Grid
    {
        /// <summary>
        /// Allows to set column count
        /// </summary>
        public static readonly BindableProperty ColumnCountProperty = BindableProperty.Create(
            nameof(ColumnCount),
            typeof(int),
            typeof(GridView),
            null,
            propertyChanged: OnGridChanged);

        /// <summary>
        /// Gets or sets column count
        /// </summary>
        public int ColumnCount
        {
            get => (int)GetValue(ColumnCountProperty);
            set => SetValue(ColumnCountProperty, value);
        }

        /// <summary>
        /// Allows to set row count
        /// </summary>
        public static readonly BindableProperty RowCountProperty = BindableProperty.Create(
            nameof(RowCount),
            typeof(int),
            typeof(GridView),
            null,
            propertyChanged: OnGridChanged);

        /// <summary>
        /// Gets or sets row count
        /// </summary>
        public int RowCount
        {
            get => (int)GetValue(RowCountProperty);
            set => SetValue(RowCountProperty, value);
        }

        /// <summary>
        /// Allows to set source of items
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(GridView),
            null,
            propertyChanged: OnGridChanged);

        /// <summary>
        /// Gets or sets source of items
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Allows to set data template for item
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(GridView),
            null);

        /// <summary>
        /// Gets or sets data template for item
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Executed when grid columncount, rowcount or itemssource changes
        /// </summary>
        /// <param name="bindable">Object that has bindable property which changed its value</param>
        /// <param name="oldvalue">Old value of property</param>
        /// <param name="newvalue">New value of property</param>
        private static void OnGridChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            GridView gridView = bindable as GridView;
            gridView?.ArrangeItemsIntoGrid();
        }

        /// <summary>
        /// Arranges ItemsSource into grid according to RowCount and ColumnCount
        /// </summary>
        private void ArrangeItemsIntoGrid()
        {
            if (RowCount <= 0 || ColumnCount <= 0 || ItemsSource == null)
            {
                return;
            }

            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
            Children.Clear();

            for (int i = 0; i < ColumnCount; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < RowCount; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            int x = 0;
            int y = 0;
            foreach (var item in ItemsSource)
            {
                ViewCell viewCell = ItemTemplate.CreateContent() as ViewCell;
                if (viewCell == null)
                {
                    return;
                }

                View cell = viewCell.View;
                cell.BindingContext = item;
                Children.Add(cell, x, y);
                if (++x == ColumnCount)
                {
                    x = 0;
                    y++;
                    if (y == RowCount)
                    {
                        return;
                    }
                }

            }
        }
    }
}
