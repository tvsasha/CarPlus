using Library_classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CarPlusWPF
{
    internal class Photo
    {
        public static void AddPhoto(Image imgPhoto, Car car)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                string imagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(openFileDialog.FileName);
                string destinationPath = Path.Combine(imagesPath, uniqueFileName);
                File.Copy(openFileDialog.FileName, destinationPath);
                imgPhoto.Source = new BitmapImage(new Uri(destinationPath, UriKind.Absolute));
                car.PhotoPath = Path.Combine("Images", uniqueFileName);
            }
        }

        public static void OpenPhoto(string photoPath, Image imgPhoto)
        {
            if (!string.IsNullOrEmpty(photoPath))
            {
                try
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, photoPath);
                    if (File.Exists(fullPath))
                    {
                        imgPhoto.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
                    }
                    else
                    {
                        MessageBox.Show("Файл изображения не найден: " + fullPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Фотография отсутствует.");
            }
        }
    }
}