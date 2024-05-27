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
    /// Логика взаимодействия для UserCabinet.xaml
    /// </summary>
    public partial class UserCabinet : Window
    {
        private string _filePath = "cars.json";
        private List<Car> _userCars;

        public UserCabinet()
        {
            InitializeComponent();
            LoadUserCars();
            lvUserCars.ItemsSource = _userCars;
        }
        private void LoadUserCars()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                var allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
                _userCars = allCars.Where(c => c.SellerName == Login.CurrentUser.FullName).ToList();
            }
            else
            {
                _userCars = new List<Car>();
            }
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCarWindow = new AddCar();
            addCarWindow.Show();
            Close();
        }

        private void LvUserCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvUserCars.SelectedItem is Car selectedCar)
            {
                CarDetails carDetailsWindow = new CarDetails(selectedCar);
                carDetailsWindow.Show();
            }
        }

        private void EditCar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvUserCars.SelectedItem is Car selectedCar)
            {
                EditCar editCarWindow = new EditCar(selectedCar);
                editCarWindow.Show();
                Close();
            }
        }

        private void DeleteCar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvUserCars.SelectedItem is Car selectedCar)
            {
                _userCars.Remove(selectedCar);
                SaveAllCars();
                RefreshUserCars();
            }
        }

        private void SaveAllCars()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                var allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
                var remainingCars = allCars.Where(c => c.SellerName != Login.CurrentUser.FullName).ToList();
                remainingCars.AddRange(_userCars);
                var options = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(remainingCars, options);
                System.IO.File.WriteAllText(_filePath, json);
            }
        }

        private void RefreshUserCars()
        {
            lvUserCars.ItemsSource = null;
            lvUserCars.ItemsSource = _userCars;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
