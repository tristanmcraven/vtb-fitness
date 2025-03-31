using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using vtb_fitness_client.Pages;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

        }

        private void signIn_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signUp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_Frame_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = main_Frame;
            PageManager.MainFrame.Navigate(new SignInPage());
        }

        private void video_MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Play();
        }

        private void video_MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            video_MediaElement.Position = TimeSpan.Zero;
            video_MediaElement.Play();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //no idea why
            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Light, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Dark, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
        }
    }
}
