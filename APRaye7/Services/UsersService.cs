using APRaye7.Models;
using APRaye7.Models.ViewModels;
using APRaye7.Shared;
using CIBAdminsDB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace APRaye7.Services
{
    public class UsersService : ServicesBase
    {
        public List<UserVM> GetAllUsers()
        {
            var listOfUsers = context.users.Where(u => u.soft_deleted == false).Select(s => new UserVM
            {
                UserID = s.id,
                Email = s.email,
                First_Name = s.first_name,
                Last_Name = s.last_name,
                User_Points = s.user_points,
                Gender = (s.gender == 0 ? "Male" : "Female"),
                Business_Purpose = s.business_purpose,
                Phone_Number = s.phone_number,
                Have_Car = (s.have_car == true ? "Yes" : "No"),

                Same_Gender = (s.same_gender == true ? "Yes" : "No"),

                Smoking = (s.smoking == true ? "Yes" : "No"),
                Last_Seen = s.last_seen,
                Driven_Points = s.driven_points,
                Current_Sign_In_At = s.current_sign_in_at,
                Sign_In_Count = s.sign_in_count,
                Created_At = s.created_at,
                Branch = (from cs in context.community_subscriptions
                         join c in context.communities
                         on cs.community_id equals c.id
                         where cs.user_id == s.id

                         select c.name).FirstOrDefault(),
                Birth_Date = s.birth_date,
                Provider = s.provider,
                Rating_Counter = s.rating_counter,
                Rating = s.rating,
                Referral_Points = s.referral_points,
                Ridden_Points = s.ridden_points,
                SMS_Count = s.sms_count,
                Tokens = s.tokens,
                Personal_Image = s.personal_image,
                Updated_At = s.updated_at
            }).OrderByDescending(s => s.First_Name).ToList();
            return listOfUsers;
        }
        public users getUserbyID(int? UID)
        {
            return context.users.FirstOrDefault(u => u.id == UID);
        }
        public UserVM getUserVMbyID(int? UID)
        {
            var user = context.users.FirstOrDefault(u => u.id == UID);
            UserVM UserViewModel = new UserVM();
            UserViewModel.UserID = user.id;
            UserViewModel.Email = user.email;
            UserViewModel.First_Name = user.first_name;
            UserViewModel.Last_Name = user.last_name;
            UserViewModel.User_Points = user.user_points;
            UserViewModel.Gender = (user.gender == 0 ? "Male" : "Female");
            UserViewModel.Business_Purpose = user.business_purpose;
            UserViewModel.Phone_Number = user.phone_number;
            UserViewModel.Have_Car = (user.have_car == true ? "Yes" : "No");
            UserViewModel.Same_Gender = (user.same_gender == true ? "Yes" : "No");
            UserViewModel.Smoking = user.smoking == true ? "Yes" : "No";
            UserViewModel.Last_Seen = user.last_seen;
            UserViewModel.Driven_Points = user.driven_points;
            UserViewModel.Current_Sign_In_At = user.current_sign_in_at;
            UserViewModel.Sign_In_Count = user.sign_in_count;
            UserViewModel.Created_At = user.created_at;
            UserViewModel.Personal_Image = user.personal_image;
            UserViewModel.NumberOfRides = context.trips.Where(t => t.driver_id == user.id).Count();
            UserViewModel.NumerOfDriverTrips = getNumberOfDriverTrips(UID);
            UserViewModel.NumberOfPassengerTrips = getNumberOfPassengerTrips(UID);
            UserViewModel.NumberOfCancelledDriverTrips = getNumberOfCancelledDriverTrips(UID);
            UserViewModel.NumberOfCancelledPassengerTrips = getNumberOfCancelledPassengerTrips(UID);
            UserViewModel.Branch = (from cs in context.community_subscriptions
                                    join c in context.communities
                                    on cs.community_id equals c.id
                                    where cs.user_id == user.id
                                    select c.name).FirstOrDefault();
            UserViewModel.BranchID = (from cs in context.community_subscriptions
                                      join c in context.communities
                                      on cs.community_id equals c.id
                                      where cs.user_id == user.id
                                      select c.id).FirstOrDefault();
            return UserViewModel;
        }
        public void CreateUser(UserVM _user)
        {
            users newUser = new users();
            newUser.email = _user.Email;
            newUser.first_name = _user.First_Name;
            newUser.last_name = _user.Last_Name;
            newUser.gender = int.Parse(_user.Gender);
            newUser.phone_number = _user.Phone_Number;
            newUser.business_purpose = _user.Business_Purpose;
            newUser.have_car = _user.Have_A_Car;
            newUser.smoking = _user.Smoking_bool;
            newUser.same_gender = _user.Same_Gender_bool;
            newUser.personal_image = _user.Personal_Image;
            newUser.soft_deleted = false;
            newUser.created_at = DateTime.Now;
            context.users.Add(newUser);
            community_subscriptions newSub = new community_subscriptions();
            newSub.community_id = _user.BranchID;
            newSub.user_id = _user.UserID;
            newSub.subscription_proof = _user.Email;
            newSub.approved = true;
            newSub.type = "EmailCommunitySubscription";
            newSub.created_at = DateTime.Now;
            context.community_subscriptions.Add(newSub);
            context.SaveChanges();
            NewChangeLog(newUser.id, "Users", "Create");

        }
        public bool DeleteUser(int? id)
        {
            bool isDeleted = false;
            try
            {

                var User = context.users.SingleOrDefault(u => u.id == id);
                User.soft_deleted = true;
                context.Entry(User).State = System.Data.Entity.EntityState.Modified;
                community_subscriptions newSub = context.community_subscriptions.Where(n => n.user_id == id).SingleOrDefault();
                context.community_subscriptions.Remove(newSub);
                context.SaveChanges();
                NewChangeLog(User.id, "Users", "Delete");
                isDeleted = true;
            }
            catch (Exception e)
            {
                isDeleted = false;
            }
            return isDeleted;
        }
        public void SaveEdit(UserVM _user)
        {
            users modifiedUser = context.users.FirstOrDefault(u => u.id == _user.UserID);
            List<ChangeLogDetail> listOfChanges = new List<ChangeLogDetail>();
            listOfChanges = new List<ChangeLogDetail>(getAllDetailsForUser(modifiedUser, _user));
            ChangeLog newChange = new ChangeLog();
            newChange.FK_SystemUserID = SessionController.User.SystemUserID;
            newChange.Entity = "Users";
            newChange.Action = "Edit";
            newChange.FK_RecordID = modifiedUser.id;
            newChange.LogDate = DateTime.Now;
            CIBcontext.ChangeLogs.Add(newChange);
            foreach (var log in listOfChanges)
            {
                log.ChangeLog = newChange;
                CIBcontext.ChangeLogDetails.Add(log);
            }
            try
            {
                CIBcontext.SaveChanges();
            }
           
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            modifiedUser.email = _user.Email;
            modifiedUser.first_name = _user.First_Name;
            modifiedUser.last_name = _user.Last_Name;
            modifiedUser.gender = int.Parse(_user.Gender);
            modifiedUser.phone_number = _user.Phone_Number;
            modifiedUser.business_purpose = _user.Business_Purpose;
            modifiedUser.have_car = _user.Have_A_Car;
            modifiedUser.smoking = _user.Smoking_bool;
            modifiedUser.same_gender = _user.Same_Gender_bool;
            modifiedUser.personal_image = _user.Personal_Image;
            modifiedUser.updated_at = DateTime.Now;
            context.Entry(modifiedUser).State = System.Data.Entity.EntityState.Modified;
            community_subscriptions newSub = context.community_subscriptions.Where(n => n.user_id == _user.UserID).SingleOrDefault();
            newSub.community_id = _user.BranchID;
            newSub.user_id = _user.UserID;
            newSub.subscription_proof = _user.Email;
            newSub.approved = true;
            newSub.type = "EmailCommunitySubscription";
            newSub.updated_at = DateTime.Now;
            context.Entry(newSub).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public List<ChangeLogDetail> getAllDetailsForUser(users oldUser, UserVM newUser)
        {
            List<ChangeLogDetail> listOfDetails = new List<ChangeLogDetail>();
            if (oldUser.email != newUser.Email)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "email", oldUser.email.ToString(), newUser.Email.ToString()));
            }
            if (oldUser.first_name != newUser.First_Name)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "First Name", oldUser.first_name.ToString(), newUser.First_Name.ToString()));

            }
            if (oldUser.last_name != newUser.Last_Name)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "Last Name", oldUser.last_name.ToString(), newUser.Last_Name.ToString()));
            }
            if ((oldUser.gender == 0 && newUser.Gender == "Female") || ((oldUser.gender == 1 && newUser.Gender == "Male")))
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "gender", oldUser.gender.ToString(), (newUser.Gender.ToString() == "Male" ? "0" : "1")));
            }
            if (oldUser.phone_number != newUser.Phone_Number)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "Phone Number", oldUser.phone_number.ToString(), newUser.Phone_Number.ToString()));
            }
            if (oldUser.business_purpose != newUser.Business_Purpose)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "Business Purpose", oldUser.business_purpose.ToString(), newUser.Business_Purpose.ToString()));
            }
            if (oldUser.have_car != newUser.Have_A_Car)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "Have A Car", oldUser.have_car.ToString(), (newUser.Have_Car == "Yes" ? "1" : "0")));

            }
            if (oldUser.smoking != newUser.Smoking_bool)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "smoking", oldUser.smoking.ToString(), (newUser.Smoking == "Yes" ? "1" : "0")));

            }
            if (oldUser.same_gender != newUser.Same_Gender_bool)
            {
                listOfDetails.Add(CreateChangeLogDetail("Users", "Same gender", oldUser.same_gender.ToString(), (newUser.Same_Gender == "Yes" ? "1" : "0")));

            }
            if (newUser.Personal_Image != null)
            {
                if (newUser.Personal_Image != oldUser.personal_image && oldUser.personal_image != null)
                    listOfDetails.Add(CreateChangeLogDetail("Users", "Personal Image", (oldUser.personal_image == null ? "" : oldUser.personal_image.ToString()), newUser.Personal_Image.ToString()));
                else
                    listOfDetails.Add(CreateChangeLogDetail("Users", "Personal Image", "", newUser.Personal_Image.ToString()));
            }
            return listOfDetails;
        }
        public int getNumberOfDriverTrips(int? uid)
        {
            var count = context.trips.Where(t => t.soft_deleted == false && t.driver_id == uid).Count();
            return count;
        }
        public int getNumberOfPassengerTrips(int? uid)
        {
            var count = context.pickups.Where(p => p.passenger_id == uid).Count();
            return count;
        }
        public int getNumberOfCancelledDriverTrips(int? uid)
        {
            return context.trip_pickup_requests.Where(tp => tp.sender_id == uid && (tp.status == (int)status.cancelled_by_driver)).Select(tp => tp.trip_id).Count();
        }
        public int getNumberOfCancelledPassengerTrips(int? uid)
        {
            return context.trip_pickup_requests.Where(tp => tp.sender_id == uid && (tp.status == (int)status.cancelled_by_passenger)).Select(tp => tp.pickup_id).Count();

        }

    }
}