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
    public class CountryRep : ICountryRep
    {
        private readonly ProjectContext db;

        public CountryRep(ProjectContext db)
        {
            this.db = db;
        }

        public IEnumerable<Country> Get()
        {
            var data = db.Country.Select(a => a);
            return data;
        }

        public Country GetById(int id)
        {
            var data = db.Country.Where(a => a.Id == id).FirstOrDefault();
            return data; 
        }
    }
}
