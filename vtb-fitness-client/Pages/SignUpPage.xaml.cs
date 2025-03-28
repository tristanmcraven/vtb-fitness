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
using vtb_fitness_client.Windows;

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
            InitView();
        }

        private void InitView()
        {
            role_ComboBox.SelectedIndex = 0;
        }

        private bool IsSomethingEmpty()
        {
            bool result = 
                String.IsNullOrWhiteSpace(lastName_TextBox.Text) ||
                String.IsNullOrWhiteSpace(name_TextBox.Text) ||
                String.IsNullOrWhiteSpace(middleName_TextBox.Text) ||
                String.IsNullOrWhiteSpace(phone_TextBox.Text) ||
                String.IsNullOrWhiteSpace(email_TextBox.Text) ||
                String.IsNullOrWhiteSpace(passportSeries_TextBox.Text) ||
                String.IsNullOrWhiteSpace(passportNumber_TextBox.Text) ||
                String.IsNullOrWhiteSpace(issuedBy_TextBox.Text) ||
                issuedDate_DatePicker.SelectedDate == null ||
                String.IsNullOrWhiteSpace(unitCode_TextBox.Text) ||
                birthDay_DatePicker.SelectedDate == null ||
                String.IsNullOrWhiteSpace(birthPlace_TextBox.Text) ||
                String.IsNullOrWhiteSpace(login_TextBox.Text) ||
                String.IsNullOrWhiteSpace(passwordBox.Password) ||
                String.IsNullOrWhiteSpace(passwordBoxConfirm.Password);
            if (role_ComboBox.SelectedIndex == 0)
            {
                result = result || workingInVtbSince_DatePicker.SelectedDate == null;
            }
            else
            {
                bool tempResult =
                    String.IsNullOrWhiteSpace(correspondentAccount_TextBox.Text) ||
                    String.IsNullOrWhiteSpace(bik_TextBox.Text) ||
                    String.IsNullOrWhiteSpace(bankName_TextBox.Text) ||
                    String.IsNullOrWhiteSpace(debitCardNumber_TextBox.Text);
                result = result || tempResult;
            }
            return result;
        }

        private bool DoPasswordsMatch()
        {
            return passwordBox.Password == passwordBoxConfirm.Password;
        }

        private void photo_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void role_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (role_ComboBox.SelectedIndex == 0)
            {
                workingInVtbSince_DatePicker.Visibility = Visibility.Visible;
                bankingDetails_Expander.Visibility = Visibility.Collapsed;
            }
            else
            {
                workingInVtbSince_DatePicker.Visibility = Visibility.Collapsed;
                bankingDetails_Expander.Visibility = Visibility.Visible;
            }
        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsSomethingEmpty())
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                                 "Ошибка",
                                 "Сначала заполните все поля!",
                                 DialogWindowButtons.Ok,
                                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                return;
            }

            if (DoPasswordsMatch())
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                 "Ошибка",
                 "Пароли не совпадают",
                 DialogWindowButtons.Ok,
                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                return;
            }


        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
        }
    }
}
