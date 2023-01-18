using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Interface
{
   public interface IEmployeeRep
    {
        IEnumerable<Employee> Get();
        Employee GetById(int id);
        Employee Create(Employee opj);
        void Edit(Employee opj);
        void Delete(Employee opj);
        IEnumerable<Employee> SearchByName( string name);
    }
}
