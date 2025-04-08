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

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new AddTrackerWindow(WindowManager.Get<MainWindow>()) { Owner = WindowManager.Get<MainWindow>() }.ShowDialog();
        }
    }
}
