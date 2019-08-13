using System;
using System.Collections.Generic;

namespace SmartChoiceApp.Models
{
    public class ProductDetail
    {
        //public Manufacturer ManufacturerInfo { get; set; }
        //public Product ProductInfo { get; set; }
        //public ProductType ProductTypeInfo { get; set; }
        //public User UserInfo { get; set; }
        public ProductInfo infor { get; set; }
        public List<Review> comments { get; set; }
    }
}
