using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Helper
{
    public class ApiResponse <type>
    {
        public string Code { get; set; }
        public string Status { get; set; }

        public string Messsage { get; set; }

        public type Data { get; set; }

        public type Error { get; set; }


    }
}
