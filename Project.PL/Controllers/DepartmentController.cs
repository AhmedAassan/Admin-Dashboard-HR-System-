using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Progect.BL.Interface;
using Progect.BL.Models;
using Progect.BL.Repository;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {



        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRep department, IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }



        public IActionResult Index()
        {
            var data = department.Get();
            var model = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(model);
        }




        public IActionResult Create()
        {
            
            return View();
        }



        [HttpPost]
        public IActionResult Create(DepartmentVM model) // استقبلت VM
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Create(data);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }








        public IActionResult Details(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }







        public IActionResult Edit(int id)
        {
            
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }



        [HttpPost]
        public IActionResult Edit(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Edit(data);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }








        public IActionResult Delete(int id)
        {

            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DepartmentVM model)
        {

            try
            {
                var data = mapper.Map<Department>(model);
                    department.Delete(data);
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }

    }
}
