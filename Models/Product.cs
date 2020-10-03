using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopWebMVC.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string CategoryID { get; set; }
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
    }
}