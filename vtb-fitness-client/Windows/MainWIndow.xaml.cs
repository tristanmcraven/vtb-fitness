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
            InitView();
        }

        private void InitView()
        {
            var user = App.CurrentUser;
            fullname_TextBlock.Text = $"{user.Lastname} {user.Name} {user.Middlename}";
            role_TextBlock.Text = App.CurrentUser.RoleId switch
            {
                1 => "Администратор",
                2 => "Модератор",
                3 => "Сотрудник ВТБ",
                4 => "Тренер",
                _ => "Неизвестный"
            };
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
            //no idea why
            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Light, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
            paletteHelper.SetTheme(Theme.Create(BaseTheme.Dark, SwatchHelper.Lookup[MaterialDesignColor.Blue], SwatchHelper.Lookup[MaterialDesignColor.YellowSecondary]));
        }

        private void profile_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ProfilePage());
        }

        private void changeUser_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(WindowManager.Get<MainWindow>(),
                          "Выход",
                          "Вы уверены, что хотите сменить пользователя?",
                          DialogWindowButtons.YesNo,
                          DialogWindowType.Warning)
            { Owner = WindowManager.Get<MainWindow>() };
            dw.ShowDialog();
            if (dw.DialogResult)
            {
                new StartWindow().Show();
                Close();
            }
        }

        private void quit_Button_Click(object sender, RoutedEventArgs e)
        {
            var dw = new DialogWindow(WindowManager.Get<MainWindow>(),
                                      "Выход",
                                      "Вы уверены, что хотите закрыть приложение?",
                                      DialogWindowButtons.YesNo,
                                      DialogWindowType.Warning)
            { Owner = WindowManager.Get<MainWindow>() };
            dw.ShowDialog();
            if (dw.DialogResult)
                Application.Current.Shutdown();
        }
    }
}
