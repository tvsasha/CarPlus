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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string _filePath = "users.json";
        private List<User> _users;
        public static User CurrentUser { get; private set; } // Текущий пользователь

        public Login()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                CurrentUser = user; 

                if (user.IsAdmin)
                {
                    // администраторская панель
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                    Close();
                }
                else
                {
                    // обычная панель пользователя
                    WindowsHelper.OpenMainWindow(this);
                }
            }
            else
            {
                MessageBox.Show("Неверный email или пароль. Попробуйте снова.");
            }
        }
        private void Login_Closed(object sender, EventArgs e)
        {
            WindowsHelper.OpenMainWindow(this);
        }
    }
}
