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
using vtb_fitness_client.Network;
using vtb_fitness_client.Utility;
using vtb_fitness_client.Windows;

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private bool IsSomethingEmpty() => String.IsNullOrWhiteSpace(login_TextBox.Text) || String.IsNullOrWhiteSpace(passwordBox.Password);

        private void signIn_Button_Click(object sender, RoutedEventArgs e)
        {
            var login = login_TextBox.Text;
            var password = passwordBox.Password;

            SignIn(login, password);
        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new SignUpPage());
        }

        public async void SignIn(string login, string password)
        {
            if (IsSomethingEmpty())
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                                 "Ошибка",
                                 "Введите данные от учётной записи",
                                 DialogWindowButtons.Ok,
                                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                return;
            }
            var user = await ApiClient._User.SignIn(login, password);
            if (user == null)
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                                 "Ошибка",
                                 "Неправильный логин и/или пароль",
                                 DialogWindowButtons.Ok,
                                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                return;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            login_TextBox.Focus();
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    signIn_Button_Click(null, null);
                    break;
            }
        }
    }
}
