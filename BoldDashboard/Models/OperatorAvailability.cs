using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class OperatorData
    {
        public string LoginID { get; set; }
        public string UserName { get; set; }
        public string ChatName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailName { get; set; }
        public string ServiceTypeID { get; set; }
        public int StatusType { get; set; }
        public string ClientID { get; set; }
        public string CustomOperatorStatusID { get; set; }
    }

    public class OperatorAvailability
    {
        public string Status { get; set; }
        public bool Truncated { get; set; }
        public List<OperatorData> Data { get; set; }
    }

}