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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        private string _filePath = "cars.json";
        private List<Car> _cars;

        public AddCar()
        {
            InitializeComponent();

            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                _cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
            }
            else
            {
                _cars = new List<Car>();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string vin = txtVIN.Text;
            string model = txtModel.Text;
            string color = txtColor.Text;
            string configuration = txtConfiguration.Text;
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Пожалуйста, введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string sellerName = txtSellerName.Text;
            string description = txtDescription.Text;

            if (string.IsNullOrWhiteSpace(vin) ||
                string.IsNullOrWhiteSpace(model) ||
                string.IsNullOrWhiteSpace(color) ||
                string.IsNullOrWhiteSpace(configuration) ||
                string.IsNullOrWhiteSpace(sellerName) ||
                string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Car newCar = new Car
            {
                VIN = vin,
                Model = model,
                Color = color,
                Configuration = configuration,
                Price = price,
                SellerName = sellerName,
                Description = description
            };

            _cars.Add(newCar);
            SaveCars();

            MessageBox.Show("Автомобиль успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void SaveCars()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_cars, options);
            System.IO.File.WriteAllText(_filePath, json);
        }
    }
}
