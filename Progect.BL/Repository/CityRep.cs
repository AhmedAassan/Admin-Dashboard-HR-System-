using Progect.BL.Interface;
using Progect.DAL.Database;
using Progect.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Repository
{
    public class CityRep : ICityRep
    {
        private readonly ProjectContext db;

        public CityRep(ProjectContext db)
        {
            this.db = db;
        }

        public IEnumerable<City> Get(Expression<Func<City, bool>> filter = null)
        {

            if (filter == null)
            {
                var data = db.City.Select(a => a);
                return data;
            }
            else
            {
                return db.City.Where(filter);
            }
        }

        public City GetById(int id)
        {
            var data = db.City.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }

}

    