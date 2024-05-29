using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPlus;
using System.Windows;

namespace CarPlusWPF
{
    public static class WindowsHelper
    {
        public static void OpenMainWindow(Window currentWindow)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            currentWindow.Close();
        }
    }
}
