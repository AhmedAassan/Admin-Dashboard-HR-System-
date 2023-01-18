using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progect.BL.Helper;
using Progect.BL.Interface;
using Progect.BL.Models;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Progect.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRep employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }






        [HttpGet]
        [Route("~/Api/GetEmployees")]
        public IActionResult GetEmployees()
        {
            try
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);

                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>(){ 

                    Code = "200" ,
                    Status = "Ok" ,
                    Messsage = "Data Retrieved" ,
                    Data = model 

                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>(){

                            Code = "404",
                            Status = "Not Found",
                            Messsage = "Data Not Found",
                            Error = ex.Message

                });
            }
        }



        [HttpGet]
        [Route("~/Api/GetEmployeesById/{Id}")]
        public IActionResult GetEmployeesById(int id)
        {
            try
            {
                var data = employee.GetById(id);
                var model = mapper.Map<EmployeeVM>(data);

                return Ok(new ApiResponse<EmployeeVM>()
                {

                    Code = "200",
                    Status = "Ok",
                    Messsage = "Data Retrieved",
                    Data = model

                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "Not Found",
                    Messsage = "Data Not Found",
                    Error = ex.Message

                });
            }
        }





        [HttpPost]
        [Route("~/Api/PostEmployee")]
        public IActionResult PostEmployee(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);

                    var result = employee.Create(data);

                    return Ok(new ApiResponse<Employee>()
                    {

                        Code = "201",
                        Status = "Created",
                        Messsage = "Data Created" ,
                        Data = result

                    });

                }
                return Ok(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Not valid",
                    Messsage = "Data Invalid"

                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "failed",
                    Messsage = "Data Not Created",
                    Error = ex.Message

                });
            }
        }






        [HttpPut]
        [Route("~/Api/PutEmployee")]
        public IActionResult PutEmployee(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);

                    //var result = employee.Edit(data); مشكلة الريبوزاتوري

                    return Ok(new ApiResponse<Employee>()
                    {

                        Code = "200",
                        Status = "Put",
                        Messsage = "Data Updateed",
                        Data = data

                    });

                }
                return Ok(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Not valid",
                    Messsage = "Data Invalid"

                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "failed",
                    Messsage = "Data Not Updateed",
                    Error = ex.Message

                });
            }
        }








        [HttpDelete]
        [Route("~/Api/DeleteEmployee")]
        public IActionResult DeleteEmployee(EmployeeVM model)
        {
            try
            {
                
                    var data = mapper.Map<Employee>(model);

                     employee.Delete(data);

                    return Ok(new ApiResponse<EmployeeVM>()
                    {

                        Code = "200",
                        Status = "Delete",
                        Messsage = "Data Deleted",
                        Data = model

                    });

               
               
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "failed",
                    Messsage = "Data Not Deleted",
                    Error = ex.Message

                });
            }
        }
    }
}
