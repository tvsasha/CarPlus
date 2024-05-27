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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private User _user;
        private string _filePath = "users.json";

        public EditUser(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;

            if (_user != null)
            {
                txtFullName.Text = _user.FullName;
                txtEmail.Text = _user.Email;
                txtPhone.Text = _user.Phone;
                txtPassword.Password = _user.Password; // Assuming the User class has a Password property
                txtConfirmPassword.Password = _user.Password;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _user.FullName = txtFullName.Text;
            _user.Email = txtEmail.Text;
            _user.Phone = txtPhone.Text;
            _user.Password = txtPassword.Password;

            SaveAllUsers();
            Close();
        }

        private void SaveAllUsers()
        {
            var json = System.IO.File.ReadAllText(_filePath);
            var allUsers = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

            var userToUpdate = allUsers.FirstOrDefault(u => u.Email == _user.Email);
            if (userToUpdate != null)
            {
                userToUpdate.FullName = _user.FullName;
                userToUpdate.Phone = _user.Phone;
                userToUpdate.Password = _user.Password;
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(allUsers, options);
            System.IO.File.WriteAllText(_filePath, json);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
