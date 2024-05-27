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
                txtSellerName.Text = _car.SellerName;
                txtDescription.Text = _car.Description;
                if (!string.IsNullOrEmpty(_car.PhotoPath))
                {
                    imgPhoto.Source = new BitmapImage(new Uri(_car.PhotoPath, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _car.VIN = txtVIN.Text;
            _car.Model = txtModel.Text;
            _car.Color = txtColor.Text;
            _car.Configuration = txtConfiguration.Text;
            _car.Price = decimal.Parse(txtPrice.Text);
            _car.SellerName = txtSellerName.Text;
            _car.Description = txtDescription.Text;
            _car.SellerEmail = Login.CurrentUser.Email;

            if (_car.PhotoPath == null && imgPhoto.Source != null)
            {
                SavePhoto();
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.AddCar(_car);
            mainWindow.Show();
            Close();
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

            // Save the photo to the specified path (you need to implement this)
            // SavePhotoToPath(photoPath);

            _car.PhotoPath = photoPath;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UserCabinet userCabinet = new UserCabinet();
            userCabinet.Show();
            Close();
        }
    }
}
