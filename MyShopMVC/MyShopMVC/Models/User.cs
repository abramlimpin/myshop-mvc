using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShopMVC.Models
{
    public class User
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FN { get; set; }
        public string LN { get; set; }
        public string Street { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }
}