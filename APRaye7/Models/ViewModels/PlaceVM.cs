using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class PlaceVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public double? longitude { get; set; }
        public double? latitude { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public bool? soft_deleted { get; set; }
    }
}