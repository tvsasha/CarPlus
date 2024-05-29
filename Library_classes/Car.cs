using System.Text.Json;

namespace Library_classes
{
    public class Car
    {
        public string VIN { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Configuration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string Status { get; set; } 
        public int Mileage { get; set; } 
        public string SellerPhone { get; set; }

        public delegate void SellerPhoneChangedHandler(string newSellerPhone);

        public static event SellerPhoneChangedHandler OnSellerPhoneChanged;

        public Car()
        {
            User.OnUserPhoneChanged += UpdateSellerPhone;
        }

        private void UpdateSellerPhone(string newSellerPhone)
        {
            SellerPhone = newSellerPhone;
            SaveCar(car);
        }

        private void SaveCar(Car car)
        {
            List<Car> cars;
            if (File.Exists("cars.json"))
            {
                var json = File.ReadAllText("cars.json");
                cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
            }
            else
            {
                cars = new List<Car>();
            }

            // Проверяем, есть ли уже машина с таким VIN в списке
            var existingCar = cars.FirstOrDefault(c => c.VIN == car.VIN);
            if (existingCar != null)
            {
                // Обновляем информацию о машине
                existingCar.Model = car.Model;
                existingCar.Color = car.Color;
                existingCar.Configuration = car.Configuration;
                existingCar.Price = car.Price;
                existingCar.Description = car.Description;
                existingCar.SellerName = car.SellerName;
                existingCar.SellerEmail = car.SellerEmail;
                existingCar.Status = car.Status;
                existingCar.Mileage = car.Mileage;
                existingCar.SellerPhone = car.SellerPhone; // Обновляем номер телефона продавца
                existingCar.PhotoPath = car.PhotoPath;
            }
            else
            {
                // Добавляем новую машину в список
                cars.Add(car);
            }

            // Сериализуем обновленный список машин в формат JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(cars, options);

            // Записываем JSON-строку в файл
            File.WriteAllText("cars.json", jsonString);
        }

    }
}
