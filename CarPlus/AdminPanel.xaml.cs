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
               
            }
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                AddCar addCarWindow = new AddCar(selectedCar);
                addCarWindow.Show();
                Close();
            }
        }

        private void DeleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllCars.SelectedItem is Car selectedCar)
            {
                allCars.Remove(selectedCar);
                SaveAllCars();
                lvAllCars.ItemsSource = null;
                lvAllCars.ItemsSource = allCars;
            }
        }

        private void SaveAllCars()
        {
            var json = JsonSerializer.Serialize(allCars);
            System.IO.File.WriteAllText(carsFilePath, json);
        }

        private void LvAllUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle user selection change if needed
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllUsers.SelectedItem is User selectedUser)
            {
                Register registerWindow = new Register(selectedUser);
                registerWindow.Show();
                Close();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllUsers.SelectedItem is User selectedUser)
            {
                allUsers.Remove(selectedUser);
                SaveAllUsers();
                lvAllUsers.ItemsSource = null;
                lvAllUsers.ItemsSource = allUsers;
            }
        }

        private void SaveAllUsers()
        {
            var json = JsonSerializer.Serialize(allUsers);
            System.IO.File.WriteAllText(usersFilePath, json);
        }
    }
}

