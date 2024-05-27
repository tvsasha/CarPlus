using CarPlusWPF;
using Library_classes;
using System.ComponentModel;
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
        private string carsFilePath = "cars.json";
        private List<Car> allCars;

        public MainWindow()
        {
            InitializeComponent();
            LoadAllCars();
        }

        private void LoadAllCars()
        {
            if (System.IO.File.Exists(carsFilePath))
            {
                var json = System.IO.File.ReadAllText(carsFilePath);
                allCars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
                lvCars.ItemsSource = allCars;
            }
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Login.CurrentUser == null)
            {
                MessageBox.Show("Войдите, чтобы добавить автомобиль.");
                return;
            }

            AddCar addCarWindow = new AddCar();
            addCarWindow.Show();
            Close();
            
        }

        public void AddCar(Car car)
        {
            allCars.Add(car);
            SaveAllCars();
            lvCars.ItemsSource = null;
            lvCars.ItemsSource = allCars;
        }

        private void SaveAllCars()
        {
            var json = JsonSerializer.Serialize(allCars);
            System.IO.File.WriteAllText(carsFilePath, json);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            Close();
        }

        private void LvCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCars.SelectedItem is Car selectedCar)
            {
                CarDetails carDetailsWindow = new CarDetails(selectedCar);
                carDetailsWindow.Show();
                carDetailsWindow.Topmost = true;
            }
        }

        private void UserCabinet(object sender, RoutedEventArgs e)
        {
            if (Login.CurrentUser == null)
            {
                MessageBox.Show("Войдите в систему для доступа к личному кабинету");
                return;
            }
            User user = Login.CurrentUser;
            if (user.IsAdmin)
            {
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                Close();
            }
            else
            {
                UserCabinet userCabinet = new UserCabinet();
                userCabinet.Show();
                Close();
            }
            
        }

        private void SortCriteriaComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SortCriteriaComboBox.SelectedItem is System.Windows.Controls.ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Tag.ToString();
                SortCars(sortBy);
            }
        }

        private void SortCars(string sortBy)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvCars.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
            view.Refresh();
        }

    }
}