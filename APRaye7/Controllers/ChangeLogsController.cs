using APRaye7.Models.Custom_Models;
using APRaye7.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APRaye7.Controllers
{
    public class ChangeLogsController : Controller
    {
        ChangeLogService _changeLogService = new ChangeLogService();
        // GET: ChangeLogs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllChangeLogs(jQueryDataTableParamModel param)
        {
            var list = _changeLogService.getAllLogs();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int? id,int? RowID)
        {
            var log = _changeLogService.CIBcontext.ChangeLogs.SingleOrDefault(l => l.ChangeLogId == id);
            if(log.Action == "Create" || log.Action == "Delete")
            {
                if (log.Entity == "Trips")
                    return RedirectToAction("Details", "Trips", new { id = RowID });
                else if (log.Entity == "Users")
                    return RedirectToAction("Details", "Users", new { id = RowID });
            }
            else if(log.Action == "Edit")
            {
                if(log.Entity == "Trips")
                {
                    TripsService tempServ = new TripsService();
                    var t = tempServ.GetTripVM(RowID);
                    t.ChangeLogId = (int)id;
                    return View("TripsEditDetails", t);
                }
                else if(log.Entity == "Users")
                {
                    UsersService tempServ = new UsersService();
                    var u = tempServ.getUserVMbyID(RowID);
                    u.ChangeLogId = (int)id;
                    return View("UsersEditDetails", u);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult getTripEditLog(jQueryDataTableParamModel param,int? ChangeLogID)
        {
            var listOfTripDetails =_changeLogService.getAllDetailLogs(ChangeLogID);
            //var listOfTripDetails = new List<ChangeLogService>();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfTripDetails.Count,
                iTotalDisplayRecords = listOfTripDetails.Count,
                aaData = listOfTripDetails
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getUsersEditLog(jQueryDataTableParamModel param, int? ChangeLogID)
        {
            var listOfUserDetails = _changeLogService.getAllDetailLogs(ChangeLogID);
            //var listOfTripDetails = new List<ChangeLogService>();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfUserDetails.Count,
                iTotalDisplayRecords = listOfUserDetails.Count,
                aaData = listOfUserDetails
            }, JsonRequestBehavior.AllowGet);
        }

    }
}