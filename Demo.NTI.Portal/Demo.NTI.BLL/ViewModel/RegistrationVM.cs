using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.ViewModel
{
    public class RegistrationVM
    {


        [Required(ErrorMessage = "user name required")]
        [StringLength(10, ErrorMessage = "length 10 charc.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        public string Password { get; set; }

        [Required(ErrorMessage = "confirm password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
