using CarPlus;
using Library_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarPlusWPF
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private string carsFilePath = "cars.json";
        private string usersFilePath = "users.json";
        private List<Car> allCars;
        private List<User> allUsers;

        public AdminPanel()
        {
            InitializeComponent();
            LoadAllCars();
            LoadAllUsers();
        }

        private void LoadAllCars()
        {
            if (System.IO.File.Exists(carsFilePath))
            {
                var json = System.IO.File.ReadAllText(carsFilePath);
                allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
                lvAllCars.ItemsSource = allCars;
            }
        }

        private void LoadAllUsers()
        {
            if (System.IO.File.Exists(usersFilePath))
            {
                var json = System.IO.File.ReadAllText(usersFilePath);
                allUsers = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                lvAllUsers.ItemsSource = allUsers;
            }
        }

        private void LvAllCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                return;
            }
        }

        private void LvCars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                CarDetails carDetailsWindow = new CarDetails(selectedCar);
                carDetailsWindow.Topmost = true;
                carDetailsWindow.Show();
                carDetailsWindow.Topmost = false;
            }
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                EditCarAdmin editCarWindow = new EditCarAdmin(selectedCar);
                editCarWindow.ShowDialog();
                RefreshCarList();
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllUsers.SelectedItem is User selectedUser)
            {
                EditUserAdmin editUserAdmin = new EditUserAdmin(selectedUser);
                editUserAdmin.ShowDialog();
                RefreshUserList();
            }
        }

        private void DeleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                allCars.Remove(selectedCar);
                SaveAllCars();
                RefreshCarList();
            }
        }

        private void SaveAllCars()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(allCars, options);
            System.IO.File.WriteAllText(carsFilePath, json);
        }

        private void LvAllUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllUsers.SelectedItem is User selectedUser)
            {
                allUsers.Remove(selectedUser);
                SaveAllUsers();
                RefreshUserList();
            }
        }

        private void RefreshUserList()
        {
            lvAllUsers.ItemsSource = null;
            lvAllUsers.ItemsSource = allUsers;
        }

        private void RefreshCarList()
        {
            lvAllCars.ItemsSource = null;
            lvAllCars.ItemsSource = allCars;
        }

        private void SaveAllUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(allUsers, options);
            System.IO.File.WriteAllText(usersFilePath, json);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsHelper.OpenMainWindow(this);
        }
    }
}

