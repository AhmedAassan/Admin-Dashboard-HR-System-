using Progect.BL.Models;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Interface
{
   public interface IDepartmentRep
    {
        IEnumerable<Department> Get();
        Department GetById(int id);
        void Create(Department opj);
        void Edit(Department opj);
        void Delete(Department opj);
    }
}
