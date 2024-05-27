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
    /// Логика взаимодействия для EditCar.xaml
    /// </summary>
    public partial class EditCar : Window
    {
        private Car _car;
        private string _carsFilePath = "cars.json";
        private string _originalVIN;

        public EditCar(Car car)
        {
            InitializeComponent();
            _car = car;
            _originalVIN = car.VIN;
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
            _car.Status = txtStatus.Text;
            _car.Mileage = int.Parse(txtMileage.Text);
            _car.SellerName = txtSellerName.Text;
            _car.SellerPhone = txtSellerPhone.Text;
            _car.Description = txtDescription.Text;

            if (_car.PhotoPath == null && imgPhoto.Source != null)
            {
                SavePhoto();
            }
            SaveAllCars();
            Close();
            UserCabinet userCabinet = new UserCabinet();
            userCabinet.Show();
        }

        private void SaveAllCars()
        {
            var json = System.IO.File.ReadAllText(_carsFilePath);
            var allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();

            var carToUpdate = allCars.FirstOrDefault(c => c.VIN == _originalVIN);
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
            System.IO.File.WriteAllText(_carsFilePath, json);
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
            if (!System.IO.Directory.Exists(imagesPath))
            {
                System.IO.Directory.CreateDirectory(imagesPath);
            }

            string photoFileName = $"{_car.VIN}.jpg";
            string photoPath = System.IO.Path.Combine(imagesPath, photoFileName);

            _car.PhotoPath = photoPath;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            UserCabinet userCabinet = new UserCabinet();
            userCabinet.Show();
        }
    }
}
