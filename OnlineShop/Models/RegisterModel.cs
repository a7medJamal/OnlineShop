using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [Display(Name ="User name")]
        [Required(ErrorMessage ="your User name plz")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "your Password plz")]
        [StringLength(maximumLength:20,MinimumLength =6,ErrorMessage = "Password betwwen 6 -20 ًCharacter")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your Password Not identical")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "your  Name plz")]
        public string Name { get; set; }

        [Display(Name = "Addesss")]
        [Required(ErrorMessage = "your Addesss plz")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "your Email plz")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "your Phone plz")]
        public string Phone { get; set; }
    }
}