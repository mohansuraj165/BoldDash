using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class Next
    {
        public int PageSize { get; set; }
        public int PageStart { get; set; }
        public DateTime ToDate { get; set; }
    }
}