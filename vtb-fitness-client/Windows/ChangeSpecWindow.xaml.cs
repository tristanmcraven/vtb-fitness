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
using vtb_fitness_client.Model;
using vtb_fitness_client.Dto;
using vtb_fitness_client.Network;
using vtb_fitness_client.UserControls;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for ChangeSpecWindow.xaml
    /// </summary>
    public partial class ChangeSpecWindow : CustomWindow
    {
        public ChangeSpecWindow()
        {
            InitializeComponent();
            Owner = WindowManager.ActiveWindow;
            InitView();
        }

        //sorry idc about that waraga ^^
        private async void InitView()
        {
            var specs = await ApiClient._Spec.Get();
            var currentTrainerSpecs = await ApiClient._TrainerSpecs.GetTrainerSpecs(App.CurrentUser.Id);

            foreach (var spec in specs)
            {
                spec1_ComboBox.Items.Add(new SpecUserControl(spec));
                spec2_ComboBox.Items.Add(new SpecUserControl(spec));
                spec3_ComboBox.Items.Add(new SpecUserControl(spec));
            }
            spec1_ComboBox.Items.Insert(0, new TextBlock { Text = "< Нет >" });
            spec2_ComboBox.Items.Insert(0, new TextBlock { Text = "< Нет >" });
            spec3_ComboBox.Items.Insert(0, new TextBlock { Text = "< Нет >" });
            spec1_ComboBox.SelectedIndex = 0;
            spec2_ComboBox.SelectedIndex = 0;
            spec3_ComboBox.SelectedIndex = 0;

            for (int i = 1; i <= currentTrainerSpecs.Count; i++)
            {
                var cb = FindName($"spec{i}_ComboBox") as ComboBox;
                cb.SelectedIndex = currentTrainerSpecs[i-1].Id;
            }
        }

        private async void confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecIds = new List<int>();
            for (int i = 1; i <= 3; i++)
            {
                var cb = FindName($"spec{i}_ComboBox") as ComboBox;
                if (cb.SelectedIndex != 0)
                {
                    selectedSpecIds.Add(cb.SelectedIndex);
                }
            }

            var result = await ApiClient._TrainerSpecs.Post(new TrainerSpecPostDto
            {
                TrainerId = App.CurrentUser.Id,
                SpecIds = selectedSpecIds
            });

            if (result != null) Close();
            else new DialogWindow().ShowDialog();
        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var senderCb = (sender as ComboBox)!;
            var cbs = new List<ComboBox>
            {
                spec1_ComboBox,
                spec2_ComboBox,
                spec3_ComboBox
            };

            foreach (var cb in cbs)
            {
                if (cb != senderCb)
                {
                    if (senderCb.SelectedIndex != 0 && cb.SelectedIndex != 0)
                    {
                        if (senderCb.SelectedIndex == cb.SelectedIndex)
                        {
                            new DialogWindow(DialogWindowType.Error, "Уже выбрано").ShowDialog();
                            senderCb.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
    }
}
