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
        public CarDetails(Car car)
        {
            InitializeComponent();
            DataContext = car;
            txtVIN.Text = car.VIN;
            txtModel.Text = car.Model;
            txtColor.Text = car.Color;
            txtConfiguration.Text = car.Configuration;
            txtPrice.Text = car.Price.ToString("C");
            txtSellerName.Text = car.SellerName;
            txtDescription.Text = car.Description;

            if (!string.IsNullOrEmpty(car.PhotoPath) && System.IO.File.Exists(car.PhotoPath))
            {
                imgPhoto.Source = new BitmapImage(new Uri(car.PhotoPath, UriKind.RelativeOrAbsolute));
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
