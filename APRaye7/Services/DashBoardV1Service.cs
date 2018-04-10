using APRaye7.Models;
using APRaye7.Models.ViewModels;
using APRaye7.Models.ViewModels.DashBoardVMs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APRaye7.Services
{
    public class DashBoardV1Service : ServicesBase
    {
        public int? getNumberOfTripsToday ()
        {
            int? todaytrips = context.trips.Where(t => t.soft_deleted==false && DbFunctions.TruncateTime(t.departure_time.Value) == DbFunctions.TruncateTime(DateTime.Now)).Count();
            if (todaytrips == null)
                todaytrips = 0;
            return todaytrips;
        }

        //public string getMostVisitedPlace()
        //{
        //    var mostvisitedID = context.trips.Where(t => t.deleted == false).GroupBy(t => t.FK_DestinationID).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
           
        //    if (mostvisitedID != null)
        //    {
        //        string mostVisitedPlaceName = GetPlaceNamebyID(mostvisitedID);
        //        return mostVisitedPlaceName;
        //    }
               
        //    else
        //        return "";

        //}
        //public string getMostVisitedSource()
        //{
        //    var mostvisitedID = context.trips.Where(t => t.deleted == false).GroupBy(t => t.FK_SourceID).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
        //    string mostVisitedPlaceName = GetPlaceNamebyID(mostvisitedID);
        //    return mostVisitedPlaceName;
        //}
        //public string getDriverWithMostTrips()
        //{
        //    var mostDriverID = context.trips.Where(t => t.deleted == false).GroupBy(t => t.driver_id).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
        //    string mostVisitedDriverName = GetUserNamebyID(mostDriverID);
        //    return mostVisitedDriverName;
        //}

        public int? getNumberOfUsers()
        {
            int? numofUsers = context.users.Where(u=>u.soft_deleted == false).Count();
            return numofUsers;
        }
        public int? getNumberOfUsers(DateTime start, DateTime End)
        {
            int? numofUsers = context.users.Where(u => u.soft_deleted==false && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End)).Count();
            return numofUsers;
        }

        //public string getUserWithMostTrips()
        //{
        //    var userName = context.trips.Where(t => t.deleted == false).GroupBy(t => t.driver_id).OrderByDescending(g => g.Count()).Select(g => g.Key).First();
        //    string Name = GetUserNamebyID(userName.Value);
        //    return Name;
        //}

        public DashBoardV1VM getDashBoardVM()
        {
            DashBoardV1VM newVM = new DashBoardV1VM();
            //newVM.NumberOfTripsToday = getNumberOfTripsToday();
            //newVM.MostVisitedPlace = getMostVisitedPlace();
            //newVM.UserWithMostTrips = getUserWithMostTrips();
            newVM.TotalNumberOfUsers = getNumberOfUsers();
            newVM.SmokersPercentage = getSmokersPercentage();
            newVM.FemaleUsersNumber = getNumberOfFemaleUsers();
            newVM.MaleUsersNumber = getNumberOfMaleUsers();
            newVM.MaleUsersPercentage = (float)Math.Round(((float)newVM.MaleUsersNumber / (float)newVM.TotalNumberOfUsers) * 100,2);
            newVM.FemaleUsersPercentage = (float)Math.Round(((float)newVM.FemaleUsersNumber / (float)newVM.TotalNumberOfUsers) * 100,2);
            newVM.UsersWithCar = getUsersWithCar();
            newVM.UsersWithoutCar = getUsersWithoutCar();
            newVM.UsersWithCarPercentage = (float)Math.Round(((float)newVM.UsersWithCar / (float)newVM.TotalNumberOfUsers) * 100,2);
            newVM.UsersWithoutCarPercentage = (float)Math.Round(((float)newVM.UsersWithoutCar / (float)newVM.TotalNumberOfUsers) * 100,2);

            return newVM;
        }

        public int? getNumberOfMaleUsers()
        {
            int? numofMales = context.users.Where(u => u.soft_deleted == false && u.gender == 0).Count();
            return numofMales;
        }
        public int? getNumberOfMaleUsers(DateTime start,DateTime End)
        {
            int? numofMales = context.users.Where(u =>u.soft_deleted == false && u.gender == 0 && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End)).Count();
            return numofMales;
        }

        public int? getNumberOfFemaleUsers()
        {
            int? numofFemales = context.users.Where(u =>u.soft_deleted == false && u.gender == 1).Count();
            return numofFemales;
        }
        public int? getNumberOfFemaleUsers(DateTime start, DateTime End)
        {
            int? numofFemales = context.users.Where(u => u.soft_deleted == false && u.gender == 1 && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End)).Count();
            return numofFemales;
        }
        public float? getSmokersPercentage()
        {
            var smokPercentage = ((context.users.Where(u => u.smoking==true && u.soft_deleted==false).Count()) / (context.users.Count())) * 100;
            return smokPercentage;
        }

        public int? getUsersWithCar()
        {
            var HaveCarPercentage = (context.users.Where(u => u.have_car == true && u.soft_deleted == false).Count());
            return HaveCarPercentage;
        }
        public int? getUsersWithCar(DateTime start, DateTime End)
        {
            var HaveCarPercentage = (context.users.Where(u => u.have_car == true && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End) && u.soft_deleted == false).Count());
            return HaveCarPercentage;
        }

        public int? getUsersWithoutCar()
        {
            var HaveCarPercentage = (context.users.Where(u => u.have_car == false && u.soft_deleted == false).Count());
            return HaveCarPercentage;
        }
        public int? getUsersWithoutCar(DateTime start, DateTime End)
        {
            var HaveCarPercentage = (context.users.Where(u => u.have_car == false && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End) && u.soft_deleted == false).Count());
            return HaveCarPercentage;
        }

        public List<BarChartCountPerBranch> getTopTenVisited()
        {
            var listOfTopTenBranches = context.TopTenBranchesNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfTopTenBranches;
        }
        public List<BarChartCountPerBranch> getTopTenVisited(DateTime start, DateTime End)
        {
            var listOfTopTenBranches = context.TopTenBranches(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfTopTenBranches;
        }

        public int? getTotalKilometersMade(DateTime fromDate, DateTime toDate)
        {
            int? KMsCount = context.trips.Where(t => t.soft_deleted == false && (DbFunctions.TruncateTime(t.departure_time) >= fromDate && DbFunctions.TruncateTime(t.departure_time) <= toDate)).Sum(t => t.points);
            if (KMsCount == null)
                KMsCount = 0;

            return KMsCount;
        }
        public int? getTotalKilometersMade()
        {
            int? KMsCount = context.trips.Where(t => t.soft_deleted == false).Sum(t => t.points);
            if (KMsCount == null)
                KMsCount = 0;
            return KMsCount;
        }


        public int? getSmokers()
        {
            return context.users.Where(u => u.smoking == true && u.soft_deleted == false).Count();
        }
        public int? getSameGenders()
        {
            return context.users.Where(u => u.same_gender == true && u.soft_deleted == false).Count();
        }

        public List<BarChartCountPerBranch> getTripsPerBranch()
        {

            var listOfTripsPerBranches = context.TripsPerBranchNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.Name,Count=t.VisitsCount}).ToList();
            return listOfTripsPerBranches;
        }
        public List<BarChartCountPerBranch> getTripsPerBranch(DateTime start, DateTime End)
        {
            var listOfTripsPerBranches = context.TripsPerBranch(start,End).Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfTripsPerBranches;
        }

        public List<BarChartCountPerBranch> getPassengerTripsPerBranch()
        {

            var listOfPassengerTripsPerBranches = context.PassengerTripsPerBranchNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfPassengerTripsPerBranches;
        }
        public List<BarChartCountPerBranch> getPassengerTripsPerBranch(DateTime start, DateTime End)
        {
            var listOfPassengerTripsPerBranches = context.PassengerTripsPerBranch(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfPassengerTripsPerBranches;
        }

        public List<BarChartCountPerBranch> getDeletedTripsPerBranch()
        {

            var listOfDeletedTripsPerBranches = context.DeletedTripsPerBranchNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfDeletedTripsPerBranches;
        }
        public List<BarChartCountPerBranch> getDeletedTripsPerBranch(DateTime start, DateTime End)
        {
            var listOfDeletedTripsPerBranches = context.DeletedTripsPerBranch(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfDeletedTripsPerBranches;
        }

        public List<BarChartCountPerBranch> getDeletedPassengerTripsPerBranch()
        {

            var listOfDeletedPassengerTripsPerBranches = context.DeletedPassengerTripsNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfDeletedPassengerTripsPerBranches;
        }
        public List<BarChartCountPerBranch> getDeletedPassengerTripsPerBranch(DateTime start, DateTime End)
        {
            var listOfDeletedPassengerTripsPerBranches = context.DeletedPassengerTrips(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.Name, Count = t.VisitsCount }).ToList();
            return listOfDeletedPassengerTripsPerBranches;
        }

        public List<string> getAllBranchesNames()
        {
            return context.places.OrderBy(p=>p.id).Select(b => b.name).ToList();
        }

        public List<BarChartCountPerBranch> getUsersPerBranch()
        {
            var listOfUsersPerBranch = context.UsersPerBranchNoDate().Select(u => new BarChartCountPerBranch { BranchName = u.Name, Count = u.UsersCount }).ToList();


            return listOfUsersPerBranch;
        }
        public List<BarChartCountPerBranch> getUsersPerBranch(DateTime start, DateTime End)
        {
            var listOfUsersPerBranch = context.UsersPerBranch(start, End).Select(u => new BarChartCountPerBranch { BranchName = u.Name, Count = u.UsersCount }).ToList();

            return listOfUsersPerBranch;
        }

        public int getNumberOfActiveUsers()
        {
            var ActiveCount = context.users.Where(u=>u.soft_deleted == false).Select(u => u.last_seen).ToList();
            int count = 0;
            DateTime? CurrDate = DateTime.Now;
            foreach(var d in ActiveCount)
            {
                float monthDiff = ((CurrDate.Value.Year - d.Value.Year) * 12) + CurrDate.Value.Month - d.Value.Month;
                if (monthDiff < 1)
                    count++;
            }
            return count;
        }
        public int getNumberOfActiveUsers(DateTime start, DateTime End)
        {
            var ActiveCount = context.users.Where(u=> DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End && u.soft_deleted == false).Select(u => u.last_seen).ToList();
            int count = 0;
            DateTime? CurrDate = DateTime.Now;
            foreach (var d in ActiveCount)
            {
                float monthDiff = ((CurrDate.Value.Year - d.Value.Year) * 12) + CurrDate.Value.Month - d.Value.Month;
                if (monthDiff < 1)
                    count++;
            }
            return count;
        }

        public int getNumberOfInActiveUsers()
        {
            var InActiveCount = context.users.Where(u=>u.soft_deleted == false).Select(u => u.last_seen).ToList();
            int count = 0;
            DateTime? CurrDate = DateTime.Now;
            foreach (var d in InActiveCount)
            {
                float monthDiff = ((CurrDate.Value.Year - d.Value.Year) * 12) + CurrDate.Value.Month - d.Value.Month;
                if (monthDiff >= 1)
                    count++;
            }
            return count;
        }
        public int getNumberOfInActiveUsers(DateTime start, DateTime End)
        {
            var InActiveCount = context.users.Where(u => DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End && u.soft_deleted == false).Select(u => u.last_seen).ToList();
            int count = 0;
            DateTime? CurrDate = DateTime.Now;
            foreach (var d in InActiveCount)
            {
                float monthDiff = ((CurrDate.Value.Year - d.Value.Year) * 12) + CurrDate.Value.Month - d.Value.Month;
                if (monthDiff >= 1)
                    count++;
            }
            return count;
        }

        public int getRecurrentTrips()
        {
            int RecTrips = (from t in context.trips.Where(t=>t.soft_deleted==false)
                            join sch in context.schedules.Where(s => s.repeatable_type == "Trip")
                            on t.id equals sch.repeatable_id
                            
                            select t).Count();
            return RecTrips;
        }
        public int getRecurrentTrips(DateTime start, DateTime End)
        {
            int RecTrips = (from t in context.trips.Where(t => (DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End) && t.soft_deleted == false)
                            join sch in context.schedules.Where(s => s.repeatable_type == "Trip")
                            on t.id equals sch.repeatable_id
                            select t).Count();
            return RecTrips;  
        }

        public int getTotalTrips()
        {
            int count = context.trips.Where(t=>t.soft_deleted==false).Count();
            return count;
        }
        public int getTotalTrips(DateTime start, DateTime End)
        {
            int count = context.trips.Where(t => (DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End) && t.soft_deleted == false).Count();
            return count;
        }

        public int getNumberOfAcceptedTrips()
        {
            var count = context.trip_pickup_requests.Where(tp => tp.status == (int?)status.accepted && tp.trip_id.HasValue).Count();
            return count;
        }
        public int getNumberOfAcceptedTrips(DateTime start, DateTime End)
        {
            //var count = context.trip_pickup_requests.Where(t => DbFunctions.TruncateTime(t.trips.departure_time) >= start && DbFunctions.TruncateTime(t.Trip.departure_time) <= End && t.status == (int?)status.accepted && t.trip_id.HasValue).Count();
            var count = (from tp in context.trip_pickup_requests
                         join t in context.trips
                         on tp.trip_id equals t.id
                         where DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End && tp.status == (int)status.accepted
                         select t.id).Count();
            return count;

        }

        public int getNumberOfInvitedTrips()
        {
            var count = (from tp in context.trip_pickup_requests
                         join t in context.trips
                         on tp.trip_id equals t.id
                         where tp.status == (int)status.pending && tp.sender_id == t.driver_id 
                         select tp.trip_id).Count();
            return count;
        }
        public int getNumberOfInvitedTrips(DateTime start, DateTime End)
        {
            //var count = context.trip_pickup_requests.Where(t => t.status == (int?)status.pending && t.sender_id == t.trips.driver_id && (DbFunctions.TruncateTime(t.Trip.departure_time) >= start && DbFunctions.TruncateTime(t.Trip.departure_time) <= End) && t.trip_id.HasValue).Count();
            var count= (from tp in context.trip_pickup_requests
                            join t in context.trips
                            on tp.trip_id equals t.id
                            where tp.status == (int)status.pending && tp.sender_id == t.driver_id &&
                            (DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End)
                            select tp.trip_id).Count();
                           
            return count;
        }

        public int getNumberOfRequestedTrips()
        {
            //var count = context.trip_pickup_requests.Where(t => t.status == (int?)status.pending && t.sender_id == t.Pickups.passenger_id && t.pickup_id.HasValue).Count();
            var count = (from tp in context.trip_pickup_requests
                            join p in context.pickups
                            on tp.pickup_id equals p.id
                            where tp.status == (int)status.pending && tp.sender_id == p.passenger_id 
                          
                            select tp.pickup_id).Count();
            return count;
        }
        public int getNumberOfRequestedTrips(DateTime start, DateTime End)
        {
            //var count = context.trip_pickup_requests.Where(t => t.status == (int?)status.pending && t.sender_id == t.Pickups.passenger_id && (DbFunctions.TruncateTime(t.Trip.departure_time) >= start && DbFunctions.TruncateTime(t.Trip.departure_time) <= End) && t.pickup_id.HasValue).Count();
            var count = (from tp in context.trip_pickup_requests
                            join p in context.pickups
                            on tp.pickup_id equals p.id
                            where tp.status == (int)status.pending && tp.sender_id == p.passenger_id
                            && (DbFunctions.TruncateTime(p.departure_time_from) >= start && DbFunctions.TruncateTime(p.departure_time_to) <= End)
                            select tp.pickup_id).Count();
            return count;
        }

        public List<BarChartCountPerBranch> getTotalTripsPerAllBranches()
        {
            var List = context.TotalTripsPerBranchNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.BranchName, Count = t.VisitCount }).ToList();
            return List;
        }
        public List<BarChartCountPerBranch> getTotalTripsPerAllBranches(DateTime start, DateTime End)
        {
            var listOfTripsPerBranches = context.TotalTripsPerBranch(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.BranchName, Count = t.VisitCount }).ToList();
            return listOfTripsPerBranches;
        }

        public double getCarbonSavings()
        {
            var USDPerTon = context.CarbonSavingsNoDate().Select(t => t.Value).SingleOrDefault() ;
            return USDPerTon;
        }
        public double getCarbonSavings(DateTime start, DateTime End)
        {
            var USDPerTon = context.CarbonSavings(start,End).Select(t => t.Value).SingleOrDefault();

            return USDPerTon;
        }

        public int? getUserPoints()
        {
            var p = context.users.Where(u=>u.soft_deleted==false).Sum(u => u.user_points);
            if (p == null)
                p = 0;
            return p;
        }
        public int? getDrivenPoints()
        {
            var p = context.users.Where(u => u.soft_deleted == false).Sum(u => u.driven_points);
            if (p == null)
                p = 0;
            return p;
        }
        public int? getRiddenPoints()
        {
            var p = context.users.Where(u => u.soft_deleted == false).Sum(u => u.ridden_points);
            if (p == null)
                p = 0;
            return p;
        }

        public ReportVM FillReportData(DateTime start, DateTime End)
        {
            ReportVM newRep = new ReportVM();
            newRep.TotalUsersTillToday = context.users.Where(u => u.soft_deleted == false && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End)).Count();
            newRep.totalPostedTrips = context.trips.Where(t =>  t.soft_deleted == false && (DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End)).Count()+context.pickups.Where(p=>(DbFunctions.TruncateTime(p.departure_time_from) >= start && DbFunctions.TruncateTime(p.departure_time_to) <= End)).Count();
            newRep.NumberOfCancelledTrips = context.trips.Where(t => t.deleted == true && t.soft_deleted == false && (DbFunctions.TruncateTime(t.departure_time) >= start && DbFunctions.TruncateTime(t.departure_time) <= End)).Count();
            newRep.TripsPerBuilding = context.TotalTripsPerBranch(start, End).Select(t => new BarChartCountPerBranch { BranchName = t.BranchName, Count = t.VisitCount}).ToList();
            newRep.TopUsers = context.users.Where(u => u.soft_deleted == false && (DbFunctions.TruncateTime(u.created_at) >= start && DbFunctions.TruncateTime(u.created_at) <= End)).Select(u => new BarChartCountPerBranch { BranchName = u.first_name + u.last_name, Count = u.user_points }).ToList();
            return newRep;
        }
        public ReportVM FillReportData()
        {
            ReportVM newRep = new ReportVM();
            newRep.TotalUsersTillToday = context.users.Where(u => u.soft_deleted == false).Count();
            newRep.totalPostedTrips = context.trips.Where(t =>  t.soft_deleted == false ).Count()+context.pickups.Count();
            newRep.NumberOfCancelledTrips = context.trips.Where(t => t.deleted == true && t.soft_deleted == false).Count();
            newRep.TripsPerBuilding = context.TotalTripsPerBranchNoDate().Select(t => new BarChartCountPerBranch { BranchName = t.BranchName, Count = t.VisitCount }).ToList();
            newRep.TopUsers = context.users.Where(u => u.soft_deleted == false).Select(u => new BarChartCountPerBranch { BranchName = u.first_name +" "+u.last_name, Count = u.user_points }).OrderByDescending(u=>u.Count).ToList();
            return newRep;
        }
    }
}