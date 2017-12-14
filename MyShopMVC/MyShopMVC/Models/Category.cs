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
        public string ID { get; set; }

        [Display(Name="Category")]
        public string Name { get; set; }
    }
}