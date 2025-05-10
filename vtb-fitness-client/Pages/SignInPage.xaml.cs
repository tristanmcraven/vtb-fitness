using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        private async void signIn_Button_Click(object sender, RoutedEventArgs e)
        {
            var login = login_TextBox.Text;
            var password = passwordBox.Password;

            signIn_Button.Background = FindResource("dark_blue") as SolidColorBrush;
            signIn_Button.BorderBrush = FindResource("dark_blue") as SolidColorBrush;
            signIn_Button.Content = new ProgressBar
            {
                IsIndeterminate = true,
                Style = (Style)FindResource("MaterialDesignCircularProgressBar")
            };
            signIn_Button.Click -= signIn_Button_Click;

            try
            {
                if (IsSomethingEmpty())
                {
                    new DialogWindow(DialogWindowType.Error, "Введите данные от учётной записи").ShowDialog();
                    return;
                }

                var user = await ApiClient._User.SignIn(login, password);
                if (user == null)
                {
                    new DialogWindow(DialogWindowType.Error, "Неправильный логин и/или пароль").ShowDialog();
                    return;
                }

                App.CurrentUser = user;
                
                new MainWindow().Show();
                WindowManager.Close<StartWindow>();
            }
            finally
            {
                signIn_Button.Content = "Войти";
                signIn_Button.Click += signIn_Button_Click;
                signIn_Button.ClearValue(Button.BackgroundProperty);
                signIn_Button.ClearValue(Button.BorderBrushProperty);
            }
        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new SignUpPage());
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
