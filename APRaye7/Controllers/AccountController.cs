using APRaye7.Models.Custom_Models;
using APRaye7.Models.ViewModels;
using APRaye7.Services;
using APRaye7.Shared;
using APRaye7.App_GlobalResources;
using CIBAdminsDB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static APRaye7.Models.ViewModels.SystemUser;

namespace APRaye7.Controllers
{
    public class AccountController : Controller
    {
        AccountService usersService = new AccountService();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SystemUserVM user)
        {
            user.Username = user.UsernameNoValidation;
            if (IsValid(user))
            {
                // MenusInitializer myMenus = new MenusInitializer();
                // SessionController.Menus = myMenus.ProjectMenus.OrderBy(m => m.MenuName).ToList();
                
                return RedirectToAction("Index", "Home");

            }

            else
            {
                ModelState.AddModelError("", App_GlobalResources.Login_resource.LoginFailed);
                return View(user);
            }
        }

        private bool IsValid(SystemUserVM user)
        {
            var myUser = new SystemUserVM();
            try
            {
                myUser = LogmeIn(user.Username, user.Password);
            }
            catch(Exception e)
            {

            }
            if (myUser != null)
            {
                SessionController.User = myUser;
                return true;
            }
            else
                return false;
        }

        private SystemUserVM LogmeIn(string userName, string password)
        {
            var user = usersService.CIBcontext.SystemUsers.FirstOrDefault(s => s.Username.ToLower().Trim() == userName.ToLower().Trim());
            /// TODO: Add password Filter
            if (user != null && CompareHash(password, user.Password))
            {
                
                return usersService.SystemUserToSystemUserVM(user);
            }
            return null;

        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(SystemUserVM user)
        {
            ModelState["EmailNoValidation"].Errors.Clear();
            ModelState["UsernameNoValidation"].Errors.Clear();
            if (ModelState.IsValid)
            {
                if(user.ConfirmPassword == user.Password)
                {
                    try
                    {
                        var Newuser = new CIBAdminsDB.SystemUser();
                        Newuser = usersService.SystemUserVMToSystemUser(user);
                        Newuser.Password = GetHashedString(Newuser.Password);
                        Newuser.CreationDate = DateTime.Now;
                        usersService.CIBcontext.SystemUsers.Add(Newuser);
                        usersService.CIBcontext.SaveChanges();
                        return RedirectToAction("Login");
                    }
                   catch(DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
                else
                {
                    SessionController.Errors = new List<string>() { "Password and Confirm Password must match!" };
                    return RedirectToAction("Register");
                }
            
            }
            else
            {
                SessionController.Errors = new List<string>() { "Registeration Failed,please try again!" };
                return RedirectToAction("Register");
            }
        }
        
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(SystemUserVM _user)
        {
            try
            {

            var email = _user.EmailNoValidation;
            var query = usersService.CIBcontext.SystemUsers.Where(u => u.Email.ToLower() == email.ToLower());
            var validEmail = query.Count() == 1;
            if (!validEmail)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ForgetPassword.EmailNotRegistered };
                return RedirectToAction("ForgetPassword");
            }
            var user = query.Single();
            var tempPassword = usersService.TryGenerateSecureToken(user.SystemUserID);
            if (tempPassword == null)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ForgetPassword.RequestPasswordLater };
                return RedirectToAction("ForgetPassword");
            }
            MailMessage msg = new MailMessage();
            msg.To.Add(email);
            msg.Subject = App_GlobalResources.ForgetPassword.PasswordResetInstructions;
            msg.Body = String.Format(App_GlobalResources.ForgetPassword.TempPassword, user.Username, tempPassword);
            SmtpClient client = new SmtpClient();
                //client.Host = "localhost";
                msg.From = new MailAddress("noreply@cib.com");
                client.Send(msg);
            SessionController.Success = new List<string> { App_GlobalResources.ForgetPassword.EmailSuccessful };
            return RedirectToAction("ResetPassword");

            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string enteredPassword, string confirmation, string tempPassword)
        {
            string encryptedPassword;
            string secureToken;
            CIBAdminsDB.SystemUser forgetter;

            if (enteredPassword != confirmation)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.PasswordMatchError };
                return RedirectToAction("ResetPassword");
            }
            if (enteredPassword.Length < 6)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.PasswordLength };
                return RedirectToAction("ResetPassword");
            }
            secureToken = usersService.CalculateSecureToken(tempPassword);
            forgetter = usersService.CIBcontext.SystemUsers.SingleOrDefault(u => u.SecureToken == secureToken);
            if (forgetter == null)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.TempPasswordIncorrect };
                return RedirectToAction("ResetPassword");
            }
            if (forgetter.SecureTokenExpiryTime < DateTime.Now)
            {
                SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.TempPasswordExpired };
                return RedirectToAction("ForgetPassword");
            }

            encryptedPassword = GetHashedString(enteredPassword);
            forgetter.Password = encryptedPassword;
            forgetter.SecureToken = null;
            forgetter.SecureTokenExpiryTime = null;
            usersService.CIBcontext.SaveChanges();
            SessionController.Success = new List<string> { App_GlobalResources.ResetPassword.NewPassword };
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult SystemUserProfile(int? id)
        {
            var user = usersService.CIBcontext.SystemUsers.FirstOrDefault(sys => sys.SystemUserID == id);
            return View(usersService.SystemUserToSystemUserVM(user));
            
        }
        
        public ActionResult ChangePassword(int? id)
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldpassword,string newpassword,string confirmnewpassword)
        {
            string oldpass = GetHashedString(oldpassword);
            var user = usersService.CIBcontext.SystemUsers.SingleOrDefault(s => s.Password == oldpass);
            if(user != null)
            {
                if (newpassword.Length < 6)
                {
                    SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.PasswordLength };
                    return RedirectToAction("ChangePassword");
                }
                string newpass = GetHashedString(newpassword);
                string confirm = GetHashedString(confirmnewpassword);
                if (newpass == confirm)
                {


                    user.Password = GetHashedString(newpassword);
                    usersService.CIBcontext.SaveChanges();
                    return RedirectToAction("SystemUserProfile",new { id = user.SystemUserID });

                }
                else
                {
                    SessionController.Errors = new List<string> { App_GlobalResources.ResetPassword.PasswordMatchError };
                    return RedirectToAction("ChangePassword");
                }
            }
          
            else
            return View();
        }
        public JsonResult CheckUserNameUnique(string UserName, int? SystemUserID)
        {
            return Json(usersService.CheckRedundantForName(UserName,SystemUserID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckEmailUnique(string Email, int? SystemUserID)
        {
            return Json(usersService.CheckRedundant(Email, SystemUserID), JsonRequestBehavior.AllowGet);
        }
        private static bool CompareHash(string input, string hashed)
        {
            string inputHash = GetHashedString(input);

            return (string.CompareOrdinal(inputHash, hashed)) == 0;
        }
        private static string GetHashedString(string text)
        {
            UTF8Encoding encoder = new UTF8Encoding();

            Byte[] hashedDataBytes = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(encoder.GetBytes(text));

            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hashedDataBytes.Length; i++)
            {
                sBuilder.Append(hashedDataBytes[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public ActionResult ViewSystemUsers()
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            return View();
        }
        public ActionResult getSystemUsers(jQueryDataTableParamModel param)
        {
            var listOfSystemUsers = usersService.getAllSystemUsersVM();
            
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listOfSystemUsers.Count,
                iTotalDisplayRecords = listOfSystemUsers.Count,
                aaData = listOfSystemUsers
            }, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult EditSystemUser(int? id)
        {
            if (SessionController.User.IsAdmin == null && SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            else
            {
                if (SessionController.User.SystemUserID != id)
                    return View("AccessDenied");
            }
            var systemUser = usersService.SystemUserToSystemUserVM(usersService.CIBcontext.SystemUsers.SingleOrDefault(u => u.SystemUserID == id));
            List<SelectListItem> RoleOptions = new List<SelectListItem>();
           
            RoleOptions.Add(new SelectListItem() { Text = "User", Value = "0" });
            RoleOptions.Add(new SelectListItem() { Text = "Admin", Value = "1" });
            if (systemUser.IsAdmin != null)
            {
                systemUser.RoleIndex = 1;
                RoleOptions[1].Selected = true;
            }
            else if (systemUser.IsUser != null)
            {
                systemUser.RoleIndex = 0;
                RoleOptions[0].Selected = true;
            }
            //else if (systemUser.RoleIndex != 2)
            //{
            //    systemUser.RoleIndex = 2;
            //}
            //RoleOptions.Add(new SelectListItem() { Text = "Super Admin", Value = "2" });
            ViewBag.RoleOptions = RoleOptions;
            return View(systemUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSystemUser(SystemUserVM _user)
        {
            if (SessionController.User.IsAdmin == null || SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            var oldSysUser = usersService.CIBcontext.SystemUsers.SingleOrDefault(u => u.SystemUserID == _user.SystemUserID);

            if (_user.Personal_Image_File != null)
            {
                if (!String.IsNullOrEmpty(oldSysUser.PersonalImage))
                    //Delete old image and create new one then update the image path
                    System.IO.File.Delete(oldSysUser.PersonalImage);
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(_user.Personal_Image_File.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                // store the uploaded file on the file system
                _user.Personal_Image_File.SaveAs(path);
                _user.PersonalImage = path;
                oldSysUser.PersonalImage = _user.PersonalImage;
            }
            oldSysUser.Email = _user.Email;
            oldSysUser.FullName = _user.FullName;

            switch (_user.RoleIndex)
            {
                case 0:
                    oldSysUser.IsUser = true;
                    oldSysUser.IsSuperAdmin = null;
                    oldSysUser.IsAdmin = null;
                    break;
                case 1:
                    oldSysUser.IsUser = null;
                    oldSysUser.IsSuperAdmin = null;
                    oldSysUser.IsAdmin = true;
                    break;
                case 3:
                    oldSysUser.IsUser = null;
                    oldSysUser.IsSuperAdmin = true;
                    oldSysUser.IsAdmin = null;
                    break;
            }
            oldSysUser.LastModifiedDate = DateTime.Now;
            oldSysUser.FK_LastModifiedBy = SessionController.User.SystemUserID;
            usersService.CIBcontext.Entry(oldSysUser).State = System.Data.Entity.EntityState.Modified;
            usersService.CIBcontext.SaveChanges();
            return RedirectToAction("ViewSystemUsers");
        }
        public ActionResult Delete(int? id)
        {
            if (SessionController.User.IsAdmin == null || SessionController.User.IsSuperAdmin == null)
            {
                return View("AccessDenied");
            }
            var systemUserToDelete = usersService.CIBcontext.SystemUsers.SingleOrDefault(u => u.SystemUserID == id);
            if (systemUserToDelete != null)
            {
               
                usersService.CIBcontext.SystemUsers.Remove(systemUserToDelete);
                usersService.CIBcontext.SaveChanges();
               
            }

            return RedirectToAction("ViewSystemUsers", "Account");
        }
        public ActionResult getSessionControllerUser()
        {
            var user = SessionController.User;
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}