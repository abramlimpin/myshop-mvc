using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShopMVC.Models
{
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name ="User Type")]
        [Required(ErrorMessage ="Select from the list...")]
        public int TypeID { get; set; }
        public List<UserType> UserTypes { get; set; }

        [Display(Name ="User Type")]
        public string UserType { get; set; }

        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Required")]
        [MaxLength(100, ErrorMessage ="Invalid input length")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [Required(ErrorMessage ="Required")]
        [MaxLength(50, ErrorMessage ="Invalid input length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Required")]
        [MaxLength(80, ErrorMessage ="Invalid input length")]
        public string FN { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(80, ErrorMessage = "Invalid input length")]
        public string LN { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "Invalid input length")]
        public string Street { get; set; }

        [Display(Name = "Municipality")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "Invalid input length")]
        public string Municipality { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "Invalid input length")]
        public string City { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(12, ErrorMessage = "Invalid input length")]
        [RegularExpression("^[0-9]{12}$", ErrorMessage ="Invalid format.")]
        public string Phone { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(12, ErrorMessage = "Invalid input length")]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Invalid format.")]
        public string Mobile { get; set; }

        [Display(Name ="Status")]
        public string Status { get; set; }

        [Display(Name ="Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name ="Date Modified")]
        [DisplayFormat(NullDisplayText = "")]
        public DateTime? DateModified { get; set; }
    }
}