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
using vtb_fitness_client.UserControls;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Pages
{
    /// <summary>
    /// Interaction logic for TariffsPage.xaml
    /// </summary>
    public partial class TariffsPage : Page
    {
        public TariffsPage()
        {
            InitializeComponent();
            InitView();
        }

        private async void InitView()
        {
            var tariffs = await ApiClient._Tariff.Get();
            foreach (var t in tariffs)
            {
                tariffs_StackPanel.Children.Add(new TariffUserControl(t));
            }

            if (App.CurrentUser.RoleId == 1)
            {
                var button = new Button
                {
                    Content = "Добавить новый абонемент",
                    Style = Application.Current.FindResource("global_Button") as Style,
                    Margin = new Thickness(0, 0, 0, 20)
                };
                button.Click += (s, e) =>
                {
                    PageManager.MainFrame.Navigate(new AddNewTariffPage());
                };
                tariffs_StackPanel.Children.Insert(0, button);
            }
        }
    }
}
