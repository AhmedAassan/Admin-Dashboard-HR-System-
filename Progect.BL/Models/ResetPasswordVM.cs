using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Models
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "this field Required")]
        [MinLength(6, ErrorMessage = "Min Length 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "this field Required")]
        [MinLength(6, ErrorMessage = "Min Length 6")]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
