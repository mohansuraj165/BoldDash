using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class AllChatData
    {
        public string SystemMessage { get; set; }
        public string AccountID { get; set; }
        public string CannedMessageID { get; set; }
        public string ChatMessageID { get; set; }
        public string VisitorFacing { get; set; }
        public int PersonType { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string ChatID { get; set; }
        public DateTime Created { get; set; }
        public string LanguageCode { get; set; }
        public string MessageKey { get; set; }
        public string PersonID { get; set; }
        public string Hidden { get; set; }
        public string HasAutoTranslationError { get; set; }
        public string Deleted { get; set; }
        public string OriginalMessageID { get; set; }
    }
}