using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Models
{
   public class DepartmentVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50,ErrorMessage ="Max Len 50")]
        [MinLength(2, ErrorMessage = "Min Len 2")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Required")]

        public string Code { get; set; }
    }
}
