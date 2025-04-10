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
using vtb_fitness_client.Network;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for WeightUserControl.xaml
    /// </summary>
    public partial class WeightUserControl : UserControl
    {
        private List<Exercise> _exercises = new();

        public int Sits = 0;
        public int Reps = 0;

        public string Exercise = "";
        public WeightUserControl()
        {
            InitializeComponent();
            InitView();
        }

        private async void InitView()
        {
            _exercises = await ApiClient._Exercise.GetWeight();
            foreach (var e in _exercises)
            {
                exercises_ComboBox.Items.Add(e.Name);
            }
        }

        private async void exercises_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = exercises_ComboBox.SelectedItem;
            if (item != null)
            {
                weight_Image.Source = ImageHelper.GetImageFromPackPath((await ApiClient._Exercise.GetByName(item.ToString())).ImgName);
                Exercise = item.ToString();
            }
        }

        private void sits_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sits = int.Parse(sits_TextBox.Text);
        }

        private void reps_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reps = int.Parse(reps_TextBox.Text);
        }
    }
}
