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
using vtb_fitness_client.Dto;
using vtb_fitness_client.Network;
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

        private async void SignUp()
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

            if (!DoPasswordsMatch())
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                 "Ошибка",
                 "Пароли не совпадают",
                 DialogWindowButtons.Ok,
                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                return;
            }
            var roleId = role_ComboBox.SelectedIndex == 0 ? 3 : 4;
            var dto = new UserSignUpDto
            {
                Lastname = lastName_TextBox.Text,
                Name = name_TextBox.Text,
                Middlename = middleName_TextBox.Text,
                Phone = phone_TextBox.Text,
                Email = email_TextBox.Text,
                Pfp = null,
                RoleId = roleId,
                WorkingInVtbSince = role_ComboBox.SelectedIndex == 1 ? null : DateOnly.FromDateTime((DateTime)workingInVtbSince_DatePicker.SelectedDate!),
                Login = login_TextBox.Text,
                Password = passwordBox.Password,
                PassportCreateDto = new PassportCreateDto
                {
                    PassportSeries = passportSeries_TextBox.Text,
                    PassportNumber = passportNumber_TextBox.Text,
                    IssuedBy = issuedBy_TextBox.Text,
                    IssuedDate = DateOnly.FromDateTime((DateTime)issuedDate_DatePicker.SelectedDate!),
                    UnitCode = unitCode_TextBox.Text,
                    BirthDate = DateOnly.FromDateTime((DateTime)birthDay_DatePicker.SelectedDate!),
                    BirthPlace = birthPlace_TextBox.Text
                },
                BankingDetailsDto = role_ComboBox.SelectedIndex == 0 ? null : new BankingDetailsCreateDto
                {
                    CorrespondentAccount = correspondentAccount_TextBox.Text,
                    Bik = bik_TextBox.Text,
                    BankName = bankName_TextBox.Text,
                    DebitCardNumber = debitCardNumber_TextBox.Text
                }
            };
            var user = await ApiClient._User.SignUp(dto);

            if (user == null)
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                                 "Ошибка",
                                 "Что-то пошло не так",
                                 DialogWindowButtons.Ok,
                                 DialogWindowType.Error)
                { Owner = WindowManager.Get<StartWindow>()}.ShowDialog();
                return;
            }
            else
            {
                new DialogWindow(WindowManager.Get<StartWindow>(),
                                 "Успешная регистрация",
                                 "Пожалуйста, войдите в систему",
                                 DialogWindowButtons.Ok,
                                 DialogWindowType.Info)
                { Owner = WindowManager.Get<StartWindow>() }.ShowDialog();
                PageManager.MainFrame.GoBack();
            }
        }

        private void GoBack()
        {
            PageManager.MainFrame.GoBack();
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
            SignUp();
        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    SignUp();
                    break;
                case Key.Escape:
                    GoBack();
                    break;
            }
            e.Handled = true;
        }

        private void unitCode_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ai generated ahh, check func later
            var tb = sender as TextBox;

            if (tb == null) return;

            string digits = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (digits.Length > 6)
                digits = digits.Substring(0, 6);

            if (digits.Length > 3)
                tb.Text = digits.Insert(3, "-");
            else
                tb.Text = digits;

            tb.CaretIndex = tb.Text.Length;
        }
    }
}
