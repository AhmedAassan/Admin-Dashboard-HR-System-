using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Progect.BL.Helper;
using Progect.BL.Interface;
using Progect.BL.Models;
using Progect.BL.Repository;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;
        private readonly IDepartmentRep department;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        public EmployeeController(IEmployeeRep employee, IMapper mapper, IDepartmentRep department, ICityRep city, IDistrictRep district)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.department = department;
            this.city = city;
            this.district = district;
        }



        public IActionResult Index(string shearchValue = "")
        {

            if (shearchValue == "")
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(model);
            }
            else
            {
                var data = employee.SearchByName(shearchValue);
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(model);
            } 
           
            
        }


        

        public IActionResult Create()
        {

            
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");

            return View();
        }



        [HttpPost]
        public IActionResult Create(EmployeeVM model) // استقبلت VM
        {
            try
            {

                

                if (ModelState.IsValid)
                {
                    model.PhotoName = FileUploader.UploadFile("/wwwroot/Files/Imgs/",model.Photo);
                    model.CvName = FileUploader.UploadFile("/wwwroot/Files/Docs/", model.Cv);


                    var data = mapper.Map<Employee>(model);
                    employee.Create(data);
                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");

                return View(model);
            }
        }








        public IActionResult Details(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            ViewBag.DistrictList = new SelectList(district.Get(), "Id", "Name", model.DistrictId);
            return View(model);
        }







        public IActionResult Edit(int id)
        {

            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            ViewBag.DistrictList = new SelectList(district.Get(), "Id", "Name", model.DistrictId);
            return View(model);
        }



        [HttpPost]
        public IActionResult Edit(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    employee.Edit(data);
                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
        }








        public IActionResult Delete(int id)
        {

            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(EmployeeVM model)
        {

            try
            {
                var data = mapper.Map<Employee>(model);
                employee.Delete(data);


                FileUploader.RemoveFile("/wwwroot/Files/Imgs/", model.PhotoName);
                FileUploader.RemoveFile("/wwwroot/Files/Docs/", model.CvName);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
 
                return View(model);
            }
        }








        #region Ajax Call
        [HttpPost]
        public JsonResult GetCityDataByCountryId(int CntryId)
        {
            var data = city.Get(a => a.CountryId == CntryId);
            var model = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(model);
        }



        [HttpPost]
        public JsonResult GetDistrictDataByCityId(int CtyId)
        {
            var data = district.Get(a => a.CityId == CtyId);
            var model = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(model);
        }

        #endregion
    }
}
