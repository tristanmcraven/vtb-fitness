using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace vtb_fitness_client.Utility
{
    public static class WindowManager
    {
        private static Stack<Window> _windowStack = new();
        public static Window? ActiveWindow => _windowStack.Count > 0 ? _windowStack.Peek() : null;

        public static void Register(Window window)
        {
            AddTintToActiveWindow();
            if (!_windowStack.Contains(window)) _windowStack.Push(window);
        }

        public static void Unregister(Window window)
        {
            if (_windowStack.Count > 0 && _windowStack.Peek() == window) _windowStack.Pop();
            else _windowStack = new Stack<Window>(_windowStack.Where(w => w != window).Reverse()); //rebuild ts
            RemoveTintFromActiveWindow();
        }

        public static void AddTintToActiveWindow()
        {
            if (ActiveWindow?.FindName("body") is Grid body)
            {
                var tint = new Border
                {
                    Name = "tint",
                    Background = new SolidColorBrush(Colors.Black)
                    {
                        Opacity = 0.0
                    }
                };

                Grid.SetColumnSpan(tint, 10);
                Grid.SetRowSpan(tint, 10);
                Panel.SetZIndex(tint, 2);
                body.Children.Add(tint);

                var animation = new DoubleAnimation
                {
                    From = 0.0,
                    To = 0.8,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
                };

                tint.Background.BeginAnimation(SolidColorBrush.OpacityProperty, animation);
            }
        }

        public static void RemoveTintFromActiveWindow()
        {
            if (ActiveWindow?.FindName("body") is Grid body)
            {
                var tint = body.Children.OfType<Border>().FirstOrDefault(b => b.Name == "tint");
                if (tint != null)
                {
                    var animation = new DoubleAnimation
                    {
                        From = tint.Opacity,
                        To = 0.0,
                        Duration = TimeSpan.FromMilliseconds(150),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                    };

                    animation.Completed += async (s, e) =>
                    {
                        await Task.Delay(30);
                        body.Children.Remove(tint);
                    };
                    tint.Background.BeginAnimation(SolidColorBrush.OpacityProperty, animation);
                }
            }
        }

        public static T Get<T>() where T : Window => Application.Current.Windows.OfType<T>().First();

        public static void Close<T>() where T : Window => Application.Current.Windows.OfType<T>().First().Close();
    }
}
