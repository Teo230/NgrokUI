using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NgrokUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                MainWindowViewModel.ngrok.Close();
                System.Windows.Application.Current.Shutdown();
            }
            catch(Exception exc) { }
        }
    }
}
