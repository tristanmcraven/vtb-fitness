using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using vtb_fitness_client.Network;
using Microsoft.Xaml.Behaviors.Media;
using vtb_fitness_client.Model;
using System.Windows.Controls;

namespace vtb_fitness_client.Utility
{
    public static class Helper
    {
        public static async Task<int> GetUserSalePercent(int id)
        {
            var user = await ApiClient._User.GetById(id);
            var since = user.WorkingInVtbSince;

            if (since != null)
            {
                var sinceDate = since.Value;
                var workingYears = DateOnly.FromDateTime(DateTime.Now).Year - sinceDate.Year;

                if (workingYears > 5)
                    return 50;
                return workingYears switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 20,
                    4 => 35,
                    5 => 50,
                    _ => 0
                };
            }

            return 0;
        }

        public static double GetDiscountedPrice(double price, int salePercent)
        {
            return Math.Round(price * ((100 - salePercent) / 100.0), 2);
        }

        public static int GetDiscountedPriceAsInteger(double price, int salePercent)
        {
            return Convert.ToInt32(Math.Round(price * ((100 - salePercent) / 100.0), 0));
        }

        public static string GetDiscountedPriceAsString(double price, int salePercent)
        {
            return Math.Round(price * ((100 - salePercent) / 100.0), 0).ToString();
        }

        public static bool IsNumeric(string value) => int.TryParse(value, out _);

        public static bool IsEmailCorrect(string value) => MailAddress.TryCreate(value, out _);

        public static bool IsDateValid(DateTime? value)
        {
            if (value == null) return false;

            var date = value.Value.Date;

            return date <= DateTime.Today && date.Year >= 1900;
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        public static string GetTariffDuration(Tariff tariff)
        {
            var duration = tariff.Period.Value.TotalHours;
            return duration.ToString();
        }

        public static string GetFacilityStatus(TimeOnly? startTime, TimeOnly? endTime, TextBlock tb)
        {
            var value = "";
            var now = TimeOnly.FromDateTime(DateTime.Now);

            if (startTime == null || endTime == null)
            {
                value = "Недоступно";
                tb.Background = new SolidColorBrush(Colors.Orange);
            }

            else if (startTime == endTime)
            {
                value = "Можно (безлимит)";
                tb.Background = new SolidColorBrush(Colors.Green);
            }

            else if (startTime < now && endTime > now)
            {
                value = $"Можно (до {endTime})";
                tb.Background = new SolidColorBrush(Colors.Green);
            }

            else
            {
                value = $"Пока нет (с {startTime})";
                tb.Background = new SolidColorBrush(Colors.Red);
            }

            return value;

        }

        public static string GetTrainerStatus(int? workouts, TextBlock tb, int? totalWorkouts = null)
        {
            var value = "";
            if (workouts == null)
            {
                value = "Недоступно";
                tb.Background = new SolidColorBrush(Colors.DarkOrange);
            }
            else
            {
                if (totalWorkouts != null && totalWorkouts == 666)
                {
                    value = $"Безлимит";
                    tb.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    value = $"{workouts} {DeclineWorkoutWord((int)workouts)}";
                    if (workouts == 0)
                        tb.Background = new SolidColorBrush(Colors.Red);
                    else
                        tb.Background = new SolidColorBrush(Colors.Green);
                }
            }
            return value;

        }

        private static string DeclineWorkoutWord(int workouts)
        {
            return workouts switch
            {
                1 => "занятие",
                2 => "занятия",
                3 => "занятия",
                4 => "занятия",
                _ => "занятий"
            };
        }
    }
}
