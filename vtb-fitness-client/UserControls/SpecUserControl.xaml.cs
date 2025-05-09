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

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for SpecUserControl.xaml
    /// </summary>
    public partial class SpecUserControl : UserControl
    {
        private Spec _spec;
        public SpecUserControl(Spec spec)
        {
            InitializeComponent();
            _spec = spec;
            InitView();
        }

        private void InitView()
        {
            spec_TextBlock.Text = _spec.Name;
        }
    }
}
