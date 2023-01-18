using Progect.BL.Interface;
using Progect.BL.Models;
using Progect.DAL.Database;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {
        private readonly ProjectContext db;

        //ProjectContext db = new ProjectContext();
        public DepartmentRep(ProjectContext db)
        {
            this.db = db;
        }

        public IEnumerable<Department> Get()
        {
            var data = db.Department.Select(a => a);
            return data;
        }







        public Department GetById(int id)
        {
            var data = db.Department.Where(a=>a.Id ==id).FirstOrDefault();
            return data;
        }










        public void Create(Department opj)
        {
            

            var data = db.Department.Add(opj);
            db.SaveChanges();
        }







        public void Edit(Department opj)
        {
            //var oldData = db.Department.Find(opj.Id);
            //oldData.Name = opj.Name;
            //oldData.Code = opj.Code;
            db.Entry(opj).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
            db.SaveChanges();
        }








        public void Delete(Department opj)
        {

            db.Department.Remove(opj);
            db.SaveChanges();
        }




    }
}
