using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Interface
{
   public interface ICityRep
    {
        IEnumerable<City> Get(Expression<Func<City, bool>> filter = null);
        City GetById(int id);
    }
}
