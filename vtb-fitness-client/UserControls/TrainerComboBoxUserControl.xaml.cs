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

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TrainerComboBoxUserControl.xaml
    /// </summary>
    public partial class TrainerComboBoxUserControl : UserControl
    {
        private User? _trainer;
        public TrainerComboBoxUserControl(User? trainer)
        {
            InitializeComponent();
            _trainer = trainer;
            InitView();
        }

        private void InitView()
        {
            if (_trainer != null)
            {
                pfp.Source = ImageHelper.GetImage(_trainer.Pfp);
                trainerName_TextBlock.Text = $"{_trainer.Lastname} {_trainer.Name} {_trainer.Middlename}";
            }
            else
            {
                pfp.Visibility = Visibility.Collapsed;
                trainerName_TextBlock.Visibility = Visibility.Collapsed;

                noTrainer_TextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
