using Xamarin.Forms;
using Calendar.Interfaces;
using Calendar.Tizen.TV.Components;
using Calendar.Tizen.TV.Views;

[assembly: Dependency(typeof(CalendarPage))]
namespace Calendar.Tizen.TV.Views
{
    /// <summary>
    /// CalendarPage class.
    /// Main application view.
    /// </summary>
    public partial class CalendarPage : IAppPage
    {
        #region methods

        /// <summary>
        /// Application main page constructor.
        /// Sets binding context and adds Calendar component.
        /// </summary>
        public CalendarPage()
        {
            InitializeComponent();

            BindingContext = ViewModels.ViewModelLocator.ViewModel;
            CalendarWrapper.Children.Add(new CalendarControl());
        }

        #endregion
    }
}