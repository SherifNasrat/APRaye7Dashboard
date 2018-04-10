using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class ChangeLogVM
    {
        public int? ChangeLogId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public int? RecordNumber { get; set; }
        public string LogDate { get; set; }
       
    }
}