using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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
using vtb_fitness_client.Network;
using vtb_fitness_client.Pages;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitView();
        }

        private void InitView()
        {
            var user = App.CurrentUser;
            fullname_TextBlock.Text = $"{user.Lastname} {user.Name} {user.Middlename}";
            role_TextBlock.Text = App.CurrentUser.RoleId switch
            {
                1 => "Администратор",
                2 => "Модератор",
                3 => "Сотрудник ВТБ",
                4 => "Тренер",
                _ => "Неизвестный"
            };

            if (user.RoleId == 3)
            {
                users_TextBlock.Visibility = Visibility.Collapsed;
                trainers_TextBlock.Visibility = Visibility.Collapsed;
                mods_TextBlock.Visibility = Visibility.Collapsed;
            }

            UpdateCurrentTariffData();
        }

        public async void UpdateCurrentTariffData()
        {
            var userTariff = await ApiClient._User.GetCurrentTariff(App.CurrentUser.Id);
            if (userTariff == null)
            {
                currentTariff_TextBlock.Text = "Нет";
                currentTariff_TextBlock.Background = new SolidColorBrush(Colors.Red);

                tariffStatuses_StackPanel.Visibility = Visibility.Collapsed;
                noTariff_TextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                var tariff = userTariff.Tariff;

                currentTariff_TextBlock.Text = tariff.Name + $" {Helper.GetTariffDuration(tariff)}";
                currentTariff_TextBlock.ClearValue(TextBlock.BackgroundProperty);

                gymStatus_TextBlock.Text = Helper.GetFacilityStatus(tariff.GymStartTime, tariff.GymEndTime, gymStatus_TextBlock);
                poolStatus_TextBlock.Text = Helper.GetFacilityStatus(tariff.PoolStartTime, tariff.PoolEndTime, poolStatus_TextBlock);
                hammamStatus_TextBlock.Text = Helper.GetFacilityStatus(tariff.HammamStartTime, tariff.HammamEndTime, hammamStatus_TextBlock);
                var remainingWorkouts = await ApiClient._User.GetRemainingTrainerWorkouts(App.CurrentUser.Id);
                trainerStatus_TextBlock.Text = Helper.GetTrainerStatus(remainingWorkouts, trainerStatus_TextBlock, tariff.TrainerWorkoutsPerWeek);

                tariffStatuses_StackPanel.Visibility = Visibility.Visible;
                noTariff_TextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void main_Frame_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = main_Frame;
        }

        private void tariffs_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new TariffsPage());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //no idea why
            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Light, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.GreenSecondary]));
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Dark, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.GreenSecondary]));
        }

        private void profile_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(App.CurrentUser));
        }

        private void changeUser_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(DialogWindowType.Confirmation, "Вы уверены, что хотите сменить пользователя?");
            dw.ShowDialog();
            if (dw.DialogResult)
            {
                new StartWindow().Show();
                Close();
            }
        }

        private void quit_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(DialogWindowType.Confirmation, "Вы уверены, что хотите закрыть приложение?");
            dw.ShowDialog();
            if (dw.DialogResult)
                Application.Current.Shutdown();
        }

        private void profile_Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowPopup();
            Cursor = Cursors.Hand;
        }

        private void profile_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!popup.IsMouseOver)
            {
                HidePopup();
            }
            Cursor = null;
        }

        private void popup_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePopup();
        }

        private void HidePopup()
        {
            popup.Visibility = Visibility.Collapsed;
            patchup.Visibility = Visibility.Collapsed;
            profile_Border.CornerRadius = new CornerRadius(50);
            profile_Border.BorderThickness = new Thickness(2);
        }

        private void ShowPopup()
        {
            popup.Visibility = Visibility.Visible;
            patchup.Visibility = Visibility.Visible;
            profile_Border.CornerRadius = new CornerRadius(50, 50, 0, 0);
            profile_Border.BorderThickness = new Thickness(2, 2, 2, 0);
        }

        private void mods_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new UsersPage(UserSearchType.Mods));
        }

        private void users_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new UsersPage(UserSearchType.Users));
        }

        private void trainers_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new UsersPage(UserSearchType.Trainers));
        }

        private void tracker_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new TrackerPage());
        }

        private void Run_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new TariffsPage());
        }
    }
}
