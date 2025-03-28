using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private Window _senderWindow;
        public new bool DialogResult = false;
        public DialogWindow(Window senderWindow, string title, string message, DialogWindowButtons buttons, DialogWindowType type)
        {
            _senderWindow = senderWindow;
            InitializeComponent();
            InitView(title, message, buttons, type);
        }

        private void InitView(string title, string message, DialogWindowButtons buttons, DialogWindowType type)
        {
            AddTintToSenderWindow();

            if (type == DialogWindowType.Error)
                SystemSounds.Hand.Play();
            else if (type == DialogWindowType.Warning)
                SystemSounds.Exclamation.Play();

            title_TextBlock.Text = title;
            message_TextBlock.Text = message;

            if (buttons == DialogWindowButtons.Ok)
            {
                ok_Button.Visibility = Visibility.Visible;
                yes_Button.Visibility = Visibility.Collapsed;
                no_Button.Visibility = Visibility.Collapsed;
            }
            else if (buttons == DialogWindowButtons.YesNo) {
                ok_Button.Visibility = Visibility.Collapsed;
                yes_Button.Visibility = Visibility.Visible;
                no_Button.Visibility = Visibility.Visible;
            }
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
                tint.Background.Opacity = 0.5;
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

        private void ok_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void yes_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void no_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
