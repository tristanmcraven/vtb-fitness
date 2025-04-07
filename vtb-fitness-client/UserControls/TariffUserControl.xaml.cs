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
            nameTb.Text = $"{_tariff.Name} ({_tariff.Period.Value.Hours} мес.)";

            ExpanderAssist.SetExpanderButtonContent(expander, new TariffPriceUserControl(_tariff.Price));

            var pros = _tariff.Pros;
            foreach (var p in pros)
            {
                pros_StackPanel.Children.Add(new ProUserControl(p));
            }
        }

        private void buy_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(WindowManager.Get<MainWindow>(),
                                      "Подтверждение",
                                      $"Вы уверены, что хотите купить абонемент \"{_tariff.Name}\" за {_tariff.Price}₽?",
                                      DialogWindowButtons.YesNo,
                                      DialogWindowType.Info)
            { Owner = WindowManager.Get<MainWindow>() };
            dw.ShowDialog();
            if (dw.DialogResult)
            {

            }
        }
    }
}
