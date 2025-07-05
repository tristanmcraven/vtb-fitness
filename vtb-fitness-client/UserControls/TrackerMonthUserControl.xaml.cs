using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vtb_fitness_client.Model;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TrackerMonthUserControl.xaml
    /// </summary>
    public partial class TrackerMonthUserControl : UserControl
    {
        private string _month;
        private List<Tracker> _tracker;
        public TrackerMonthUserControl(string month, List<Tracker> tracker)
        {
            InitializeComponent();
            _month = month;
            _tracker = tracker;
            InitView();
        }

        private void InitView()
        {
            month_TextBlock.Text = _month;

            var trackerByDays = _tracker
                .GroupBy(x => x.Timestamp.Date)
                .OrderBy(x => x.Key);

            foreach (var day in trackerByDays)
            {
                days_WrapPanel.Children.Add(new TrackerDayUserControl(day.ToList()));
            }

            if (_tracker.First().Timestamp.Month == DateTime.Now.Month)
                days_WrapPanel.Children.Add(new AddTrackerUserControl());
        }
    }
}
