using Microsoft.AspNetCore.Http;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Models
{
   public class EmployeeVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        [MinLength(2, ErrorMessage = "Min Len 2")]
        public string Name { get; set; }





        [Required(ErrorMessage = "Salary Required")]
        [Range(2000,15000,ErrorMessage ="Range BTW 2000 : 15000")]
        public double Salary { get; set; }




        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }




        public string Notes { get; set; }

        [EmailAddress(ErrorMessage ="Email Is Invalid")]
        public string Email { get; set; }




        public string Address { get; set; }

        [Required(ErrorMessage ="Choose Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }



        public int DistrictId { get; set; }
        public District District { get; set; }

        public IFormFile Photo { get; set; }
        public IFormFile Cv { get; set; }

        public string PhotoName { get; set; }
        public string CvName { get; set; }
    }
}
