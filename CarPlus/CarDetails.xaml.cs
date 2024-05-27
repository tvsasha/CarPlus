using CarPlus;
using Library_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для CarDetails.xaml
    /// </summary>
    public partial class CarDetails : Window
    {
        private Car _car;

        public CarDetails(Car car)
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
                txtMileage.Text = _car.Mileage.ToString();
                txtStatus.Text = _car.Status;
                txtSellerName.Text = _car.SellerName;
                txtSellerPhone.Text = _car.SellerPhone;
                txtDescription.Text = _car.Description;
                if (!string.IsNullOrEmpty(_car.PhotoPath))
                {
                    imgPhoto.Source = new BitmapImage(new Uri(_car.PhotoPath, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
