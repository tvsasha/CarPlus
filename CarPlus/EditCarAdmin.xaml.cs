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
                txtSellerName.Text = _car.SellerName;
                txtDescription.Text = _car.Description;
                if (!string.IsNullOrEmpty(_car.PhotoPath))
                {
                    imgPhoto.Source = new BitmapImage(new Uri(_car.PhotoPath, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _car.VIN = txtVIN.Text;
            _car.Model = txtModel.Text;
            _car.Color = txtColor.Text;
            _car.Configuration = txtConfiguration.Text;
            _car.Price = decimal.Parse(txtPrice.Text);
            _car.SellerName = txtSellerName.Text;
            _car.Description = txtDescription.Text;

            if (_car.PhotoPath == null && imgPhoto.Source != null)
            {
                SavePhoto();
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
                carToUpdate.Model = _car.Model;
                carToUpdate.Color = _car.Color;
                carToUpdate.Configuration = _car.Configuration;
                carToUpdate.Price = _car.Price;
                carToUpdate.SellerName = _car.SellerName;
                carToUpdate.Description = _car.Description;
                carToUpdate.PhotoPath = _car.PhotoPath;
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(allCars, options);
            File.WriteAllText(_carsFilePath, json);
        }

        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                _car.PhotoPath = openFileDialog.FileName;
            }
        }

        private void SavePhoto()
        {
            string imagesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            string photoFileName = $"{_car.VIN}.jpg";
            string photoPath = System.IO.Path.Combine(imagesPath, photoFileName);

            _car.PhotoPath = photoPath;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
