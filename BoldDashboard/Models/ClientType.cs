using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class ClientType
    {
        public int nonMobile { get; set; }
        public int mobile { get; set; }

        public ClientType()
        {
            nonMobile = 0;
            mobile = 0;
        }
    }
}