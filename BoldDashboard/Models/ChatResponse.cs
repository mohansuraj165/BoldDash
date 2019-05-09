using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class ChatResponse
    {
        public string Status { get; set; }
        public bool Truncated { get; set; }
        public Next Next { get; set; }
        public List<AllChatData> Data { get; set; }
    }

    
}