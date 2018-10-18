using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="User name")]
        [Required(ErrorMessage ="Enter username")]
        public string UserName { get; set; }
        [Display(Name ="Password")]
        [Required(ErrorMessage ="Enter Password")]
        public string Password { get; set; }
    }
}