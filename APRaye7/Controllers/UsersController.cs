using APRaye7.Models;
using APRaye7.Models.Custom_Models;
using APRaye7.Models.ViewModels;
using APRaye7.Services;
using APRaye7.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace APRaye7.Controllers
{
    public class UsersController : Controller
    {
        private UsersService _userService = new UsersService();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getUsers(jQueryDataTableParamModel param)
        {
            var listOfUsers = _userService.GetAllUsers();
            
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfUsers.Count,
                iTotalDisplayRecords = listOfUsers.Count,
                aaData = listOfUsers
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int? id)
        {
            try
            {
                if(id != null)
                {
                    ViewBag.Title = App_GlobalResources.Users_resource.PageTitle;
                    ViewBag.SubTitle = App_GlobalResources.Users_resource.Details;
                    var user = _userService.getUserVMbyID(id);
                    return View(user);
                }
                else
                    return RedirectToAction("Index", "Users");

            }
            catch(Exception c)
            {
                throw c;
            }
          
        }
        public ActionResult noUserProfilePicture ()
        {
                return File(@"/Content/empty-profile.png", "image/png");
        }
        public ActionResult loadUserPicture(string Personal_Image)
        {
            return File(Personal_Image, "image/png");
        }
        public ActionResult Create()
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            List<SelectListItem> GenderList = new List<SelectListItem>();
            GenderList.Add(new SelectListItem{ Text = "Male", Value = "0" });
            GenderList.Add(new SelectListItem{ Text = "Female", Value = "1" });
            ViewBag.GenderOptions = GenderList;
            List<SelectListItem> BranchList = _userService.context.communities.Select(c => new SelectListItem { Text = c.name, Value = c.id.ToString() }).ToList();
           
            ViewBag.BranchesOptions = BranchList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM _user)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
           

            if (_user.Personal_Image_File != null)
            {
               
                    //Delete old image and create new one then update the image path
                   
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(_user.Personal_Image_File.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                // store the uploaded file on the file system
                _user.Personal_Image_File.SaveAs(path);
                _user.Personal_Image = path;
            }
            _userService.CreateUser(_user);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            bool DeletedSuccessfully = _userService.DeleteUser(id);
            if (DeletedSuccessfully)
                return RedirectToAction("Index", "Users");
            else
                return View("Error500");
        }
        public ActionResult Edit(int? id)
        {
            if(SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UserVM = _userService.getUserVMbyID(id);
            List<SelectListItem> GenderList = new List<SelectListItem>();
            GenderList.Add(new SelectListItem { Text = "Male", Value = "0" });
            GenderList.Add(new SelectListItem { Text = "Female", Value = "1" });
            ViewBag.GenderOptions = GenderList;
            List<SelectListItem> BranchList = _userService.context.communities.Select(c => new SelectListItem { Text = c.name, Value = c.id.ToString() }).ToList();
            foreach(var b in BranchList)
            {
                if (b.Value == UserVM.BranchID.ToString())
                    b.Selected = true;
            }
            ViewBag.BranchesOptions = BranchList;
            if (UserVM == null)
            {
                return HttpNotFound();
            }
            ModelState.Clear();
            return View(UserVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVM _user)
        {
            
            var oldUser = _userService.getUserbyID(_user.UserID);
            if(_user.Personal_Image_File != null)
            {
                if(!String.IsNullOrEmpty(oldUser.personal_image))
                //Delete old image and create new one then update the image path
                System.IO.File.Delete(oldUser.personal_image);
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(_user.Personal_Image_File.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                // store the uploaded file on the file system
                _user.Personal_Image_File.SaveAs(path);
                _user.Personal_Image = path;
            }
            else
            {
                _user.Personal_Image = oldUser.personal_image;
            }
            if(SessionController.User.IsAdmin != null || SessionController.User.IsSuperAdmin != null)
            _userService.SaveEdit(_user);
            return RedirectToAction("Index");


        }
    }
}