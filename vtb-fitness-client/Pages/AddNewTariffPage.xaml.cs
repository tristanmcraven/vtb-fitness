using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Storage;
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
    /// Interaction logic for AddNewTariffPage.xaml
    /// </summary>
    public partial class AddNewTariffPage : Page
    {
        public AddNewTariffPage()
        {
            InitializeComponent();
            InitView();
        }

        private void InitView()
        {
            AddProTextBox();
        }

        private bool IsSomethingEmpty()
        {
            var result =
                String.IsNullOrWhiteSpace(name_TextBox.Text) ||
                String.IsNullOrWhiteSpace(description_TextBox.Text) ||
                String.IsNullOrWhiteSpace(price_TextBox.Text) ||
                String.IsNullOrWhiteSpace(period_TextBox.Text);
            bool pros = false;
            foreach (var obj in pros_StackPanel.Children)
            {
                var tb = obj as TextBox;
                if (String.IsNullOrWhiteSpace(tb.Text))
                    pros = true;
            }

            bool hours = false;
            if (gymEnabled_CheckBox.IsChecked == true && gym24_CheckBox.IsChecked == false)
            {
                hours = !gymStartHours_TimePicker.SelectedTime.HasValue || !gymEndHours_TimePicker.SelectedTime.HasValue;
            }
            if (poolEnabled_CheckBox.IsChecked == true && pool24_CheckBox.IsChecked == false)
            {
                hours = !poolStartHours_TimePicker.SelectedTime.HasValue || !poolEndHours_TimePicker.SelectedTime.HasValue;
            }
            if (hammamEnabled_CheckBox.IsChecked == true && hammam24_CheckBox.IsChecked == false)
            {
                hours = !hammamStartHours_TimePicker.SelectedTime.HasValue || !hammamEndHours_TimePicker.SelectedTime.HasValue;
            }
            if (trainerEnabled_CheckBox.IsChecked == true && trainer24_CheckBox.IsChecked == false)
            {
                hours = String.IsNullOrWhiteSpace(trainerWorkoutsPerWeek_TextBox.Text);
            }

            return result || pros || hours;
        }

        private void AddProTextBox()
        {
            var tb = new TextBox();
            tb.KeyUp += (s, e) =>
            {
                if (e.Key == Key.Enter)
                    AddProTextBox();
            };
            pros_StackPanel.Children.Add(tb);
            tb.Focus();
        }

        private List<string> GetPros()
        {
            List<string> pros = new();

            foreach (var obj in pros_StackPanel.Children)
            {
                var tb = obj as TextBox;
                pros.Add(tb.Text);
            }

            return pros;
        }

        private async void add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsSomethingEmpty())
            {
                new DialogWindow(DialogWindowType.Error, "Введите всю информацию об абонементе").ShowDialog();
                return;
            }

            var dto = new TariffCreateDto
            {
                Name = name_TextBox.Text,
                Description = description_TextBox.Text,
                Price = Convert.ToDouble(price_TextBox.Text),
                Period = new TimeSpan(int.Parse(period_TextBox.Text), 0, 0), //it is what it is
                Pros = GetPros(),
                GymStartTime = gymEnabled_CheckBox.IsChecked == true 
                    ? gym24_CheckBox.IsChecked == false 
                        ? TimeOnly.FromDateTime(gymStartHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                GymEndTime = gymEnabled_CheckBox.IsChecked == true
                    ? gym24_CheckBox.IsChecked == false
                        ? TimeOnly.FromDateTime(gymEndHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                PoolStartTime = poolEnabled_CheckBox.IsChecked == true
                    ? pool24_CheckBox.IsChecked == false
                        ? TimeOnly.FromDateTime(poolStartHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                PoolEndTime = poolEnabled_CheckBox.IsChecked == true
                    ? pool24_CheckBox.IsChecked == false
                        ? TimeOnly.FromDateTime(poolEndHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                HammamStartTime = hammamEnabled_CheckBox.IsChecked == true
                    ? hammam24_CheckBox.IsChecked == false
                        ? TimeOnly.FromDateTime(hammamStartHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                HammamEndTime = hammamEnabled_CheckBox.IsChecked == true
                    ? hammam24_CheckBox.IsChecked == false
                        ? TimeOnly.FromDateTime(hammamEndHours_TimePicker.SelectedTime!.Value) : new TimeOnly(00, 00) : null,
                TrainerWorkoutsPerWeek = trainerEnabled_CheckBox.IsChecked == true
                    ? trainer24_CheckBox.IsChecked == false
                        ? int.Parse(trainerWorkoutsPerWeek_TextBox.Text) : 666 : null
            };
            var tariff = await ApiClient._Tariff.Create(dto);
            if (tariff != null)
            {
                new DialogWindow(DialogWindowType.Success, "Абонемент успешно добавлен!").ShowDialog();
                PageManager.MainFrame.Navigate(new TariffsPage());
                return;
            }
            else
            {
                new DialogWindow(DialogWindowType.Error, "Что-то пошло не так").ShowDialog();
                return;
            }

        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
        }

        private void TwentyFourCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var tag = (sender as CheckBox)?.Tag as string;
            if (String.IsNullOrWhiteSpace(tag)) return;

            if (tag == "trainer")
            {
                (FindName($"{tag}WorkoutsPerWeek_TextBox") as TextBox).IsEnabled = false;
                return;
            }

            var startTimePicker = FindName($"{tag}StartHours_TimePicker") as TimePicker;
            var endTimePicker = FindName($"{tag}EndHours_TimePicker") as TimePicker;

            if (startTimePicker != null) startTimePicker.IsEnabled = false;
            if (endTimePicker != null) endTimePicker.IsEnabled = false;
        }

        private void TwentyFourCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var tag = (sender as CheckBox)?.Tag as string;
            if (String.IsNullOrWhiteSpace(tag)) return;

            if (tag == "trainer")
            {
                (FindName($"{tag}WorkoutsPerWeek_TextBox") as TextBox).IsEnabled = true;
                return;
            }

            var startTimePicker = FindName($"{tag}StartHours_TimePicker") as TimePicker;
            var endTimePicker = FindName($"{tag}EndHours_TimePicker") as TimePicker;

            if (startTimePicker != null) startTimePicker.IsEnabled = true;
            if (endTimePicker != null) endTimePicker.IsEnabled = true;
        }

        private void EnabledCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb != null)
            {
                var gb = Helper.FindParent<GroupBox>(cb);
                if (gb != null)
                {
                    gb.ClearValue(GroupBox.BackgroundProperty);
                    var headerBorder = gb.Header as Border;
                    if (headerBorder != null) headerBorder.ClearValue(Border.BackgroundProperty);

                    var tag = cb.Tag as string;
                    if (tag != null)
                    {
                        var _24cb = FindName($"{tag}24_CheckBox") as CheckBox;
                        if (_24cb != null) _24cb.IsEnabled = true;
                    }
                    var content = gb.Content as Grid;
                    if (content != null)
                    {
                        content.IsEnabled = true;
                    }
                }
            }
        }

        private void EnabledCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb != null)
            {
                var gb = Helper.FindParent<GroupBox>(cb);
                if ( gb != null )
                {
                    gb.Background = FindResource("dark_blue") as SolidColorBrush;
                    var headerBorder = gb.Header as Border;
                    if (headerBorder != null) headerBorder.Background = FindResource("dark_blue") as SolidColorBrush;

                    var tag = cb.Tag as string;
                    if (tag != null)
                    {
                        var _24cb = FindName($"{tag}24_CheckBox") as CheckBox;
                        if (_24cb != null) _24cb.IsEnabled = false;
                    }
                    var content = gb.Content as Grid;
                    if (content != null)
                    {
                        content.IsEnabled = false;
                    }
                }
            }
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
