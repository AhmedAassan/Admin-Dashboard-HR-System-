using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Models
{
   public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "this field Required")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }
    }
}
