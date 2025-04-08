using System;
using System.Collections.Generic;
using System.Globalization;
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
using vtb_fitness_client.Network;
using vtb_fitness_client.UserControls;

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for TrackerPage.xaml
    /// </summary>
    public partial class TrackerPage : Page
    {
        private List<Tracker> _tracker; 
        public TrackerPage()
        {
            InitializeComponent();
            InitView();
        }

        private async void InitView()
        {
            _tracker = await ApiClient._User.GetTracker(App.CurrentUser.Id);

            var trackerByMonth = _tracker
                .GroupBy(x => new { x.Timestamp.Year, x.Timestamp.Month })
                .OrderBy(x => x.Key.Year)
                .ThenBy(x => x.Key.Month);

            foreach (var month in trackerByMonth)
            {
                var tmuc = new TrackerMonthUserControl($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Key.Month)}", month.ToList());
                tracker_stackPanel.Children.Add(tmuc);
            }

            if (!trackerByMonth.Any())
            {
                tracker_stackPanel.Children.Add(new TrackerMonthUserControl($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)}", new List<Tracker>()));
            }
        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addTracker_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
