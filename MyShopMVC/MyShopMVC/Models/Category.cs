using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShopMVC.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Display(Name="Category")]
        public string Name { get; set; }
    }
}