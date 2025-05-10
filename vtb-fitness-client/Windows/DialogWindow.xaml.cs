using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : CustomWindow
    {
        public new bool DialogResult = false;
        public DialogWindow(DialogWindowType type, string message, string? title = null)
        {
            Owner = WindowManager.ActiveWindow;
            InitializeComponent();
            InitView(type, message, title);
        }

        public DialogWindow()
        {
            Owner = WindowManager.ActiveWindow;
            InitializeComponent();
            InitView(DialogWindowType.Error, "Ошибка", "Что-то пошло не так");
        }

        private void InitView(DialogWindowType type, string message, string? title)
        {
            if (type == DialogWindowType.Error)
            {
                SystemSounds.Hand.Play();
                title_TextBlock.Text = "Ошибка";
                Title = "Ошибка";

                ShowOkButton();
            }
            else if (type == DialogWindowType.Success)
            {
                SystemSounds.Exclamation.Play();
                title_TextBlock.Text = "Успех";
                Title = "Успех";

                ShowOkButton();
            }
            else if (type == DialogWindowType.Confirmation)
            {
                SystemSounds.Exclamation.Play();
                title_TextBlock.Text = "Подтверждение";
                Title = "Подтверждение";

                ShowYesNoButtons();
            }

            message_TextBlock.Text = message;
            if (title != null) title_TextBlock.Text = title;
        }

        private void ShowOkButton()
        {
            ok_Button.Visibility = Visibility.Visible;
            yes_Button.Visibility = Visibility.Collapsed;
            no_Button.Visibility = Visibility.Collapsed;
        }

        private void ShowYesNoButtons()
        {
            ok_Button.Visibility = Visibility.Collapsed;
            yes_Button.Visibility = Visibility.Visible;
            no_Button.Visibility = Visibility.Visible;
        }

        private void ok_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void yes_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void no_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
