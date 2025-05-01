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

            return result || pros;
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
                Period = new TimeSpan(int.Parse(period_TextBox.Text), 0, 0),
                Pros = GetPros()
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
    }
}
