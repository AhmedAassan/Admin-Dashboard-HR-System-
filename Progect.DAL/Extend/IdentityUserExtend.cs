using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.DAL.Extend
{
    public class IdentityUserExtend : IdentityUser
    {
        public bool IsAgree { get; set; }
    }
}
