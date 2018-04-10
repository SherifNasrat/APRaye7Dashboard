using APRaye7.Models;
using APRaye7.Shared;
using CIBAdminsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Services
{
    public class ServicesBase
    {
        private AP_Raye7DbEntities _db = new AP_Raye7DbEntities();
        public AP_Raye7DbEntities context { get { return _db; } set { _db = value; } }
        public CIBAdminsDB.CIBAdminsEntities _dbCIB = new CIBAdminsDB.CIBAdminsEntities();
        public CIBAdminsDB.CIBAdminsEntities CIBcontext
        {
            get { return _dbCIB; }
            set { _dbCIB = value; }
        }
        public string GetPlaceNamebyID(int? PlaceID)
        {
            try
            {
                var placeName = context.places.FirstOrDefault(p => p.id == PlaceID).name;
                return placeName;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string GetUserNamebyID(int? UserID)
        {
            try
            {
                var userFirstName = context.users.FirstOrDefault(u => u.id == UserID).first_name;
                var userLastName = context.users.FirstOrDefault(u => u.id == UserID).last_name;
                return userFirstName + " " + userLastName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public places GetPlacebyID(int? PlaceID)
        {
            try
            {
                return context.places.FirstOrDefault(p => p.id == PlaceID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void NewChangeLog(int id, string Entity, string ActionType)
        {
            switch (Entity)
            {
                case "Trips":

                    if (ActionType == "Create")
                    {
                        ChangeLog newTripAdded = new ChangeLog();
                        newTripAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newTripAdded.Action = "Create";
                        newTripAdded.Entity = "Trips";
                        newTripAdded.FK_RecordID = id;
                        newTripAdded.LogDate = DateTime.Now;
                        CIBcontext.ChangeLogs.Add(newTripAdded);
                    }
                    else if (ActionType == "Delete")
                    {
                        ChangeLog newTripAdded = new ChangeLog();
                        newTripAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newTripAdded.Action = "Delete";
                        newTripAdded.Entity = "Trips";
                        newTripAdded.FK_RecordID = id;
                        newTripAdded.LogDate = DateTime.Now;

                        CIBcontext.ChangeLogs.Add(newTripAdded);
                    }
                    break;
                case "Users":
                    if (ActionType == "Create")
                    {
                        ChangeLog newUserAdded = new ChangeLog();
                        newUserAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newUserAdded.Action = "Create";
                        newUserAdded.Entity = "Users";
                        newUserAdded.FK_RecordID = id;
                        newUserAdded.LogDate = DateTime.Now;
                        CIBcontext.ChangeLogs.Add(newUserAdded);
                    }
                    else if (ActionType == "Delete")
                    {
                        ChangeLog newUserAdded = new ChangeLog();
                        newUserAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newUserAdded.Action = "Delete";
                        newUserAdded.Entity = "Users";
                        newUserAdded.FK_RecordID = id;
                        newUserAdded.LogDate = DateTime.Now;
                        CIBcontext.ChangeLogs.Add(newUserAdded);
                    }
                    break;
                case "Branches":
                    if (ActionType == "Create")
                    {
                        ChangeLog newBranchAdded = new ChangeLog();
                        newBranchAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newBranchAdded.Action = "Create";
                        newBranchAdded.Entity = "Branches";
                        newBranchAdded.FK_RecordID = id;
                        newBranchAdded.LogDate = DateTime.Now;
                        CIBcontext.ChangeLogs.Add(newBranchAdded);
                    }
                    else if (ActionType == "Delete")
                    {
                        ChangeLog newBranchAdded = new ChangeLog();
                        newBranchAdded.FK_SystemUserID = SessionController.User.SystemUserID;
                        newBranchAdded.Action = "Delete";
                        newBranchAdded.Entity = "Branches";
                        newBranchAdded.FK_RecordID = id;
                        newBranchAdded.LogDate = DateTime.Now;
                        CIBcontext.ChangeLogs.Add(newBranchAdded);
                    }
                    break;

            }
            CIBcontext.SaveChanges();
        }
       
        public ChangeLogDetail CreateChangeLogDetail(string Entity, string Field, string OldValue, string NewValue)
        {
            switch (Entity)
            {
                case "Trips":
                    ChangeLogDetail newTripEditedDetails = new ChangeLogDetail();

                    newTripEditedDetails.Field = Field;
                    newTripEditedDetails.PreviousValue = OldValue;
                    newTripEditedDetails.NewValue = NewValue;
                    newTripEditedDetails.DetailDate = DateTime.Now;
                    return newTripEditedDetails;

                case "Users":
                    ChangeLogDetail newUserEditedDetails = new ChangeLogDetail();
                    newUserEditedDetails.Field = Field;
                    newUserEditedDetails.PreviousValue = OldValue;
                    newUserEditedDetails.NewValue = NewValue;
                    newUserEditedDetails.DetailDate = DateTime.Now;
                    return newUserEditedDetails;
                case "Branches":
                    ChangeLogDetail newBranchEditedDetails = new ChangeLogDetail();

                    newBranchEditedDetails.Field = Field;
                    newBranchEditedDetails.PreviousValue = OldValue;
                    newBranchEditedDetails.NewValue = NewValue;
                    newBranchEditedDetails.DetailDate = DateTime.Now;
                    return newBranchEditedDetails;
                default:
                    return new ChangeLogDetail();
            }
        }
        public enum status  {
            pending = 0,
            accepted = 1,
            cancelled_by_passenger = 2,
            rejected_by_passenger = 3,
            cancelled_by_driver = 4,
            rejected_by_driver = 5,
            timeout = 6,
            rejected_by_system = 7,
            deleted_by_driver= 8,
            deleted_by_passenger = 9
 };

    }
}