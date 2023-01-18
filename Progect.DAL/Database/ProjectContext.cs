using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Progect.DAL.Entities;
using Progect.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.DAL.Database
{
   public class ProjectContext : IdentityDbContext<IdentityUserExtend>
    {
        
        public ProjectContext(DbContextOptions<ProjectContext> opj):base(opj)
        {

        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }

    }
}
