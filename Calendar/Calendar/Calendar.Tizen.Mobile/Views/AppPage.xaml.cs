using Calendar.Tizen.Mobile.Components;
using Calendar.Tizen.Mobile.Views;
using Xamarin.Forms;
using Calendar.Interfaces;

[assembly: Dependency(typeof(CalendarPage))]
namespace Calendar.Tizen.Mobile.Views
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