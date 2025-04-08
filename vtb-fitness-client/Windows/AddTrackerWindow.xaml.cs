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
using System.Windows.Shapes;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for AddTrackerWindow.xaml
    /// </summary>
    public partial class AddTrackerWindow : Window
    {
        private Window _senderWindow;
        public AddTrackerWindow(Window senderWindow)
        {
            InitializeComponent();
            _senderWindow = senderWindow;
            InitView();
        }

        private void InitView()
        {
            AddTintToSenderWindow();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveTintFromSenderWindow();
        }

        private void AddTintToSenderWindow()
        {
            var body = _senderWindow.FindName("body") as Grid;

            if (body != null)
            {
                var tint = new Border
                {
                    Background = new SolidColorBrush(Colors.Black),
                    Name = "tint"
                };
                tint.Background.Opacity = 0.8;
                Grid.SetColumnSpan(body, 10);
                Grid.SetRowSpan(body, 10);
                Panel.SetZIndex(body, 2);

                body.Children.Add(tint);
            }
        }

        private void RemoveTintFromSenderWindow()
        {
            var body = _senderWindow.FindName("body") as Grid;

            if (body != null)
            {
                var tint = body.Children.OfType<Border>().FirstOrDefault(b => b.Name == "tint");
                if (tint != null)
                    body.Children.Remove(tint);
            }
        }

        private void addTracker_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
