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
using vtb_fitness_client.UserControls;
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

            DisplayGenericData();

            AdjustRole();

            DisplayCurrentTariff();

            DisplayCurrentTrainer();
        }

        private void AdjustRole()
        {
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

        private async void DisplayGenericData()
        {
            fullName_TextBlock.Text = $"{_user.Lastname} {_user.Name} {_user.Middlename}";
            role_TextBlock.Text = $"{(await ApiClient._Role.GetById(_user.RoleId)).Name}";
            registrationDate_TextBlock.Text = $"Дата регистрации: {_user.CreatedAt}";
            phone_TextBlock.Text = $"Номер телефона: +7{_user.Phone}";
            email_TextBlock.Text = $"Email: {_user.Email}";
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

        private async void chooseTrainer_Button_Click(object sender, RoutedEventArgs e)
        {
            var userTariff = await ApiClient._User.GetCurrentTariff(App.CurrentUser.Id);
            if (userTariff == null)
            {
                new DialogWindow(DialogWindowType.Error, "Вы должны купить абонемент перед тем, как записаться к тренеру.").ShowDialog();
                return;
            }

            if (userTariff.Tariff.TrainerWorkoutsPerWeek == null || userTariff.Tariff.TrainerWorkoutsPerWeek < 1)
            {
                new DialogWindow(DialogWindowType.Error, "В ваш абонемент не входят занятия с тренером.").ShowDialog();
                return;
            }

            var dw = new DialogWindow(DialogWindowType.Confirmation, $"Вы уверены, что хотите записаться" +
                $" к тренеру {_user.Lastname} {_user.Name} {_user.Middlename}?");
            dw.ShowDialog();

            if (dw.DialogResult == true)
            {
                var result = await ApiClient._User.AssignTrainer(App.CurrentUser.Id, _user.Id);
                if (result == null) new DialogWindow().ShowDialog();
                PageManager.MainFrame.Navigate(new ProfilePage(_user));
            }
        }

        private async void DisplayCurrentTariff()
        {
            var userTariff = await ApiClient._User.GetCurrentTariff(_user.Id);

            if (userTariff == null)
            {
                noTariff_TextBlock.Visibility = Visibility.Visible;
                currentTariff_Grid.Visibility = Visibility.Collapsed;
            }

            else
            {
                var tariff = userTariff.Tariff!;

                noTariff_TextBlock.Visibility = Visibility.Collapsed;
                currentTariff_Grid.Visibility = Visibility.Visible;

                currentTariff_Grid.Children.Add(new TariffUserControl(tariff, userTariff));
            }
        }

        private async void DisplayCurrentTrainer()
        {
            var trainer = await ApiClient._User.GetTrainer(_user.Id);

            if (trainer == null)
            {
                noTrainer_TextBlock.Visibility = Visibility.Visible;
                currentTrainer_Grid.Visibility= Visibility.Collapsed;
            }

            else
            {
                noTrainer_TextBlock.Visibility = Visibility.Collapsed;
                currentTrainer_Grid.Visibility = Visibility.Visible;

                currentTrainer_Grid.Children.Add(new UserUserControl(trainer));
            }
        }
    }
}
