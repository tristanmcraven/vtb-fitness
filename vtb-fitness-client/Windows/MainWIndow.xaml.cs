using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using vtb_fitness_client.Pages;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void main_Frame_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = main_Frame;
        }

        private void tariffs_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PageManager.MainFrame.Navigate(new TariffsPage());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Light, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Dark, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
        }
    }
}
