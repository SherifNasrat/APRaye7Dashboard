using APRaye7.Models;
using APRaye7.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APRaye7.Shared;
using CIBAdminsDB;

namespace APRaye7.Services
{
    public class TripsService : ServicesBase
    {
        public List<TripVM> GetAllTrips()
        {
            var tempList = (from t in context.trips
                            join sp in context.places on t.source_id equals sp.id
                            join dp in context.places on t.destination_id equals dp.id
                            join u in context.users on t.driver_id equals u.id
                            where t.soft_deleted == false
                               select new {TripID = t.id,Departure_Time = t.departure_time,Seats = t.seats,Points = t.points,From = sp.name,FK_SourceID = t.source_id,To = dp.name,FK_DestinationID = t.destination_id,DriverName = u.first_name+" "+u.last_name, FK_DriverID = t.driver_id, t.created_at, t.updated_at,t.deleted }).AsQueryable();
            var listOfTrips = tempList.Select(m => new TripVM
            {
                TripID = m.TripID,
                Departure_Time = m.Departure_Time,
                Departure_Time_String = m.Departure_Time.ToString(),
                Seats = m.Seats,
                Points = m.Points,
                FK_SourceName = m.From,
                FK_SourceID = m.FK_SourceID,
                FK_DestinationName = m.To,
                FK_DestinationID = m.FK_DestinationID,
                FK_DriverName = m.DriverName,
                FK_DriverID = m.FK_DriverID,
                Cancelled = (m.deleted == true ? "Yes" : "No"),
            Created_At = m.created_at,
                Created_At_String = m.created_at.ToString(),
                Updated_At = m.updated_at,
                Updated_At_String = m.updated_at.ToString(),
                Deleted= (bool)m.deleted
                
            }).ToList();
            return listOfTrips;
        }
        public trips GetTripByID(int? TripID)
        {
            try
            {
               
                
                    var trip = context.trips.FirstOrDefault(t => t.id == TripID);
                    return trip;
                
                    
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public TripVM GetTripVM(int? TripID)
        {
            try
            {
                var Trip = GetTripByID(TripID);
                TripVM vmTrip = new TripVM();
                vmTrip.TripID = Trip.id;
                vmTrip.Departure_Time = Trip.departure_time;
                vmTrip.Seats = Trip.seats;
                vmTrip.Points = Trip.points;
                vmTrip.FK_SourceName = GetPlaceNamebyID(Trip.source_id);
                vmTrip.FK_SourceID = Trip.source_id;
                vmTrip.FK_DestinationName = GetPlaceNamebyID(Trip.destination_id);
                vmTrip.FK_DestinationID = Trip.destination_id;
                vmTrip.FK_DriverName = GetUserNamebyID(Trip.driver_id);
                vmTrip.FK_DriverID = Trip.driver_id;
                vmTrip.Deleted = (bool)Trip.deleted;
                vmTrip.Cancelled = (Trip.deleted == true ? "Yes" : "No");
                vmTrip.Created_At = Trip.created_at;
                vmTrip.Updated_At = Trip.updated_at;
                return vmTrip;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void CreateTrip(TripVM _trip)
        {
            trips modifiedTrip = new trips();
            modifiedTrip.departure_time = _trip.Departure_Time;
            modifiedTrip.seats = _trip.Seats;
            modifiedTrip.points = _trip.Points;
            modifiedTrip.source_id = _trip.FK_SourceID;
            modifiedTrip.destination_id = _trip.FK_DestinationID;
            modifiedTrip.driver_id = _trip.FK_DriverID;
            modifiedTrip.deleted = _trip.Deleted;
            modifiedTrip.soft_deleted = false;
            modifiedTrip.created_at = DateTime.Now;
            modifiedTrip.updated_at = DateTime.Now;
            context.trips.Add(modifiedTrip);
            context.SaveChanges();
            NewChangeLog(modifiedTrip.id, "Trips", "Create");
        }
        public void SaveEdit(TripVM _trip)
        {

            List<ChangeLogDetail> listOfChanges = new List<ChangeLogDetail>();
            trips modifiedTrip = GetTripByID(_trip.TripID);
            listOfChanges = new List<ChangeLogDetail>(getAllDetailsForTrip(modifiedTrip,_trip));
            ChangeLog newChange = new ChangeLog();
            newChange.FK_SystemUserID = SessionController.User.SystemUserID;
            newChange.Entity = "Trips";
            newChange.Action = "Edit";
            newChange.FK_RecordID = modifiedTrip.id;
            newChange.LogDate = DateTime.Now;
            CIBcontext.ChangeLogs.Add(newChange);
            foreach(var log in listOfChanges)
            {
                log.ChangeLog = newChange;
                CIBcontext.ChangeLogDetails.Add(log);
            }
            CIBcontext.SaveChanges();

            modifiedTrip.departure_time = _trip.Departure_Time;
            modifiedTrip.seats = _trip.Seats;
            modifiedTrip.points = _trip.Points;
            modifiedTrip.source_id = _trip.FK_SourceID;
            modifiedTrip.destination_id = _trip.FK_DestinationID;
            modifiedTrip.driver_id = _trip.FK_DriverID;
            modifiedTrip.created_at = _trip.Created_At;
            modifiedTrip.updated_at = DateTime.Now;
            modifiedTrip.deleted = _trip.Deleted;
            context.Entry(modifiedTrip).State = System.Data.Entity.EntityState.Modified;
            
            context.SaveChanges();
            
        }
        public bool DeleteTrip(int? TripID)
        {
            bool isDeleted = false;
            try
            {

                var Trip = context.trips.SingleOrDefault(u => u.id == TripID);
                Trip.soft_deleted = true;
                context.Entry(Trip).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                NewChangeLog(Trip.id, "Trips", "Delete");
                isDeleted = true;
            }
            catch(Exception e)
            {
                isDeleted = false;
            }
            return isDeleted;
        }
        public List<SelectListItem> DDLFK_DriverIDs()
        {
            var listOfUserIDs = context.users.Select(u => new SelectListItem{ Text = u.first_name + " " + u.last_name + " - " + u.email, Value = u.id.ToString() }).OrderBy(u => u.Text).ToList();
            return listOfUserIDs;
        }
        public List<SelectListItem> DDLFK_PlaceIDs()
        {
            var listOfPlaceIDs = context.places.Select(u => new SelectListItem{ Text = u.name, Value = u.id.ToString() }).OrderBy(u => u.Text).ToList();
            return listOfPlaceIDs;
        }
        public List<ChangeLogDetail> getAllDetailsForTrip(trips oldTrip,TripVM newTrip)
        {
            List<ChangeLogDetail> listOfDetails = new List<ChangeLogDetail>();
            if(oldTrip.departure_time != newTrip.Departure_Time)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Departure Time", oldTrip.departure_time.ToString(), newTrip.Departure_Time.ToString()));
            }
            if(oldTrip.seats != newTrip.Seats)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Seats", oldTrip.seats.ToString(), newTrip.Seats.ToString()));

            }
            if(oldTrip.points != newTrip.Points)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Points", oldTrip.points.ToString(), newTrip.Points.ToString()));
            }
            if (oldTrip.source_id != newTrip.FK_SourceID)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Source", oldTrip.source_id.ToString(), newTrip.FK_SourceID.ToString()));
            }
            if(oldTrip.destination_id != newTrip.FK_DestinationID)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Destination", oldTrip.destination_id.ToString(), newTrip.FK_DestinationID.ToString()));
            }
            if(oldTrip.driver_id != newTrip.FK_DriverID)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Driver", oldTrip.driver_id.ToString(), newTrip.FK_DriverID.ToString()));
            }
            if (oldTrip.deleted != newTrip.Deleted)
            {
                listOfDetails.Add(CreateChangeLogDetail("Trips", "Deleted", oldTrip.deleted.ToString(), newTrip.Deleted.ToString()));
            }
            return listOfDetails;
        }
        public string getCancellationReason(int tid)
        {
            var reason = context.feedbacks.Where(f => f.leavable_id == tid && f.leavable_type == "Trip").Select(t => t.comment).SingleOrDefault().ToString();
            return reason;
        }


    }
}