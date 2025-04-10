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

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TariffPriceUserControl.xaml
    /// </summary>
    public partial class TariffPriceUserControl : UserControl
    {
        private double? _price;
        public TariffPriceUserControl(double? price)
        {
            InitializeComponent();
            _price = price;
            InitView();
        }

        private async void InitView()
        {
            price_TextBlock.Text = _price.ToString() + "₽";

            if (App.CurrentUser.RoleId == 3)
            {
                var sale = await Helper.GetUserSalePercent(App.CurrentUser.Id);
                if (sale > 0)
                {
                    discountedPrice_TextBlock.Text = Helper.GetDiscountedPriceAsString((double)_price, sale) + "₽";
                    discountedPrice_TextBlock.Visibility = Visibility.Visible;
                    price_TextBlock.TextDecorations = TextDecorations.Strikethrough;
                    price_TextBlock.FontSize = 16;
                    price_TextBlock.Foreground = new SolidColorBrush(Colors.Gray);
                    discountedPrice_TextBlock.Foreground = new SolidColorBrush(Colors.LightGreen);
                }
            }
        }
    }
}
