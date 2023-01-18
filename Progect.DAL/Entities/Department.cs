using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.DAL.Entities
{
    [Table("Department")]
   public class Department
    {
        public int Id { get; set; }



        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        [Required]
        public string Code { get; set; }


        public ICollection<Employee> Employee { get; set; }
    }
}
