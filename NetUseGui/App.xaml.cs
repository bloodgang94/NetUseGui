using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NetUseGui.ViewModel;

namespace NetUseGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            var viewModel = new NetUseVm();
            MainWindow = new MainWindow() { DataContext = viewModel };
            MainWindow.Show();
            Task.Run(async()=>await viewModel.OnSearch());
        }

    }
}
