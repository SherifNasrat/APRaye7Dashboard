using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APRaye7.App_GlobalResources;

namespace APRaye7.Models.ViewModels
{
    [MetadataType(typeof(SystemUserMetadata))]
    public partial class SystemUser
    {
        public class SystemUserMetadata
        {
            public int SystemUserID { get; set; }
            [Remote("CheckUserNameUnique", "Account", AdditionalFields = "UserName,SystemUserID", ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "InvalidUserName")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50, ErrorMessage = "Maxmum 50 symbol", MinimumLength = 1)]
            [Required(ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "UserNameRequired")]
            [Display(Name = "UserName", ResourceType = typeof(SystemUser_resource))]
            public string Username { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(1000, ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "MinimumPassword", MinimumLength = 6)]
            [Required(ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "EmptyPassword")]
            [DataType(DataType.Password)]
            public string Password { get; set; }


            

            public string FullName { get; set; }


            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(
               @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
               ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "EnterValidMail")]
            [DataType(DataType.EmailAddress)]
            [Required(ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "EmailRequired")]

            [Display(Name = "Email", ResourceType = typeof(SystemUser_resource))]
            [Remote("CheckEmailUnique", "Account", AdditionalFields = "Email,SystemUserID", ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "duplicateEmails")]
            public string Email { get; set; }


            public bool? IsUser { get; set; }
            public bool? IsAdmin { get; set; }
            public bool? IsSuperAdmin { get; set; }
            public string PersonalImage { get; set; }
            [Display(Name = "Creation Date")]
            public System.DateTime? CreationDate { get; set; }
            [Display(Name = "Added By")]
            public int? FK_Addedby { get; set; }
            [Display(Name = "Last Modified Date")]

            public System.DateTime? LastModifiedDate { get; set; }
            [Display(Name = "Last Modified By")]
            public int? FK_LastModifiedBy { get; set; }
            public string SecureToken { get; set; }
            public Nullable<System.DateTime> SecureTokenExpiryTime { get; set; }
        }
        public class SystemUserVM : SystemUserMetadata
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50, ErrorMessage = "Maxmum 50 symbol", MinimumLength = 1)]
            [Required(ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "UserNameRequired")]
            [Display(Name = "UserName", ResourceType = typeof(SystemUser_resource))]
            public string UsernameNoValidation { get; set; }


            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(
              @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
              ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "EnterValidMail")]
            [DataType(DataType.EmailAddress)]
            [Required(ErrorMessageResourceType = typeof(SystemUser_resource), ErrorMessageResourceName = "EmailRequired")]

            [Display(Name = "Email", ResourceType = typeof(SystemUser_resource))]
            public string EmailNoValidation { get; set; }
            public string ConfirmPassword { get; set; }
            public bool RememberMe { get; set; }
            public string oldPassword { get; set; }
            public string Role { get; set; }
            public string CreationDateString { get; set; }
            public int? RoleIndex { get; set; }
            public HttpPostedFileBase Personal_Image_File { get; set; }
        }

          
    }
}