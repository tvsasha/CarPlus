using Library_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_brains
{
    public class ManagePhoto
    {
        public static void SavePhoto(Car car)
        {
            string imagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            string photoFileName = $"{car.VIN}.jpg";
            string photoPath = Path.Combine(imagesPath, photoFileName);

            car.PhotoPath = photoPath;
        }
    }
}
