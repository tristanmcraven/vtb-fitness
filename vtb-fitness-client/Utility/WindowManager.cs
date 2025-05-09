using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            if (ActiveWindow != null)
            {
                var body = ActiveWindow.FindName("body") as Grid;

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

        }

        public static void RemoveTintFromActiveWindow()
        {
            if (ActiveWindow != null)
            {
                var body = ActiveWindow.FindName("body") as Grid;

                if (body != null)
                {
                    var tint = body.Children.OfType<Border>().FirstOrDefault(b => b.Name == "tint");
                    if (tint != null)
                        body.Children.Remove(tint);
                }
            }
        }

        public static T Get<T>() where T : Window => Application.Current.Windows.OfType<T>().First();

        public static void Close<T>() where T : Window => Application.Current.Windows.OfType<T>().First().Close();
    }
}
