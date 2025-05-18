using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace BuddyProductivityAssistant
{
    public partial class App : Application
    {
        //Create a instance of the MainWindow to use in the following methods
        private MainWindow _mainWindow;

        //Manually shows the Window and increment in the MainWindow_Closing
        //And creates a Tray icon to use in the other methods
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var trayIcon = (TaskbarIcon)FindResource("TrayIcon");

            _mainWindow = new MainWindow();
            _mainWindow.Closing += MainWindow_Closing;
            _mainWindow.Show();

        }
        //Prevents the exit of the application and simply closes the window, maintaining the application life
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            _mainWindow.Hide();
        }
        //When in the Tray Menu, opens the Window again
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.Activate();
        }
        //Closes the application for good, tho you dont want the buddy to die, right?
        //So you never going to use it :)
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            var trayIcon = (TaskbarIcon)Resources["TrayIcon"];
            trayIcon.Dispose();

            if (_mainWindow != null)
            {
                _mainWindow.Closing -= MainWindow_Closing;
                _mainWindow.Close();
            }
            Shutdown();
        }
    }
}
