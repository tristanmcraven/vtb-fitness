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
            AddTintToSenderWindow();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveTintFromSenderWindow();
        }

        private void AddTintToSenderWindow()
        {
            var body = _senderWindow.FindName("body") as Grid;

            if (body != null)
            {
                var tint = new Border
                {
                    Background = new SolidColorBrush(Colors.Black),
                    Name = "tint"
                };
                tint.Background.Opacity = 0.8;
                Grid.SetColumnSpan(body, 10);
                Grid.SetRowSpan(body, 10);
                Panel.SetZIndex(body, 2);

                body.Children.Add(tint);
            }
        }

        private void RemoveTintFromSenderWindow()
        {
            var body = _senderWindow.FindName("body") as Grid;

            if (body != null)
            {
                var tint = body.Children.OfType<Border>().FirstOrDefault(b => b.Name == "tint");
                if (tint != null)
                    body.Children.Remove(tint);
            }
        }

        private async void addTracker_Button_Click(object sender, RoutedEventArgs e)
        {
            var exercises = new List<TrackerCreateDto>();

            var date = datePicker.SelectedDate ?? DateTime.Now;

            var cardioChildren = cardio_StackPanel.Children;
            if (cardioChildren.Count > 0)
            {
                foreach (var item in cardioChildren)
                {
                    var uc = item as CardioUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Meters = uc.Meters,
                        TimeStamp = date
                    });
                }
            }

            var strengthChildren = strength_StackPanel.Children;
            if (strengthChildren.Count > 0)
            {
                foreach (var item in strengthChildren)
                {
                    var uc = item as StrengthUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Sits = uc.Sits,
                        Reps = uc.Reps,
                        TimeStamp = date,
                        Weight = uc.Weight
                    });
                }
            }

            var weightChildren = weight_StackPanel.Children;
            if (weightChildren.Count > 0)
            {
                foreach (var item in weightChildren)
                {
                    var uc = item as WeightUserControl;
                    exercises.Add(new TrackerCreateDto
                    {
                        UserId = App.CurrentUser.Id,
                        ExerciseId = (await ApiClient._Exercise.GetByName(uc.Exercise)).Id,
                        Sits = uc.Sits,
                        Reps = uc.Reps,
                        TimeStamp = date
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
        }

        private void goBack_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCardio_Button_Click(object sender, RoutedEventArgs e)
        {
            cardio_StackPanel.Children.Add(new CardioUserControl());
        }

        private void addStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            strength_StackPanel.Children.Add(new StrengthUserControl());
        }

        private void addWeight_Button_Click(object sender, RoutedEventArgs e)
        {
            weight_StackPanel.Children.Add(new WeightUserControl());
        }

        private void addDate_Button_Click(object sender, RoutedEventArgs e)
        {
            addDate_Button.Visibility = Visibility.Collapsed;
            datePicker.Visibility = Visibility.Visible;
        }
    }
}
