using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//
using AppDock.DataServices;
using AppDock.Entities;
//
using MahApps.Metro.Controls;

namespace AppDock.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        internal IApplicationManager MyAppManager { get; private set; }


        public MainWindow(IApplicationManager manager)
        {
            InitializeComponent();

            MyAppManager = manager;

            this.Title = GetAssemblyInfo("ProductName");
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadApplicationsForThisUser();
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Exit();
        }


        private void ManageApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = AppSettings.DatabasePath;
                if (path.Equals("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
                {
                    path = AppSettings.DatabaseName;
                }
                else
                {
                    path = string.Format(@"{0}\{1}", AppSettings.DatabasePath, AppSettings.DatabaseName);
                }

                Task.Run(() => Process.Start(path));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ReloadApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            LoadApplicationsForThisUser();
        }


        // Utility methods

        private string GetAssemblyInfo(string key)
        {
            string info = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            switch (key)
            {
                case "CompanyName":
                    info = fvi.CompanyName;
                    break;

                case "ProductName":
                    info = fvi.ProductName;
                    break;

                case "ProductVersion":
                    info = fvi.ProductVersion;
                    break;
            }

            return info;
        }

        private void LoadApplicationsForThisUser(bool activeOnly = true)
        {
            try
            {
                var labApps = MyAppManager.GetApplications(activeOnly);
                if (labApps != null)
                {
                    ApplicationsItemsControl.ItemsSource = labApps
                        .Select(node => new CommandData
                        {
                            Label = node.Label,
                            Target = node.Path,
                            StartInPath = node.StartInPath,
                            Type = (ApplicationType)node.TypeID
                        })
                        .OrderBy(node => node.Label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
