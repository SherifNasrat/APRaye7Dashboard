//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CIBAdminsDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemUser()
        {
            this.ChangeLogs = new HashSet<ChangeLog>();
        }
    
        public int SystemUserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsUser { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> IsSuperAdmin { get; set; }
        public string PersonalImage { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> FK_Addedby { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> FK_LastModifiedBy { get; set; }
        public string SecureToken { get; set; }
        public Nullable<System.DateTime> SecureTokenExpiryTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeLog> ChangeLogs { get; set; }
    }
}
