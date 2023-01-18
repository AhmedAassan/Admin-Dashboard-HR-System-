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
    public class DistrictRep : IDistrictRep
    {
        private readonly ProjectContext db;

        public DistrictRep(ProjectContext db)
        {
            this.db = db;
        }

        public IEnumerable<District> Get(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {
                var data = db.District.Select(a => a);
                return data;
            }
            else
            {
                return db.District.Where(filter);
            }
        }








        public District GetById(int id)
        {
            var data = db.District.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
