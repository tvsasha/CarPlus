using CarPlus;
using Library_classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Car _car;
        private string _filePath = "cars.json";

        public AddCar(Car car = null)
        {
            InitializeComponent();
            _car = car ?? new Car();
            DataContext = _car;

            if (_car != null)
            {
                txtVIN.Text = _car.VIN;
                txtModel.Text = _car.Model;
                txtColor.Text = _car.Color;
                txtConfiguration.Text = _car.Configuration;
                txtPrice.Text = _car.Price.ToString();
                txtMileage.Text = _car.Mileage.ToString();
                cbStatus.SelectedValue = _car.Status;
                txtSellerName.Text = _car.SellerName;
                txtSellerPhone.Text = _car.SellerPhone;
                txtDescription.Text = _car.Description;
                if (!string.IsNullOrEmpty(_car.PhotoPath))
                {
                    imgPhoto.Source = new BitmapImage(new Uri(_car.PhotoPath, UriKind.RelativeOrAbsolute));
                }
            }

            Car.OnSellerNameChanged += UpdateSellerName;
            Car.OnSellerPhoneChanged += UpdateSellerPhone;

            UpdateSellerName(Login.CurrentUser.FullName);
            UpdateSellerPhone(Login.CurrentUser.Phone);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _car.VIN = txtVIN.Text;
            _car.Model = txtModel.Text;
            _car.Color = txtColor.Text;
            _car.Configuration = txtConfiguration.Text;
            _car.Price = decimal.Parse(txtPrice.Text);
            _car.Mileage = int.Parse(txtMileage.Text);
            _car.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
            _car.Description = txtDescription.Text;
            _car.SellerEmail = Login.CurrentUser.Email;
            _car.SellerName = txtSellerName.Text;
            _car.SellerPhone = txtSellerPhone.Text;

            if (_car.PhotoPath == null && imgPhoto.Source != null)
            {
                Library_brains.ManagePhoto.SavePhoto(_car);
                
            }

            SaveCar(_car);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void SaveCar(Car car)
        {
            List<Car> cars;
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
            }
            else
            {
                cars = new List<Car>();
            }

            cars.Add(car);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(cars, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Photo.AddPhoto(imgPhoto, _car);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void UpdateSellerName(string sellerName)
        {
            txtSellerName.Text = sellerName;
            _car.SellerName = sellerName;
        }

        private void UpdateSellerPhone(string sellerPhone)
        {
            txtSellerPhone.Text = sellerPhone;
            _car.SellerPhone = sellerPhone;
        }
    }
}
