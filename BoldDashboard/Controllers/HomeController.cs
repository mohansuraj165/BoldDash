using BoldDashboard.Modals;
using BoldDashboard.Models;
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

        public JsonResult GetDashboardData(string apiKey)
        {
            if(apiKey=="")
            {
                return Json(new { success = false, error = "Incorrect input"  }, JsonRequestBehavior.AllowGet);
            }
            DashboardAPI dashboardObj = new DashboardAPI(apiKey);
            int chatCount = dashboardObj.GetChatsCount();
            var visits = dashboardObj.GetVisitType();
            return Json(new { success = true, chatCount, desktopVisit= visits.nonMobile, mobileVisits=visits.mobile }, JsonRequestBehavior.AllowGet);
        }
        
    }
}