﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APRaye7.App_GlobalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ForgetPassword {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ForgetPassword() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("APRaye7.App_GlobalResources.ForgetPassword", typeof(ForgetPassword).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email submitted is not registered!.
        /// </summary>
        public static string EmailNotRegistered {
            get {
                return ResourceManager.GetString("EmailNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An email has been successfully sent with the instructions..
        /// </summary>
        public static string EmailSuccessful {
            get {
                return ResourceManager.GetString("EmailSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forget Password.
        /// </summary>
        public static string PageTitle {
            get {
                return ResourceManager.GetString("PageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password Reset Instructions.
        /// </summary>
        public static string PasswordResetInstructions {
            get {
                return ResourceManager.GetString("PasswordResetInstructions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occured, please try again later!.
        /// </summary>
        public static string RequestPasswordLater {
            get {
                return ResourceManager.GetString("RequestPasswordLater", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi {0},
        ///
        ///You can use the following temporary password: &quot;{1}&quot; on the Reset Password page to set your new password.
        ///
        ///Cheers,
        ///Admin Panel Raye7 Team..
        /// </summary>
        public static string TempPassword {
            get {
                return ResourceManager.GetString("TempPassword", resourceCulture);
            }
        }
    }
}