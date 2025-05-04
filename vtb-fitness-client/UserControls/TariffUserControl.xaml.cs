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
using vtb_fitness_client.Utility;
using vtb_fitness_client.Windows;
using MaterialDesignThemes.Wpf;
using vtb_fitness_client.Network;
using vtb_fitness_client.Dto;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TariffUserControl.xaml
    /// </summary>
    public partial class TariffUserControl : UserControl
    {
        private Tariff _tariff;
        public TariffUserControl(Tariff tariff)
        {
            InitializeComponent();
            _tariff = tariff;
            InitView();
        }

        private void InitView()
        {
            var nameTb = expander.Header as TextBlock;
            nameTb.Text = $"{_tariff.Name} ({Helper.GetTariffDuration(_tariff)} мес.)";

            ExpanderAssist.SetExpanderButtonContent(expander, new TariffPriceUserControl(_tariff.Price));

            var pros = _tariff.Pros;
            foreach (var p in pros)
            {
                pros_StackPanel.Children.Add(new ProUserControl(p));
            }

            
        }

        private async void buy_Button_Click(object sender, RoutedEventArgs e)
        {
            var discountedPrice = Helper.GetDiscountedPrice((double)_tariff.Price, await Helper.GetUserSalePercent(App.CurrentUser.Id));

            var dw = new DialogWindow(DialogWindowType.Confirmation,
                                      $"Вы уверены, что хотите купить абонемент \"{_tariff.Name}\" за " +
                                      $"{discountedPrice}₽?");
            dw.ShowDialog();

            if (dw.DialogResult)
            {
                var dto = new TariffPurchaseDto
                {
                    UserId = App.CurrentUser.Id,
                    TariffId = _tariff.Id,
                    MoneyPaid = discountedPrice
                };
                var result = await ApiClient._Tariff.Purchase(dto);
                if (result != null)
                {
                    new DialogWindow(DialogWindowType.Success, $"Вы успешно приобрели абонемент {_tariff.Name} за {discountedPrice}₽. Можете приступать к тренировкам!")
                        .ShowDialog();
                    WindowManager.Get<MainWindow>().UpdateCurrentTariffData();
                    return;
                }
                else new DialogWindow().ShowDialog();
            }
        }
    }
}
