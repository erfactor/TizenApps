using Calendar.Tizen.Mobile.Components;
using Calendar.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using ECalendar = ElmSharp.Calendar;

[assembly: ExportRenderer(typeof(CalendarView), typeof(CalendarViewRenderer))]

namespace Calendar.Tizen.Mobile.Renderers
{
    /// <summary>
    /// CalendarView component's renderer class.
    /// </summary>
    public class CalendarViewRenderer : ViewRenderer<CalendarView, ECalendar>
    {
        #region methods

        /// <summary>
        /// Constructor.
        /// Registers properties handlers.
        /// </summary>
        public CalendarViewRenderer()
        {
            RegisterPropertyHandler(CalendarView.MinimumDateProperty, UpdateMinimumDate);
            RegisterPropertyHandler(CalendarView.MaximumDateProperty, UpdateMaximumDate);
            RegisterPropertyHandler(CalendarView.FirstDayOfWeekProperty, UpdateFirstDayOfWeek);
            RegisterPropertyHandler(CalendarView.WeekDayNamesProperty, UpdateWeekDayNames);
            RegisterPropertyHandler(CalendarView.SelectedDateProperty, UpdateSelectedDate);
        }

        /// <summary>
        /// Handles control's changes.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CalendarView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ECalendar(TForms.Context.MainWindow));
            }

            if (e.OldElement != null)
            {
                Control.DateChanged -= DateChangedHandler;
            }

            if (e.NewElement != null)
            {
                Control.DateChanged += DateChangedHandler;
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Sets new date for calendar model.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Event arguments.</param>
        void DateChangedHandler(object sender, ElmSharp.DateChangedEventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(CalendarView.SelectedDateProperty, e.NewDate);
        }

        /// <summary>
        /// Sets component's minimum date.
        /// </summary>
        void UpdateMinimumDate()
        {
            Control.MinimumYear = Element.MinimumDate.Year;
        }

        /// <summary>
        /// Sets component's maximum date.
        /// </summary>
        void UpdateMaximumDate()
        {
            Control.MaximumYear = Element.MaximumDate.Year;
        }

        /// <summary>
        /// Sets component's first day of week.
        /// </summary>
        void UpdateFirstDayOfWeek()
        {
            Control.FirstDayOfWeek = Element.FirstDayOfWeek;
        }

        /// <summary>
        /// Sets component's day names.
        /// </summary>
        void UpdateWeekDayNames()
        {
            Control.WeekDayNames = Element.WeekDayNames;
        }

        /// <summary>
        /// Sets component's date.
        /// </summary>
        void UpdateSelectedDate()
        {
            Control.SelectedDate = Element.SelectedDate;
        }

        #endregion
    }
}
