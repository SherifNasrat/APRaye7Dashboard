//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APRaye7.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class activities
    {
        public int id { get; set; }
        public Nullable<int> trackable_id { get; set; }
        public string trackable_type { get; set; }
        public Nullable<bool> unread { get; set; }
        public Nullable<int> owner_id { get; set; }
        public string owner_type { get; set; }
        public string key { get; set; }
        public string parameters { get; set; }
        public Nullable<int> recipient_id { get; set; }
        public string recipient_type { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}
