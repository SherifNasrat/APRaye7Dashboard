using APRaye7.Models;
using APRaye7.Models.ViewModels;
using APRaye7.Shared;
using CIBAdminsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Services
{
    public class PlacesService : ServicesBase
    {
        public List<PlaceVM> getAllPlaces()
        {
            var listOfPlaces = context.places.Where(p => p.soft_deleted == false).Select(p=> new PlaceVM {id=p.id,name=p.name,longitude=p.longitude,latitude=p.latitude,description=p.description }).ToList();
            return listOfPlaces;
        }
        public PlaceVM getPlaceVM(int? id)
        {
            var tempPlace = context.places.Where(p => p.id == id).Select(p => new PlaceVM { id = p.id, name = p.name, longitude = p.longitude, latitude = p.latitude, description = p.description }).FirstOrDefault();
            return tempPlace;
        }
        public void SaveEdit(PlaceVM _branch)
        {

            List<ChangeLogDetail> listOfChanges = new List<ChangeLogDetail>();
            places modifiedBranch = context.places.Where(t=>t.id== _branch.id).FirstOrDefault();
            listOfChanges = new List<ChangeLogDetail>(getAllDetailsForPlace(modifiedBranch, _branch));
            ChangeLog newChange = new ChangeLog();
            newChange.FK_SystemUserID = SessionController.User.SystemUserID;
            newChange.Entity = "Branches";
            newChange.Action = "Edit";
            newChange.FK_RecordID = modifiedBranch.id;
            newChange.LogDate = DateTime.Now;
            CIBcontext.ChangeLogs.Add(newChange);
            foreach (var log in listOfChanges)
            {
                log.ChangeLog = newChange;
                CIBcontext.ChangeLogDetails.Add(log);
            }
            CIBcontext.SaveChanges();

            modifiedBranch.name = _branch.name;
            modifiedBranch.longitude = _branch.longitude;
            modifiedBranch.latitude = _branch.latitude;
            modifiedBranch.description = _branch.description;
            modifiedBranch.updated_at = DateTime.Now;
            modifiedBranch.soft_deleted = false;         
            context.Entry(modifiedBranch).State = System.Data.Entity.EntityState.Modified;
            communities editedCommunity = context.communities.Where(u => u.place_id == modifiedBranch.id).FirstOrDefault();
            editedCommunity.name = _branch.name;
            context.Entry(editedCommunity).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

        }
        public List<ChangeLogDetail> getAllDetailsForPlace(places oldBranch, PlaceVM newBranch)
        {
            List<ChangeLogDetail> listOfDetails = new List<ChangeLogDetail>();
            if (oldBranch.name != newBranch.name)
            {
                listOfDetails.Add(CreateChangeLogDetail("Branches", "Name", oldBranch.name.ToString(), newBranch.name.ToString()));
            }
            if (oldBranch.longitude != newBranch.longitude)
            {
                listOfDetails.Add(CreateChangeLogDetail("Branches", "Longitude", oldBranch.longitude.ToString(), newBranch.longitude.ToString()));

            }
            if (oldBranch.latitude != newBranch.latitude)
            {
                listOfDetails.Add(CreateChangeLogDetail("Branches", "Latitude", oldBranch.latitude.ToString(), newBranch.latitude.ToString()));
            }
            if (oldBranch.description != newBranch.description)
            {
                listOfDetails.Add(CreateChangeLogDetail("Branches", "Description", oldBranch.description.ToString(), newBranch.description.ToString()));
            }
            
            return listOfDetails;
        }
        public bool DeleteBranch(int? BranchID)
        {
            bool isDeleted = false;
            try
            {

                var Branch = context.places.SingleOrDefault(u => u.id == BranchID);
                Branch.soft_deleted = true;
                context.Entry(Branch).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                NewChangeLog(Branch.id, "Branches", "Delete");
                isDeleted = true;
            }
            catch (Exception e)
            {
                isDeleted = false;
            }
            return isDeleted;
        }
        public void CreateBranch(PlaceVM branch)
        {
            places modifiedbranch = new places();
            modifiedbranch.name = branch.name;
            modifiedbranch.longitude = branch.longitude;
            modifiedbranch.latitude = branch.latitude;
            modifiedbranch.description = branch.description;
            modifiedbranch.created_at = DateTime.Now;
            modifiedbranch.soft_deleted = false;
            context.places.Add(modifiedbranch);
            context.SaveChanges();

            communities newCommunity = new communities();
            newCommunity.name = branch.name;
            newCommunity.type = 0;
            newCommunity.verifier = SessionController.User.Email;
            newCommunity.visible = true;
            newCommunity.cluster_id = 1;
            newCommunity.place_id = modifiedbranch.id;
            context.communities.Add(newCommunity);
            context.SaveChanges();
            NewChangeLog(modifiedbranch.id, "Branches", "Create");


        }
    }
}