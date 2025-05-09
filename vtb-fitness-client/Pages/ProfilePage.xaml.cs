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
using vtb_fitness_client.Network;
using vtb_fitness_client.Utility;
using vtb_fitness_client.Windows;

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private User _user;
        public ProfilePage(User user)
        {
            InitializeComponent();
            _user = user;
            InitView();
        }

        private async void InitView()
        {
            GetPfp();

            fullName_TextBlock.Text = $"{_user.Lastname} {_user.Name} {_user.Middlename}";
            role_TextBlock.Text = $"{(await ApiClient._Role.GetById(_user.RoleId)).Name}";
            registrationDate_TextBlock.Text = $"Дата регистрации: {_user.CreatedAt}";
            phone_TextBlock.Text = $"Номер телефона: +7{_user.Phone}";
            email_TextBlock.Text = $"Email: {_user.Email}";

            if (_user.Id == App.CurrentUser.Id)
            {
                changePfp_Button.Visibility = Visibility.Visible;
                fireMod_Button.Visibility = Visibility.Collapsed;
                fireTrainer_Button.Visibility = Visibility.Collapsed;
                deleteTariff_Button.Visibility = Visibility.Collapsed;
            }


            if (_user.RoleId == 4)
            {
                fireMod_Button.Visibility = Visibility.Collapsed;
                deleteTariff_Button.Visibility = Visibility.Collapsed;
            }

            if (_user.RoleId == 2)
            {
                fireTrainer_Button.Visibility = Visibility.Collapsed;
            }

            if (_user.RoleId == 3)
            {
                fireTrainer_Button.Visibility = Visibility.Collapsed;
                fireMod_Button.Visibility = Visibility.Collapsed;
            }
        }

        private void GetPfp()
        {
            pfp_Image.Source = ImageHelper.GetImage(_user.Pfp);
        }

        private void changePfp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteTariff_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fireMod_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fireTrainer_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void changeSpec_Button_Click(object sender, RoutedEventArgs e)
        {
            new ChangeSpecWindow().ShowDialog();
        }
    }
}
