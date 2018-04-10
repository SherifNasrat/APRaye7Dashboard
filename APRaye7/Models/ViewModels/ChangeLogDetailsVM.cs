using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class ChangeLogDetailsVM
    {
        public int? ChangeLogDetailId { get; set; }
        public string Field { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public string DetailDate { get; set; }
    }
}