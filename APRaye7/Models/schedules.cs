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
    
    public partial class schedules
    {
        public int id { get; set; }
        public string repeatable_type { get; set; }
        public Nullable<int> repeatable_id { get; set; }
        public Nullable<bool> saturday { get; set; }
        public Nullable<bool> sunday { get; set; }
        public Nullable<bool> monday { get; set; }
        public Nullable<bool> tuesday { get; set; }
        public Nullable<bool> wednesday { get; set; }
        public Nullable<bool> thursday { get; set; }
        public Nullable<bool> friday { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}