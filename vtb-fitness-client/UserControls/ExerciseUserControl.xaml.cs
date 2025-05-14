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
using vtb_fitness_client.Network;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for ExerciseUserControl.xaml
    /// </summary>
    public partial class ExerciseUserControl : UserControl
    {
        public ExerciseType Type { get; set; }

        public List<Model.Exercise> _exercises = new();
        public int Meters = 0;
        public int Sits = 0;
        public int Reps = 0;
        public int Weight = 0;
        public string Exercise = "";

        public ExerciseUserControl(ExerciseType type)
        {
            InitializeComponent();
            Type = type;
            InitView();
        }

        private async void InitView()
        {
            switch (Type)
            {
                case ExerciseType.Cardio:
                    meters_TextBox.Visibility = Visibility.Visible;

                    _exercises = await ApiClient._Exercise.GetCardio();
                    break;
                case ExerciseType.Strength:
                    sits_TextBox.Visibility = Visibility.Visible;
                    reps_TextBox.Visibility = Visibility.Visible;
                    weight_TextBox.Visibility = Visibility.Visible;

                    _exercises = await ApiClient._Exercise.GetStrength();
                    break;
                case ExerciseType.OwnWeight:
                    sits_TextBox.Visibility = Visibility.Visible;
                    reps_TextBox .Visibility = Visibility.Visible;

                    _exercises = await ApiClient._Exercise.GetWeight();
                    break;
            }

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
                exercise_Image.Source = ImageHelper.GetImageFromPackPath((await ApiClient._Exercise.GetByName(item.ToString())).ImgName);
                Exercise = item.ToString();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb || tb.Tag is not string tag) return;

            if (!int.TryParse(tb.Text, out var value)) return;

            switch (tag)
            {
                case "meters":
                    Meters = value;
                    break;
                case "sits":
                    Sits = value;
                    break;
                case "reps":
                    Reps = value;
                    break;
                case "weight":
                    Weight = value;
                    break;
            }
        }

        private void VerifyNumericInput(object sender, TextCompositionEventArgs e)
        {
            if (!Helper.IsNumeric(e.Text)) e.Handled = true;
        }

        private void VerifyNumericPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                if (!Helper.IsNumeric(text))
                    e.CancelCommand();
            }
            else e.CancelCommand();
        }
    }
}
