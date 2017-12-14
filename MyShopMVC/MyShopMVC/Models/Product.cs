using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShopMVC.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CatID { get; set; }
        public List<Category> Categories { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public int Available { get; set; }
        public int Critical { get; set; }
        public int Max { get; set; }
        public string Status { get; set; }
        public string AllStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }
}