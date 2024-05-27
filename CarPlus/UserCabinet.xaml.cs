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
                _userCars = allCars.Where(c => c.SellerName == Login.CurrentUser.Email).ToList();
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
    }
}
