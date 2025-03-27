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
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void photo_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void role_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
        }
    }
}
