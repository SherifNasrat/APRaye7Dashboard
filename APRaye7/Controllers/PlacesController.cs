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
    public class PlacesController : Controller
    {
        PlacesService _place = new PlacesService();
        // GET: Places
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getPlaces(jQueryDataTableParamModel param)
        {
            var listOfPlaces = _place.getAllPlaces();

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfPlaces.Count,
                iTotalDisplayRecords = listOfPlaces.Count,
                aaData = listOfPlaces
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int? id)
        {
            var PlaceVM = _place.getPlaceVM(id);
            return View(PlaceVM);
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
            var PlaceVM = _place.getPlaceVM(id);
          
            if (PlaceVM == null)
            {
                return HttpNotFound();
            }
            return View(PlaceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlaceVM _branch)
        {

            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            _place.SaveEdit(_branch);
            return RedirectToAction("Index");


        }
        public ActionResult Delete(int? id)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            bool DeletedSuccessfully = _place.DeleteBranch(id);
            if (DeletedSuccessfully)
                return RedirectToAction("Index", "Places");
            else
                return View("Error500");
        }
        public ActionResult Create()
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaceVM branch)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            _place.CreateBranch(branch);
            return RedirectToAction("Index");
        }
    }
}