using System;

namespace SmartChoiceApp.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CropDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Fertilizer { get; set; }
    }
}
