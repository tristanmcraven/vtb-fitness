using System.Windows.Controls;
using System.Windows.Input;
using vtb_fitness_client.Network;
using vtb_fitness_client.Utility;
using vtb_fitness_client.Windows;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for AddTrackerUserControl.xaml
    /// </summary>
    public partial class AddTrackerUserControl : UserControl
    {
        public AddTrackerUserControl()
        {
            InitializeComponent();
        }

        private async void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var currentTariff = await ApiClient._User.GetCurrentTariff(App.CurrentUser.Id);
            if (currentTariff == null)
            {
                new DialogWindow(DialogWindowType.Error, "Вы должны купить абонемент перед тем, как пользоваться фитнес-трекером.").ShowDialog();
                return;
            }
            new AddTrackerWindow(WindowManager.Get<MainWindow>()) { Owner = WindowManager.Get<MainWindow>() }.ShowDialog();
        }
    }
}
