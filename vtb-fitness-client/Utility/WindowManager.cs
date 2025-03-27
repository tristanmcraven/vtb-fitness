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
        public static T GetWindow<T>() where T : Window => Application.Current.Windows.OfType<T>().First();

        public static void Close<T>() where T : Window => Application.Current.Windows.OfType<T>().First().Close();
    }
}
