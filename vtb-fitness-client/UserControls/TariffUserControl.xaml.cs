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
using System.Windows.Interop;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TariffUserControl.xaml
    /// </summary>
    public partial class TariffUserControl : UserControl
    {
        private Tariff _tariff;
        private UserTariff? _userTariff = null;
        public TariffUserControl(Tariff tariff, UserTariff? userTariff = null)
        {
            InitializeComponent();
            _tariff = tariff;
            _userTariff = userTariff;
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

            if (_userTariff != null)
            {
                buy_Button.Visibility = Visibility.Collapsed;
                userTariffData_Grid.Visibility = Visibility.Visible;

                tariffBoughtAt_TextBlock.Text = $"Абонемент приобретён: {Helper.GetRuDateTime(_userTariff.AcquiredAt)}";
                tariffExpiresAt_TextBlock.Text = $"Истекает: {Helper.GetRuDateTime(_userTariff.ExpiresAt)}";
                moneyPaid_TextBlock.Text = $"Приобретено по цене: {_userTariff.MoneyPaid}₽";

                Margin = new Thickness(0, 0, 0, 10);
            }
        }

        private async void buy_Button_Click(object sender, RoutedEventArgs e)
        {
            var discountedPrice = Helper.GetDiscountedPrice((double)_tariff.Price, await Helper.GetUserSalePercent(App.CurrentUser.Id));

            var currentUserTariff = await ApiClient._User.GetCurrentTariff(App.CurrentUser.Id);
            var purchaseString = "";
            var expiresAtString = "";

            if (currentUserTariff != null)
            {
                var expiresAt = Helper.GetRuDateTime(currentUserTariff.ExpiresAt.Value);
                expiresAtString = $"{expiresAt.Substring(0, expiresAt.Length - 6)}";

                purchaseString = $"Внимание: у вас уже есть абонемент {currentUserTariff.Tariff.Name} {currentUserTariff.Tariff.Period.Value.TotalHours}, " +
                    $"действующий до {expiresAtString}. " +
                    $"Вы уверены, что хотите купить абонемент \"{_tariff.Name}\" за {discountedPrice}₽? Деньги за прошлый абонемент не вернутся!";
            }
            else
            {
                purchaseString = $"Вы уверены, что хотите купить абонемент \"{_tariff.Name}\" за {discountedPrice}₽?";
            }

            var dw = new DialogWindow(DialogWindowType.Confirmation, purchaseString);
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
