using CarPlusWPF;
using Library_classes;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCars();
            DisplayCars();
        }

        private string _filePath = "cars.json";
        private List<Car> _cars;

        private void LoadCars()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                _cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
            }
            else
            {
                _cars = new List<Car>();
            }
        }

        private void DisplayCars()
        {
            lstCars.ItemsSource = _cars;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();    
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            addCar.Show();
            Close();
            

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Close();
        }
    }
}