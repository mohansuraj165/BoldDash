using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoldDashboard.Models
{
    public class Chat
    {
        public string Status { get; set; }
        public ChatData Data { get; set; }
    }

    public class OperatorCustomFields
    {
        public string Status { get; set; }
        public string Category { get; set; }
    }

    public class CustomFields
    {
        public string Comments { get; set; }
    }

    public class ChatData
    {
        public string RegionCode { get; set; }
        public int? Secured { get; set; }
        public string WindowClosed { get; set; }
        public object UserStatusID { get; set; }
        public object InvitationTemplateVariantID { get; set; }
        public int? VisitorMessageCount { get; set; }
        public object VisitInfo { get; set; }
        public object VideoSessionStatus { get; set; }
        public int? ResponseMessageCount { get; set; }
        public object ExternalCrmType { get; set; }
        public object SurveyComments { get; set; }
        public int? Embedded { get; set; }
        public int? AutoTranslate { get; set; }
        public object InitialDepartmentID { get; set; }
        public string LastAssignedByOperatorID { get; set; }
        public object DeletedBy { get; set; }
        public object Load { get; set; }
        public string VisitCreated { get; set; }
        public object VisitRef { get; set; }
        public string IP { get; set; }
        public string Disconnected { get; set; }
        public object RedirectStatus { get; set; }
        public int? ChatStatusType { get; set; }
        public int? EndedReasonType { get; set; }
        public object BlockedBy { get; set; }
        public string LastAssignedAnswered { get; set; }
        public string VisitEmailAddress { get; set; }
        public int? ClientType { get; set; }
        public object SurveyResponsiveness { get; set; }
        public string Started { get; set; }
        public object ExternalCrmListID { get; set; }
        public string FolderID { get; set; }
        public object UnavailableEmailBody { get; set; }
        public object Experiments { get; set; }
        public object LastVideoSessionID { get; set; }
        public object ChatAPISettingsID { get; set; }
        public string WebsiteDefID { get; set; }
        public string Created { get; set; }
        public object CustomField1ID { get; set; }
        public int? ActiveAssistStatus { get; set; }
        public int? ChatType { get; set; }
        public int? LastMessagePersonType { get; set; }
        public int? OperatorMessageCount { get; set; }
        public object UnavailableEmailSubject { get; set; }
        public object Deleted { get; set; }
        public string VisitorID { get; set; }
        public object AutoAnswerQuestionCount { get; set; }
        public string AccountID { get; set; }
        public int? ACDReassignCount { get; set; }
        public object UserCategoryID { get; set; }
        public object LastOperatorLanguageCode { get; set; }
        public string Ended { get; set; }
        public object XmppUserID { get; set; }
        public string RequestedOperatorID { get; set; }
        public object RelatedToItemID { get; set; }
        public int? AssignedByAPI { get; set; }
        public object ExternalCrmItemID { get; set; }
        public object ChatButtonDefID { get; set; }
        public object ScheduleHistoryToEmailAddress { get; set; }
        public string ChatWindowDefID { get; set; }
        public object SurveyOverall { get; set; }
        public string VisitID { get; set; }
        public int? PostChatCustomFieldAnswerStatus { get; set; }
        public string LanguageCode { get; set; }
        public string ChatUrl { get; set; }
        public OperatorCustomFields OperatorCustomFields { get; set; }
        public string CustomField2ID { get; set; }
        public object XmppClientID { get; set; }
        public object LastACDStarted { get; set; }
        public int? PageType { get; set; }
        public int? ActiveAssistDisabled { get; set; }
        public object CustomUrl { get; set; }
        public object XmppUser { get; set; }
        public int? Availability { get; set; }
        public int? MaxQueuePosition { get; set; }
        public object SurveyKnowledge { get; set; }
        public string Updated { get; set; }
        public object Priority { get; set; }
        public string ChatName { get; set; }
        public string City { get; set; }
        public string ChatID { get; set; }
        public object OperatorLanguageCode { get; set; }
        public CustomFields CustomFields { get; set; }
        public object ExperimentDate { get; set; }
        public int? AreaCode { get; set; }
        public object LastName { get; set; }
        public int? TotalResponseTime { get; set; }
        public object FlaggedBy { get; set; }
        public object ACDAssignmentExpires { get; set; }
        public string OperatorID { get; set; }
        public string LastVisitorMessageSent { get; set; }
        public string VisitorClientID { get; set; }
        public object IncidentID { get; set; }
        public object SurveyProfessionalism { get; set; }
        public int? UnrespondedMessageCount { get; set; }
        public int? TotalReassignCount { get; set; }
        public object SurveyNps { get; set; }
        public int? RelatedToItemType { get; set; }
        public object XmppThread { get; set; }
        public object DepartmentID { get; set; }
        public object DueDate { get; set; }
        public string CountryCode { get; set; }
        public object InitialQuestion { get; set; }
        public string ReverseIP { get; set; }
        public object VisitPhone { get; set; }
        public string EndedBy { get; set; }
        public string Answered { get; set; }
        public int? AutoTranslateAllowOriginal { get; set; }
        public string Closed { get; set; }
        public object Flagged { get; set; }
    }

    
}
