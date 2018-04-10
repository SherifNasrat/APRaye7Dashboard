using APRaye7.Models.ViewModels.DashBoardVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels.DashBoardVMs
{
    public class ReportVM
    {
        public int? TotalUsersTillToday { get; set; }
        public float? UsersIncreasePercentage { get; set; }
        public int? totalPostedTrips { get; set; }
        public float? TripsIncreasePercentage { get; set; }
        public List<BarChartCountPerBranch> TripsPerBuilding {get;set;}
        public List<BarChartCountPerBranch> TopUsers { get; set; }
        public int? NumberOfCancelledTrips { get; set; }
        public float CancelledTripsPercentage { get; set; }

    }
}