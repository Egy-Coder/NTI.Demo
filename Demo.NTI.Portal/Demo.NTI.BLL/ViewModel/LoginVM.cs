using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.ViewModel
{
    public class LoginVM
    {

        [Required(ErrorMessage = " email required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
