using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class DashBoardV1VM
    {
        public int? MaleUsersNumber { get; set; }
        public float? MaleUsersPercentage { get; set; }
        public int? FemaleUsersNumber { get; set; }
        public float? FemaleUsersPercentage { get; set; }
        public string MostVisitedPlace { get; set; }
        public int? NumberOfTripsToday { get; set; }
        public string UserWithMostTrips { get; set; }
        public int? TotalNumberOfUsers { get; set; }
        public float? SmokersPercentage { get; set; }
        public int? UsersWithCar { get; set; }
        public int? UsersWithoutCar { get; set; }
        public float? UsersWithCarPercentage { get; set; }
        public float? UsersWithoutCarPercentage { get; set; }
    }
}