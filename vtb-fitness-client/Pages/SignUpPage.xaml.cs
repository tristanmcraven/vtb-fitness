using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private byte[]? _selectedImage = null;
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

        private bool AreDatesValid() =>
            Helper.IsDateValid(birthDay_DatePicker.SelectedDate) ||
            Helper.IsDateValid(issuedDate_DatePicker.SelectedDate) ||
            Helper.IsDateValid(workingInVtbSince_DatePicker.SelectedDate);


        private async void SignUp()
        {
            if (IsSomethingEmpty())
            {
                new DialogWindow(DialogWindowType.Error, "Сначала заполните все поля!").ShowDialog();
                return;
            }

            if (!DoPasswordsMatch())
            {
                new DialogWindow(DialogWindowType.Error, "Пароли не совпадают").ShowDialog();
                return;
            }

            if (!Helper.IsEmailCorrect(email_TextBox.Text))
            {
                new DialogWindow(DialogWindowType.Error, "Некорректный адрес электронной почты").ShowDialog();
                return;
            }

            if (!AreDatesValid())
            {
                new DialogWindow(DialogWindowType.Error, "Даты не корректны").ShowDialog();
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
                Pfp = _selectedImage,
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
                new DialogWindow(DialogWindowType.Error, "Что-то пошло не так. Скорее всего, пользователь с таким логином уже существует").ShowDialog();
                return;
            }
            else
            {
                new DialogWindow(DialogWindowType.Success, "Пожалуйста, войдите в систему", "Успешная регистрация").ShowDialog();
                PageManager.MainFrame.GoBack();
            }
        }

        private void GoBack()
        {
            PageManager.MainFrame.GoBack();
        }

        private void photo_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = ImageHelper.GetImageFromFileDialog();
            var fileName = result.FileName;
            if (fileName != null)
            {
                photo_Button.Content = fileName;
                _selectedImage = result.RawImage;
            }
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

        private void VerifyNumericInput(object sender, TextCompositionEventArgs e)
        {
            if (!Helper.IsNumeric(e.Text)) e.Handled = true;
        }

        private void VerifyNumericPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                if (!Helper.IsNumeric(text))
                    e.CancelCommand();
            }
            else e.CancelCommand();
        }
    }
}
