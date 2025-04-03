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

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for ProUserControl.xaml
    /// </summary>
    public partial class ProUserControl : UserControl
    {
        private string _pro;
        public ProUserControl(string pro)
        {
            InitializeComponent();
            _pro = pro;
            InitView();
        }

        private void InitView()
        {
            pro_TextBlock.Text = _pro;
        }
    }
}
