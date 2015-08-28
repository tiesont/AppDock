using System;
//
using AppDock.DataServices;
//
using SimpleInjector;

namespace AppDock.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = Bootstrap();

            RunApplication(container);
        }

        private static Container Bootstrap()
        {
            var container = new Container();

            container.Register<IApplicationManager, ApplicationManager>();

            // Register your windows and view models:
            container.Register<MainWindow>();

            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();

                // InitializeComponent() must be called, or global resources (those defined in App.xaml) will not be loaded
                app.InitializeComponent();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                //Log the exception and exit
            }
        }
    }
}
