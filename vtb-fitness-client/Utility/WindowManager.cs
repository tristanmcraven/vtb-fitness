using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace vtb_fitness_client.Utility
{
    public static class WindowManager
    {
        private static Stack<Window> _windowStack = new();
        public static Window? ActiveWindow => _windowStack.Count > 0 ? _windowStack.Peek() : null;
        public static void Register(Window window)
        {
            if (!_windowStack.Contains(window)) _windowStack.Push(window);
        }
        public static void Unregister(Window window)
        {
            if (_windowStack.Count > 0 && _windowStack.Peek() == window) _windowStack.Pop();
            else _windowStack = new Stack<Window>(_windowStack.Where(w => w != window).Reverse()); //rebuild ts
        }

        public static T Get<T>() where T : Window => Application.Current.Windows.OfType<T>().First();

        public static void Close<T>() where T : Window => Application.Current.Windows.OfType<T>().First().Close();
    }
}
