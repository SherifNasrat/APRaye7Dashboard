using APRaye7.Models;
using APRaye7.Models.Custom_Models;
using APRaye7.Models.ViewModels;
using APRaye7.Services;
using APRaye7.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace APRaye7.Controllers
{
    public class TripsController : Controller
    {
        TripsService _tripsSerivce = new TripsService();
        // GET: Trips
        public ActionResult Index()
        {
            var listOfTrips = _tripsSerivce.GetAllTrips();
            return View(listOfTrips);
        }
        public ActionResult getTrips(jQueryDataTableParamModel param)
        {
            var listOfTrips = _tripsSerivce.GetAllTrips();
          
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfTrips.Count,
                iTotalDisplayRecords = listOfTrips.Count,
                aaData = listOfTrips
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            ViewBag.FK_PlaceIDs = _tripsSerivce.DDLFK_PlaceIDs();
            ViewBag.FK_DriverIDs = _tripsSerivce.DDLFK_DriverIDs();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripVM _trip)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            _tripsSerivce.CreateTrip(_trip);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            var tripVM = _tripsSerivce.GetTripVM(id);
            if(tripVM.Deleted == true)
            {
                tripVM.CancellationReason=  _tripsSerivce.getCancellationReason(tripVM.TripID);
            }
            //ViewBag.Source = _tripsSerivce.GetPlacebyID(tripVM.FK_SourceID);
            //ViewBag.Destination = _tripsSerivce.GetPlacebyID(tripVM.FK_DestinationID);
            return View(tripVM);
        }
        public ActionResult Delete(int? id)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            bool DeletedSuccessfully = _tripsSerivce.DeleteTrip(id);
            if (DeletedSuccessfully)
                return RedirectToAction("Index", "Trips");
            else
                return View("Error500");
        }
        public ActionResult Edit(int? id)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TripVM = _tripsSerivce.GetTripVM(id);
            ViewBag.FK_PlaceIDs = _tripsSerivce.DDLFK_PlaceIDs();
            ViewBag.FK_DriverIDs = _tripsSerivce.DDLFK_DriverIDs();
            if (TripVM == null)
            {
                return HttpNotFound();
            }
            return View(TripVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripVM _trip)
        {

            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            _tripsSerivce.SaveEdit(_trip);    
            return RedirectToAction("Index");
            
           
        }
    }
}