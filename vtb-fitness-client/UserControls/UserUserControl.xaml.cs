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
using vtb_fitness_client.Pages;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for UserUserControl.xaml
    /// </summary>
    public partial class UserUserControl : UserControl
    {
        private User _user;
        public UserUserControl(User user)
        {
            InitializeComponent();
            _user = user;
            InitView();
        }

        private async void InitView()
        {
            fullname_TextBlock.Text = $"{_user.Lastname} {_user.Name} {_user.Middlename}";
            var role = await ApiClient._Role.GetById(_user.RoleId);
            role_TextBlock.Text = $"{role.Name}";
            registrationDate_TextBlock.Text = $"Дата регистрации: {Helper.GetRuDateTime(_user.CreatedAt)}";
            phone_TextBlock.Text = $"Номер телефона: +7{_user.Phone}";
            var curTariff = await ApiClient._User.GetCurrentTariff(_user.Id);
            currentTariff_TextBlock.Text = "Действующий абонемент: ";
            currentTariff_TextBlock.Text += curTariff != null ? $"{curTariff.Tariff.Name}" : "Нет";

            pfp_Image.Source = ImageHelper.GetImage(_user.Pfp);
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage(_user));
        }
    }
}
