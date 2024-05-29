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

        public delegate void SellerNameChangedHandler(string newSellerName);
        public delegate void SellerPhoneChangedHandler(string newSellerPhone);

        public static event SellerNameChangedHandler OnSellerNameChanged;
        public static event SellerPhoneChangedHandler OnSellerPhoneChanged;

        public static void UpdateSellerName(string sellerName)
        {
            OnSellerNameChanged?.Invoke(sellerName);
        }

        public static void UpdateSellerPhone(string sellerPhone)
        {
            OnSellerPhoneChanged?.Invoke(sellerPhone);
        }
    }
}
