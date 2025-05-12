using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for TrackerDayUserControl.xaml
    /// </summary>
    public partial class TrackerDayUserControl : UserControl
    {
        private List<Tracker> _exercises;

        private Tracker? _maxCardio;
        private Tracker? _maxStrength;
        private Tracker? _maxWeight;
        public TrackerDayUserControl(List<Tracker> exercises)
        {
            InitializeComponent();
            _exercises = exercises;
            InitView();
        }

        //nuke this later so no one would see this POZOR
        private void InitView()
        {
            var first = _exercises.First();
            day_TextBlock.Text = $"{first.Timestamp.Day} {new CultureInfo("ru-RU").DateTimeFormat.GetAbbreviatedMonthName(first.Timestamp.Month)}";

            _maxCardio = _exercises.Where(x => x.Exercise.TypeId == 1)
                        .OrderByDescending(x => x.Meters)
                        .FirstOrDefault();
            _maxStrength = _exercises.Where(x => x.Exercise.TypeId == 2 || x.Exercise.TypeId == 3)
                                     .OrderByDescending(x => (x.Sits * x.Reps * x.Weight))
                                     .FirstOrDefault();
            _maxWeight = _exercises.Where(x => x.Exercise.TypeId == 4)
                                   .OrderByDescending(x => (x.Sits * x.Reps * x.Weight))
                                   .FirstOrDefault();

            if (_maxCardio == null)
            {
                noCardio_Grid.Visibility = Visibility.Visible;
                cardio_Grid.Visibility = Visibility.Collapsed;
            }
            else
            {
                cardio_Image.Source = ImageHelper.GetImageFromPackPath(_maxCardio.Exercise.ImgName);
                cardioName_TextBlock.Text = _maxCardio.Exercise.Name;
                cardioResult_TextBlock.Text = _maxCardio.Meters.ToString() + " м";
                noCardio_Grid.Visibility = Visibility.Collapsed;
            }

            if (_maxStrength == null)
            {
                noStrength_Grid.Visibility = Visibility.Visible;
                strength_Grid.Visibility = Visibility.Collapsed;
            }
            else
            {
                strength_Image.Source = ImageHelper.GetImageFromPackPath(_maxStrength.Exercise.ImgName);
                strengthName_TextBlock.Text = _maxStrength.Exercise.Name;
                strengthResult_TextBlock.Text = $"{_maxStrength.Sits} x {_maxStrength.Reps} ({_maxStrength.Weight} кг)";
                noStrength_Grid.Visibility = Visibility.Collapsed;
            }

            if (_maxWeight == null)
            {
                noWeight_Grid.Visibility = Visibility.Visible;
                weight_Grid.Visibility = Visibility.Collapsed;
            }
            else
            {
                weight_Image.Source = ImageHelper.GetImageFromPackPath(_maxWeight.Exercise.ImgName);
                weightName_TextBlock.Text = _maxWeight.Exercise.Name;
                weightResult_TextBlock.Text = $"{_maxWeight.Sits} x {_maxWeight.Reps}";
                noWeight_Grid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
