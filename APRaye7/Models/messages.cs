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
    
    public partial class messages
    {
        public int id { get; set; }
        public Nullable<int> conversation_id { get; set; }
        public Nullable<int> messagable_id { get; set; }
        public string messagable_type { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}
