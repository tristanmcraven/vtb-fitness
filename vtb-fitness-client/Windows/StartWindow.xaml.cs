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
using vtb_fitness_client.Pages;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void signIn_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_Frame_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = main_Frame;
            PageManager.MainFrame.Navigate(new SignInPage());
        }
    }
}
