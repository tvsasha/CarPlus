using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_classes
{
    public class User
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } 
        public List<string> CarVINs { get; set; }

        public delegate void UserPhoneChangedHandler(string phone);

        // Событие изменения номера пользователя
        public static event UserPhoneChangedHandler OnUserPhoneChanged;

        // Статический метод для обновления номера пользователя
        public static void UpdateUserPhone(string phone)
        {
            OnUserPhoneChanged?.Invoke(phone);
        }
    }
}
