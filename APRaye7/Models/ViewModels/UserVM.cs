using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace APRaye7.Models.ViewModels
{
    public class UserVM
    {
        public int UserID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }

        public string Phone_Number { get; set; }
        public string Gender { get; set; }
        public System.DateTime? Birth_Date { get; set; }
        public string Have_Car { get; set; }
        public bool Have_A_Car { get; set; }
        public string Smoking { get; set; }
        public bool Smoking_bool { get; set; }
        public string Same_Gender { get; set; }
        public bool Same_Gender_bool { get; set; }
        public string Provider { get; set; }
      
        public int? Rating_Counter { get; set; }
        public double? Rating { get; set; }
        public string Business_Purpose { get; set; }
        public int? Referral_Points { get; set; }

       
        public int? User_Points { get; set; }
        public int? Driven_Points { get; set; }
        public int? Ridden_Points { get; set; }
        public int? SMS_Count { get; set; }
        public string Tokens { get; set; }
        public System.DateTime? Last_Seen { get; set; }
        public string Last_Seen_String { get; set; }
        public HttpPostedFileBase Personal_Image_File { get; set; }
        public string  Personal_Image { get; set; }
        public DateTime? Current_Sign_In_At { get; set; }
        
        public int? Sign_In_Count { get; set; }
        public System.DateTime? Created_At { get; set; }
        public System.DateTime? Updated_At { get; set; }
        public int NumberOfRides { get; set; }
        public int ChangeLogId { get; set; }
        public int NumerOfDriverTrips { get; set; }
        public int NumberOfPassengerTrips { get; set; }
        public int NumberOfCancelledDriverTrips { get; set; }
        public int NumberOfCancelledPassengerTrips { get; set; }
        public string Branch { get; set; }
        public int? BranchID { get; set; }

    }
}