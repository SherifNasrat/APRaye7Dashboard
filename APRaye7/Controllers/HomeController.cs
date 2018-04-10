using APRaye7.Models.ViewModels.DashBoardVMs;
using APRaye7.Services;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APRaye7.Controllers
{
    public class HomeController : Controller
    {
        private DashBoardV1Service _dashboardv1Service = new DashBoardV1Service();
        public ActionResult Index()
        {
            var DashBoardModel = _dashboardv1Service.getDashBoardVM();
            return View(DashBoardModel);
        }
        public ActionResult MaleFemaleChart(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? numOfMales = _dashboardv1Service.getNumberOfMaleUsers(startDate, endDate);
                int? numOfFemales = _dashboardv1Service.getNumberOfFemaleUsers(startDate, endDate);

                var data = new[]
          {
            numOfMales,numOfFemales
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? numOfMales = _dashboardv1Service.getNumberOfMaleUsers();
                int? numOfFemales = _dashboardv1Service.getNumberOfFemaleUsers();
                var data = new[]
           {
            numOfMales,numOfFemales
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult HaveCarChart(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? UsersWithCar = _dashboardv1Service.getUsersWithCar(startDate, endDate);
                int? UsersWithoutCar = _dashboardv1Service.getUsersWithoutCar(startDate, endDate);
                var data = new[]
             {
                UsersWithCar,UsersWithoutCar
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? UsersWithCar = _dashboardv1Service.getUsersWithCar();
                int? UsersWithoutCar = _dashboardv1Service.getUsersWithoutCar();
                var data = new[]
                {
                UsersWithCar,UsersWithoutCar
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }

        }
        public ActionResult TripsPerBranch(string fromDate, string toDate)
        {

            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var BranchGroup = _dashboardv1Service.getTripsPerBranch(startDate, endDate);
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                var DeletedBranchGroup = _dashboardv1Service.getDeletedTripsPerBranch(startDate, endDate);

                var DeletedBranchValues = DeletedBranchGroup.Select(t => t.Count).ToList();

                var data = new object[]
          {
            BranchValues,BranchesNames,DeletedBranchValues
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                var BranchGroup = _dashboardv1Service.getTripsPerBranch();
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                var DeletedBranchGroup = _dashboardv1Service.getDeletedTripsPerBranch();
                var DeletedBranchValues = DeletedBranchGroup.Select(t => t.Count).ToList();



                var data = new object[]
          {
            BranchValues,BranchesNames,DeletedBranchValues
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult TopTenVisited(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var TopTenGroup = _dashboardv1Service.getTopTenVisited(startDate, endDate);
                var TopTenNames = TopTenGroup.Select(t => t.BranchName).ToList();

                var TopTenValues = TopTenGroup.Select(t => t.Count).ToList();
                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < TopTenValues.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }
                var data = new object[]
             {
                 TopTenValues,TopTenNames,colors
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                var TopTenGroup = _dashboardv1Service.getTopTenVisited();
                var TopTenNames = TopTenGroup.Select(t => t.BranchName).ToList();

                var TopTenValues = TopTenGroup.Select(t => t.Count).ToList();
                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < TopTenValues.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }
                var data = new object[]
                {
                TopTenValues,TopTenNames,colors
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult TotalKilometersMade(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? KMs = _dashboardv1Service.getTotalKilometersMade(startDate, endDate);
                var result = Json(KMs, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {

                int? KMs = _dashboardv1Service.getTotalKilometersMade();
                var result = Json(KMs, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult UsersPerBranch(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var BranchGroup = _dashboardv1Service.getUsersPerBranch(startDate, endDate);
                List<int?> UsersCount = BranchGroup.Select(u => u.Count).ToList();
                List<string> branchNames = BranchGroup.Select(b => b.BranchName).ToList();

                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < branchNames.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }

                var data = new object[]
          {
            UsersCount,branchNames,colors
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                var BranchGroup = _dashboardv1Service.getUsersPerBranch();
                List<int?> UsersCount = BranchGroup.Select(u => u.Count).ToList();
                List<string> branchNames = BranchGroup.Select(b => b.BranchName).ToList();
                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < branchNames.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }
                var data = new object[]
           {
            UsersCount,branchNames,colors
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult ActiveInActiveUsers(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? numOfActiveUsers = _dashboardv1Service.getNumberOfActiveUsers(startDate, endDate);
                int? numOfInActiveUsers = _dashboardv1Service.getNumberOfInActiveUsers(startDate, endDate);

                var data = new[]
          {
            numOfActiveUsers,numOfInActiveUsers
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? numOfActiveUsers = _dashboardv1Service.getNumberOfActiveUsers();
                int? numOfInActiveUsers = _dashboardv1Service.getNumberOfInActiveUsers();
                var data = new[]
           {
             numOfActiveUsers,numOfInActiveUsers
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult RecurrentTrips(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? RecurrentTrips = _dashboardv1Service.getRecurrentTrips(startDate, endDate);
                int? TotalTrips = _dashboardv1Service.getTotalTrips(startDate, endDate);
                var data = new[]
             {
                TotalTrips, RecurrentTrips
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? RecurrentTrips = _dashboardv1Service.getRecurrentTrips();
                int? TotalTrips = _dashboardv1Service.getTotalTrips();
                var data = new[]
                {
                TotalTrips, RecurrentTrips
            };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult TripsStatusChart(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? numOfAccetped = _dashboardv1Service.getNumberOfAcceptedTrips(startDate, endDate);
                int? numOfInvited = _dashboardv1Service.getNumberOfInvitedTrips(startDate, endDate);
                int? numOfRequested = _dashboardv1Service.getNumberOfRequestedTrips(startDate, endDate);

                var data = new[]
          {
            numOfAccetped,numOfInvited,numOfRequested
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? numOfAccetped = _dashboardv1Service.getNumberOfAcceptedTrips();
                int? numOfInvited = _dashboardv1Service.getNumberOfInvitedTrips();
                int? numOfRequested = _dashboardv1Service.getNumberOfRequestedTrips();

                var data = new[]
          {
            numOfAccetped,numOfInvited,numOfRequested
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult TotalTripsPerBranch(string fromDate, string toDate)
        {

            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var BranchGroup = _dashboardv1Service.getTotalTripsPerAllBranches(startDate, endDate);
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < BranchesNames.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }

                var data = new object[]
          {
            BranchValues,BranchGroup,colors
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                var BranchGroup = _dashboardv1Service.getTotalTripsPerAllBranches();
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                List<string> colors = new List<string>();
                var random = new Random();
                for (int i = 0; i < BranchesNames.Count(); i++)
                {

                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    colors.Add(color);
                }
                var data = new object[]
           {
            BranchValues,BranchesNames,colors
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult CarbonSavings(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var USDsPerTon = _dashboardv1Service.getCarbonSavings(startDate, endDate);
                var result = Json(USDsPerTon, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {

                var USDsPerTon = _dashboardv1Service.getCarbonSavings();
                var result = Json(USDsPerTon, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult PointsSystem(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                int? up = _dashboardv1Service.getUserPoints();
                int? dp = _dashboardv1Service.getDrivenPoints();
                int? rp = _dashboardv1Service.getRiddenPoints();

                var data = new[]
          {
            up,dp,rp
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                int? up = _dashboardv1Service.getUserPoints();
                int? dp = _dashboardv1Service.getDrivenPoints();
                int? rp = _dashboardv1Service.getRiddenPoints();

                var data = new[]
          {
            up,dp,rp
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult PassengerTripsPerBranch(string fromDate, string toDate)
        {

            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                var BranchGroup = _dashboardv1Service.getPassengerTripsPerBranch(startDate, endDate);
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                var DeletedBranchGroup = _dashboardv1Service.getDeletedPassengerTripsPerBranch(startDate, endDate);

                var DeletedBranchValues = DeletedBranchGroup.Select(t => t.Count).ToList();

                var data = new object[]
          {
            BranchValues,BranchesNames,DeletedBranchValues
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
            else
            {
                var BranchGroup = _dashboardv1Service.getPassengerTripsPerBranch();
                var BranchesNames = BranchGroup.Select(t => t.BranchName).ToList();

                var BranchValues = BranchGroup.Select(t => t.Count).ToList();
                var DeletedBranchGroup = _dashboardv1Service.getDeletedPassengerTripsPerBranch();
                var DeletedBranchValues = DeletedBranchGroup.Select(t => t.Count).ToList();



                var data = new object[]
          {
            BranchValues,BranchesNames,DeletedBranchValues
        };
                var result = Json(data, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
        public ActionResult ViewReport(string fromDate, string toDate)
        {
            ReportVM newRep = _dashboardv1Service.FillReportData();
            return View("ViewReport", newRep);

        }
        public ActionResult ViewReportPartial(string fromDate, string toDate)
        {
            if (fromDate != null && toDate != null)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime startDate = Convert.ToDateTime(fromDate, culture);
                DateTime endDate = Convert.ToDateTime(toDate, culture);
                ReportVM newRep = _dashboardv1Service.FillReportData(startDate, endDate);


                return PartialView("ReportBody", newRep);
            }
            else
            {
                ReportVM newRep = _dashboardv1Service.FillReportData();
                return PartialView("ReportBody", newRep);
            }
        }
        public FileResult ExportXlsDynamicColumnsGeneric(ReportVM reportDetails)
        {
            /*
             Total Number of Users -  Number
             Total posted          -  Number
             Total Cancelled       -  Number
             Trips Per Building - B1=>B(n-1)
              empty             - V1=>V(n-1)
              Top Ten Users -      U1=>U(n-1)
              empty                 V1=>V(n-1)
             */
            DataTable exportedReport = new DataTable();
            for (int i = 0; i < reportDetails.TripsPerBuilding.Count + 1; i++)
            {
                exportedReport.Columns.Add(new DataColumn());
            }
            exportedReport.Rows[0].ItemArray[0] = "Total Number Of Users Till Today";
            exportedReport.Rows[0].ItemArray[1] = reportDetails.TotalUsersTillToday;

            //IEnumerable entities = entitiesQueryable.ToDataSourceResult(request).Data;

            //step 0: check correctness of all property names
            //var entityType = entities.GetType().GetGenericArguments()[0];
            //foreach (var propertyName in propertyNames)
            //{
            //    var property = entityType.GetProperty(propertyName);
            //    if (property == null)
            //        return null;
            //}

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            var numOfColumns = reportDetails.TripsPerBuilding.Count() + 1;

            //Create a header row

            //Set the column names in the header row
            for (int columnIndex = 0; columnIndex < numOfColumns; columnIndex++)
            {


                //(Optional) set the width of the columns
                sheet.SetColumnWidth(columnIndex, 50 * 256);
            }

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 0;
            //Populate the sheet with values from the grid data
            //for (int i = 0; i < entitiesQueryable.Rows.Count; i++)
            //{
            //    //Create a new row
            //    var row = sheet.CreateRow(rowNumber++);

            //    for (int columnIndex = 0; columnIndex < propertyNames.Length; columnIndex++)
            //    {


            //        string str = entitiesQueryable.Rows[i][columnIndex].ToString();
            //        int valInt;
            //        double valDouble;
            //        if (int.TryParse(str, out valInt))
            //            row.CreateCell(columnIndex).SetCellValue(valInt);
            //        else if (double.TryParse(str, out valDouble))
            //            row.CreateCell(columnIndex).SetCellValue(valDouble);
            //        else
            //            row.CreateCell(columnIndex).SetCellValue(str);


            //    }
            //}

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return File(output.ToArray(), //The binary data of the XLS file
         "application/vnd.ms-excel", //MIME type of Excel files
         "Territory to Brick by Brick.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}