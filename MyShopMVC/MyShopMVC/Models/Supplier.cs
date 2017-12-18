using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShopMVC.Models
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }

        [Display(Name ="Company Name")]
        [Required(ErrorMessage ="Required")]
        [MaxLength(200, ErrorMessage ="Invalid input length.")]
        public string Company { get; set; }

        [Display(Name ="Contact Person")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage ="Invalid input length.")]
        public string Contact { get; set; }

        [Display(Name ="Address")]
        [Required(ErrorMessage ="Required")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name ="Phone")]
        [Required(ErrorMessage ="Required")]
        [MaxLength(12)]
        [MinLength(12)]
        public string Phone { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(12)]
        [MinLength(12)]
        public string Mobile { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Select from list.")]
        public string Status { get; set; }

        public List<SelectListItem> AllStatus { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        [DisplayFormat(NullDisplayText = "")]
        public DateTime? DateModified { get; set; }
    }
}