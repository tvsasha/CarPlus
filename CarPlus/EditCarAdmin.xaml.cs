using Library_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CarPlusWPF
{
    /// <summary>
    /// Логика взаимодействия для EditCarAdmin.xaml
    /// </summary>
    public partial class EditCarAdmin : Window
    {
        private Car _car;
        private string _carsFilePath = "cars.json";

        public EditCarAdmin(Car car)
        {
            InitializeComponent();
            _car = car;
            DataContext = _car;

            if (_car != null)
            {
                txtVIN.Text = _car.VIN;
                txtModel.Text = _car.Model;
                txtColor.Text = _car.Color;
                txtConfiguration.Text = _car.Configuration;
                txtPrice.Text = _car.Price.ToString();
                txtStatus.Text = _car.Status;
                txtMileage.Text = _car.Mileage.ToString();
                txtSellerName.Text = _car.SellerName;
                txtSellerPhone.Text = _car.SellerPhone;
                txtDescription.Text = _car.Description;
                Photo.OpenPhoto(_car.PhotoPath, imgPhoto);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _car.VIN = txtVIN.Text;
            _car.Model = txtModel.Text;
            _car.Color = txtColor.Text;
            _car.Configuration = txtConfiguration.Text;
            _car.Price = decimal.Parse(txtPrice.Text);
            _car.Status = txtStatus.Text;
            _car.Mileage = int.Parse(txtMileage.Text);
            _car.SellerName = txtSellerName.Text;
            _car.SellerPhone = txtSellerPhone.Text;
            _car.Description = txtDescription.Text;

            if (_car.PhotoPath == null && imgPhoto.Source != null)
            {
                Library_brains.ManagePhoto.SavePhoto(_car);
            }
            SaveAllCars();
            Close();
        }

        private void SaveAllCars()
        {
            var json = File.ReadAllText(_carsFilePath);
            var allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();

            var carToUpdate = allCars.FirstOrDefault(c => c.VIN == _car.VIN);
            if (carToUpdate != null)
            {
                carToUpdate.VIN = _car.VIN;
                carToUpdate.Model = _car.Model;
                carToUpdate.Color = _car.Color;
                carToUpdate.Configuration = _car.Configuration;
                carToUpdate.Price = _car.Price;
                carToUpdate.Status = _car.Status;
                carToUpdate.Mileage = _car.Mileage;
                carToUpdate.SellerName = _car.SellerName;
                carToUpdate.SellerPhone = _car.SellerPhone;
                carToUpdate.Description = _car.Description;
                carToUpdate.PhotoPath = _car.PhotoPath;
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(allCars, options);
            File.WriteAllText(_carsFilePath, json);
        }

        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Photo.AddPhoto(imgPhoto, _car);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
