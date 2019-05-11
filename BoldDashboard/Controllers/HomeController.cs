using BoldDashboard.Modals;
using BoldDashboard.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace BoldDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Gets all data required for the dashboard
        /// </summary>
        /// <param name="apiKey">API Setting Key</param>
        /// <returns>A JSON containing all data or error message</returns>
        public JsonResult GetDashboardData(string apiKey)
        {
            if(apiKey=="")
            {
                return Json(new { success = false, error = "Incorrect input"  }, JsonRequestBehavior.AllowGet);
            }
            DashboardAPI dashboardObj = new DashboardAPI(apiKey);
            Session["apiKey"] = apiKey;
            try
            {
                int chatCount = dashboardObj.GetChatsCount();
                var visits = dashboardObj.GetVisitType();
                var operators = dashboardObj.GetOperatorStatus();
                var activeOperators = operators.FindAll(o => o.StatusType != 0).DistinctBy(x => x.LoginID).ToList();
                return Json(new { success = true, chatCount, desktopVisit = visits.nonMobile, mobileVisits = visits.mobile, activeOperators }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, error=ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        /// <summary>
        /// Sets the status of the operator
        /// </summary>
        /// <param name="operatorData">Object containing all data for an operator</param>
        /// <returns>New operator availability list</returns>
        public JsonResult SetOperatorStatus(string operatorData)
        {
            string apiKey = (string)Session["apiKey"];
            OperatorData od = JsonConvert.DeserializeObject<OperatorData>(operatorData);
            DashboardAPI dashboardObj = new DashboardAPI(apiKey);
            var nextStatus = 0;
            if (od.StatusType == 1)
                nextStatus = 2;
            else if (od.StatusType == 2)
                nextStatus = 1;
            Response resp= dashboardObj.SetOperatorStatus(od.ServiceTypeID, od.LoginID, nextStatus, od.ClientID);
            if(resp.Status.ToLower()=="success")
            {
                var operators = dashboardObj.GetOperatorStatus();
                var activeOperators = operators.FindAll(o => o.StatusType != 0).DistinctBy(x => x.LoginID).ToList();
                return Json(new { success = true, activeOperators }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, error="Could not change operator status" }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}