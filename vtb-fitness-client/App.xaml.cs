using System.Configuration;
using System.Data;
using System.Windows;
using vtb_fitness_client.Model;

namespace vtb_fitness_client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
    }

}
