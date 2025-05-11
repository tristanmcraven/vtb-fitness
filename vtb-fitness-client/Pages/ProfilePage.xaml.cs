using System;
using System.CodeDom;
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
using vtb_fitness_client.Dto;
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
        private bool isOwnProfile = false;
        private Tariff? _currentTariff = null;
        private User? _currentTrainer = null;
        private User? _currentUserCurrentTrainer = null;
        public ProfilePage(User user)
        {
            InitializeComponent();
            _user = user;
            if (App.CurrentUser.Id == _user.Id) isOwnProfile = true;
            InitView();
        }

        private async void InitView()
        {
            GetPfp();

            DisplayGenericData();

            await DisplayCurrentTariff();

            await DisplayCurrentTrainer();

            GetTrainerSpecs();

            AdjustRole();
        }

        private async void GetTrainerSpecs()
        {
            var specs = await ApiClient._TrainerSpecs.GetTrainerSpecs(_user.Id);
            if (specs != null && specs.Count > 0)
            {
                foreach (var s in specs)
                {
                    trainerSpecs_StackPanel.Children.Add(new TextBlock { Text = $"• {s.Name}" });
                }
            }
            else
            {
                trainerSpecs_StackPanel.Children.Add(new TextBlock { Text = "Нет" });
            }
        }

        private void AdjustRole()
        {
            //ядерный реактор нахуй
            var rules = new List<ViewVisibilityRule>
            {
                new("changePfp_Button", viewerRole => isOwnProfile),
                new("deleteTariff_Button", viewerRole => (viewerRole == UserRole.Admin || viewerRole == UserRole.Moderator) 
                    && _currentTariff != null && !isOwnProfile),
                new("fireMod_Button", viewerRole => viewerRole == UserRole.Admin && _user.RoleId == (int)UserRole.Moderator),
                new("fireTrainer_Button", viewerRole => (viewerRole == UserRole.Admin || viewerRole == UserRole.Moderator) && _user.RoleId == (int)UserRole.Trainer),
                new("changeSpec_Button", viewerRole => viewerRole == UserRole.Trainer && isOwnProfile),
                //new("chooseTrainer_Button", viewerRole => viewerRole != UserRole.Trainer
                //    && (_currentUserCurrentTrainer != null && _currentUserCurrentTrainer.Id != _user.Id) && _user.RoleId == (int)UserRole.Trainer),
                new("unChooseTrainer_Button", viewerRole => _currentUserCurrentTrainer != null && !isOwnProfile && _user.RoleId == (int)UserRole.Trainer
                    && _currentUserCurrentTrainer.Id == _user.Id),
                new("generalData_StackPanel", viewerRole => (viewerRole == UserRole.Admin || viewerRole == UserRole.Moderator)),
                new("trainerData_StackPanel", viewerRole => _user.RoleId == (int)UserRole.Trainer),
                new("currentTariff_StackPanel", viewerRole => _user.RoleId != (int)UserRole.Trainer),
                new("currentTrainer_StackPanel", viewerRole => _user.RoleId != (int)UserRole.Trainer),

            };

            foreach (var rule in rules)
            {
                var button = FindName(rule.ViewName) as FrameworkElement;
                if (button != null)
                    button.Visibility = rule.ShouldBeVisible((UserRole)App.CurrentUser.RoleId) ? Visibility.Visible : Visibility.Collapsed;
            }

            //впизду
            if (_currentUserCurrentTrainer != null && _currentUserCurrentTrainer.Id == _user.Id || _user.RoleId != (int)UserRole.Trainer
                || App.CurrentUser.RoleId == (int)UserRole.Trainer || isOwnProfile)
                chooseTrainer_Button.Visibility = Visibility.Collapsed;
            else
                chooseTrainer_Button.Visibility = Visibility.Visible;
        }

        private async void DisplayGenericData()
        {
            fullName_TextBlock.Text = $"{_user.Lastname} {_user.Name} {_user.Middlename}";
            role_TextBlock.Text = $"{(await ApiClient._Role.GetById(_user.RoleId)).Name}";
            registrationDate_TextBlock.Text = $"Дата регистрации: {Helper.GetRuDateTime(_user.CreatedAt)}";
            phone_TextBlock.Text = $"Номер телефона: +7{_user.Phone}";
            email_TextBlock.Text = $"Email: {_user.Email}";
        }

        private void GetPfp()
        {
            pfp_Image.Source = ImageHelper.GetImage(_user.Pfp);
        }

        private async void changePfp_Button_Click(object sender, RoutedEventArgs e)
        {
            var rawImage = ImageHelper.GetImageFromFileDialog().RawImage;

            if (rawImage != null)
            {
                var result = await ApiClient._User.UpdatePfp(new UserUpdatePfpDto { UserId = _user.Id, Pfp = rawImage });
                if (result == null)
                    new DialogWindow().ShowDialog();
                else
                {
                    App.CurrentUser = await ApiClient._User.GetById(App.CurrentUser.Id);
                    PageManager.MainFrame.Navigate(new ProfilePage(App.CurrentUser));
                }
            };
        }

        private void deleteTariff_Button_Click(object sender, RoutedEventArgs e)
        {
            new DialogWindow().ShowDialog();
        }

        private void fireMod_Button_Click(object sender, RoutedEventArgs e)
        {
            new DialogWindow().ShowDialog();
        }

        private void fireTrainer_Button_Click(object sender, RoutedEventArgs e)
        {
            new DialogWindow().ShowDialog();
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

            var trainerChooseString = "";

            if (_currentUserCurrentTrainer != null)
            {
                var fullName = $"{_currentUserCurrentTrainer.Lastname} {_currentUserCurrentTrainer.Name} {_currentUserCurrentTrainer.Middlename}";
                trainerChooseString = $"Вы уже занимаетесь у тренера {fullName}. ";
            }

            trainerChooseString += $"Вы уверены, что хотите записаться к тренеру {_user.Lastname} {_user.Name} {_user.Middlename}?";

            var dw = new DialogWindow(DialogWindowType.Confirmation, trainerChooseString);
            dw.ShowDialog();

            if (dw.DialogResult == true)
            {
                var result = await ApiClient._User.AssignTrainer(App.CurrentUser.Id, _user.Id);
                if (result == null) new DialogWindow().ShowDialog();
                PageManager.MainFrame.Navigate(new ProfilePage(_user));
            }
        }

        private async Task DisplayCurrentTariff()
        {
            var userTariff = await ApiClient._User.GetCurrentTariff(_user.Id);

            if (userTariff == null)
            {
                noTariff_TextBlock.Visibility = Visibility.Visible;
                currentTariff_Grid.Visibility = Visibility.Collapsed;
            }

            else
            {
                var tariff = _currentTariff = userTariff.Tariff!;

                noTariff_TextBlock.Visibility = Visibility.Collapsed;
                currentTariff_Grid.Visibility = Visibility.Visible;

                currentTariff_Grid.Children.Add(new TariffUserControl(tariff, userTariff));
            }
        }

        private async Task DisplayCurrentTrainer()
        {
            _currentTrainer = await ApiClient._User.GetTrainer(_user.Id);
            _currentUserCurrentTrainer = await ApiClient._User.GetTrainer(App.CurrentUser.Id);

            if (_currentTrainer == null)
            {
                noTrainer_TextBlock.Visibility = Visibility.Visible;
                currentTrainer_Grid.Visibility= Visibility.Collapsed;
            }

            else
            {
                noTrainer_TextBlock.Visibility = Visibility.Collapsed;
                currentTrainer_Grid.Visibility = Visibility.Visible;

                currentTrainer_Grid.Children.Add(new UserUserControl(_currentTrainer));
            }
        }

        private async void unChooseTrainer_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(DialogWindowType.Confirmation, 
                $"Вы уверены, что хотите перестать заниматься у тренера {_user.Lastname} {_user.Name} {_user.Middlename}?");
            dw.ShowDialog();

            if (dw.DialogResult == true)
            {
                var result = await ApiClient._User.UnassignTrainer(App.CurrentUser.Id, _user.Id);
                if (result == null)
                    new DialogWindow().ShowDialog();
                PageManager.MainFrame.Navigate(new ProfilePage(_user));
            }
        }
    }
}
