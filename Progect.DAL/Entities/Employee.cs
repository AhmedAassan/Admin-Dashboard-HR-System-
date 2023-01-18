using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.DAL.Entities
{
    [Table("Employee")]
    public class Employee
    {
        
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }





        [Required]
        public double Salary { get; set; }




        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }




        public string Notes { get; set; }


        public string Email { get; set; }




        public string Address { get; set; }


        public int DepartmentId { get; set; }
        public Department Department { get; set; }






        
        
        public int DistrictId { get; set; }
        public District District { get; set; }



        public string PhotoName { get; set; }
        public string CvName { get; set; }

    }
}
