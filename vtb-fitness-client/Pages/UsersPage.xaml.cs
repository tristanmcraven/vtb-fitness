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

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private UserSearchType _type;
        public UsersPage(UserSearchType type)
        {
            InitializeComponent();
            _type = type;
            InitView();
        }

        private async void InitView()
        {
            DisplayResults(await Search(""));
            searchType_TextBlock.Text = _type switch
            {
                UserSearchType.Users => "Поиск пользователей (сотрудников ВТБ)",
                UserSearchType.Mods => "Поиск модераторов",
                UserSearchType.Trainers => "Поиск тренеров"
            };
        }

        private async void search_Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayResults(await Search(search_TextBox.Text));
        }

        private async Task<List<User>> Search(string q)
        {
            if (String.IsNullOrWhiteSpace(q))
                return await ApiClient._User.Search(q, _type);
            return await ApiClient._User.Search(q, _type);
        }

        private void DisplayResults(List<User> users)
        {
            if (!users.Any())
            {
                noResults_StackPanel.Visibility = Visibility.Visible;
                return;
            }
            noResults_StackPanel.Visibility = Visibility.Collapsed;
            users_StackPanel.Children.Clear();
            foreach (var u in users)
            {
                users_StackPanel.Children.Add(new UserUserControl(u));
            }
        }
    }
}
