using Library_classes;
using System.Text.Json;
namespace Library_brains
{
    public class MoveCar
    {
        public interface IAutoRepository
        {
            void AddAuto(Car auto);
            void RemoveCar(string vin);
            Car GetCar(string vin);
            IEnumerable<Car> GetAllCars();
            void SaveChanges();
        }

        // IUserRepository.cs
        public interface IUserRepository
        {
            void AddUser(User user);
            void RemoveUser(string email);
            User GetUser(string email);
            IEnumerable<User> GetAllUsers();
            void SaveChanges();
        }
    }
}
