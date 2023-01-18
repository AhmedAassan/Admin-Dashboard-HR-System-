using Microsoft.EntityFrameworkCore;
using Progect.BL.Interface;
using Progect.DAL.Database;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Repository
{
    public class EmployeeRep : IEmployeeRep
    {




        private readonly ProjectContext db;
        public EmployeeRep(ProjectContext db)
        {
            this.db = db;
        }





        public IEnumerable<Employee> Get()
        {
            var data = db.Employee.Include("Department").Select(a => a); // Department=> Navegation proparty
            return data;
        }







        public Employee GetById(int id)
        {
            var data = db.Employee.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }










        public Employee Create(Employee opj)
        {


            var data = db.Employee.Add(opj);
            db.SaveChanges();
            //api
            return db.Employee.OrderBy(a => a.Id).LastOrDefault();
        }





        public void Edit(Employee opj)
        {

            db.Entry(opj).State = EntityState.Modified;
            db.SaveChanges();
            
        }

        //public Employee Edit(Employee opj)
        //{

        //    db.Entry(opj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    db.SaveChanges();
        //    //api
        //    return db.Employee.Find(opj.Id);
        //}








        public void Delete(Employee opj)
        {

            db.Employee.Remove(opj);
            db.SaveChanges();
        }

        public IEnumerable<Employee> SearchByName(string name)
        {
            var data = db.Employee.Include("Department").Where(a => a.Name.Contains(name) );
            return data;
        }
    }
}
