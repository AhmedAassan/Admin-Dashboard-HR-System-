using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Models
{
   public class LoginVM
    {
        [Required(ErrorMessage = "this field Required")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }




        [Required(ErrorMessage = "this field Required")]
        [MinLength(6, ErrorMessage = "Min Length 6")]
        public string Password { get; set; }

        
        public bool RememberMe { get; set; }
    }
}
