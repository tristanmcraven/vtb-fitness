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
using vtb_fitness_client.Dto;
using vtb_fitness_client.Model;
using vtb_fitness_client.Network;
using vtb_fitness_client.Pages;
using vtb_fitness_client.UserControls;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Windows
{
    /// <summary>
    /// Interaction logic for AddTrackerWindow.xaml
    /// </summary>
    public partial class AddTrackerWindow : CustomWindow
    {
        private Window _senderWindow;
        public AddTrackerWindow(Window senderWindow)
        {
            InitializeComponent();
            _senderWindow = senderWindow;
            InitView();
        }

        private void InitView()
        {
            RefreshTrainers();
        }

        private async void RefreshTrainers()
        {
            var trainers = (await ApiClient._User.Get()).Where(x => x.RoleId == 4).ToList();

            foreach (var t in trainers)
            {
                trainer_ComboBox.Items.Add(new TrainerComboBoxUserControl(t));
            }
            trainer_ComboBox.Items.Insert(0, new TrainerComboBoxUserControl(null));

            trainer_ComboBox.SelectedIndex = 0;

            var currentTrainer = await ApiClient._User.GetTrainer(App.CurrentUser.Id);
            if (currentTrainer != null)
            {
                foreach (var item in trainer_ComboBox.Items)
                {
                    if (item is TrainerComboBoxUserControl)
                    {
                        var tcbuc = (TrainerComboBoxUserControl)item;
                        if (tcbuc._trainer != null && tcbuc._trainer.Id == currentTrainer.Id)
                        {
                            trainer_ComboBox.SelectedItem = item;
                        }
                    }
                }
            }

            var currentTariff = (await ApiClient._User.GetCurrentTariff(App.CurrentUser.Id))!.Tariff!;
            if (currentTariff != null)
            {
                if (currentTariff.TrainerWorkoutsPerWeek == null || currentTariff.TrainerWorkoutsPerWeek < 1)
                {
                    trainer_ComboBox.Visibility = Visibility.Collapsed;
                    noTrainer_TextBlock.Visibility = Visibility.Visible;
                }
            }
        }

        private async void addTracker_Button_Click(object sender, RoutedEventArgs e)
        {
            var exercises = new List<TrackerCreateDto>();

            var timestamp = datePicker.SelectedDate ?? DateTime.Now;

            var trainerId = GetSelectedTrainerId();

            var cardioChildren = cardio_StackPanel.Children;
            if (cardioChildren.Count > 0)
            {
                foreach (var item in cardioChildren)
                {
                    var uc = item as ExerciseUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Meters = uc.Meters,
                        TimeStamp = timestamp,
                        TrainerId = trainerId ?? null
                    });
                }
            }

            var strengthChildren = strength_StackPanel.Children;
            if (strengthChildren.Count > 0)
            {
                foreach (var item in strengthChildren)
                {
                    var uc = item as ExerciseUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Sits = uc.Sits,
                        Reps = uc.Reps,
                        TimeStamp = timestamp,
                        Weight = uc.Weight,
                        TrainerId = trainerId ?? null
                    });
                }
            }

            var weightChildren = weight_StackPanel.Children;
            if (weightChildren.Count > 0)
            {
                foreach (var item in weightChildren)
                {
                    var uc = item as ExerciseUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Sits = uc.Sits,
                        Reps = uc.Reps,
                        TimeStamp = timestamp,
                        TrainerId = trainerId ?? null
                    });
                }
            }

            if (!exercises.Any())
            {
                new DialogWindow(DialogWindowType.Error, "Пожалуйста, добавьте хотя бы одно упражнение").ShowDialog();
                return;
            }

            foreach (var item in exercises)
            {
                await ApiClient._Tracker.Create(item);
            }

            Close();
            PageManager.MainFrame.Navigate(new TrackerPage());
            WindowManager.Get<MainWindow>().UpdateCurrentTariffData();
        }

        private int? GetSelectedTrainerId()
        {
            var tcbuc = trainer_ComboBox.SelectedItem as TrainerComboBoxUserControl;
            if (tcbuc != null && tcbuc._trainer != null)
                return tcbuc._trainer.Id;
            return null;
        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCardio_Button_Click(object sender, RoutedEventArgs e)
        {
            cardio_StackPanel.Children.Add(new ExerciseUserControl(Utility.ExerciseType.Cardio));
        }

        private void addStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            strength_StackPanel.Children.Add(new ExerciseUserControl(Utility.ExerciseType.Strength));
        }

        private void addWeight_Button_Click(object sender, RoutedEventArgs e)
        {
            weight_StackPanel.Children.Add(new ExerciseUserControl(Utility.ExerciseType.OwnWeight));
        }

        private void addDate_Button_Click(object sender, RoutedEventArgs e)
        {
            addDate_Button.Visibility = Visibility.Collapsed;
            datePicker.Visibility = Visibility.Visible;
        }
    }
}
