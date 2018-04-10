using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class TripVM
    {
        //public TripVM (DateTime? departTime,int? _seats,int? _points,string fromPlace,string toPlace,string driverName,bool? _deleted,DateTime? creation,DateTime? update)
        //{
        //    this.Departure_Time = departTime;
        //    this.Seats = _seats;
        //    this.Points = _points;
        //    this.FK_SourceID = fromPlace;
        //    this.FK_DestinationID = toPlace;
        //    this.FK_DriverID = driverName;
        //    this.Deleted = _deleted;
        //    this.Created_At = creation;
        //    this.Updated_At = update;
        //}
        public int TripID { get; set; }
        public Nullable<System.DateTime> Departure_Time { get; set; }
        public string Departure_Time_String { get; set; }
        public Nullable<int> Seats { get; set; }
        public Nullable<int> Points { get; set; }
        public string FK_SourceName { get; set; }
        public int? FK_SourceID { get; set; }
        public string FK_DestinationName { get; set; }
        public int? FK_DestinationID { get; set; }
        public string FK_DriverName { get; set; }
        public int? FK_DriverID { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public string Created_At_String { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
        public string Updated_At_String { get; set; }
        public int ChangeLogId { get; set; }
        public string CancellationReason { get; set; }
        public string Cancelled { get; set; }

    }
}