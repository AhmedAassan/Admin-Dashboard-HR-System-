using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Interface
{
   public interface ICountryRep
    {
        IEnumerable<Country> Get();
        Country GetById (int id);
    }
}
