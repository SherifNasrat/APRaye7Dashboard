using CIBAdminsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static APRaye7.Models.ViewModels.SystemUser;

namespace APRaye7.Services
{
    public class AccountService : ServicesBase
    {
        public bool CheckRedundantForName(string userName, int? userId)
        {
            return !(CIBcontext.SystemUsers.Any(s => s.Username.ToLower().Trim() == userName.ToLower().Trim() && (s.SystemUserID != null ? s.SystemUserID != userId : s.SystemUserID == 0)));
        }
        public bool CheckRedundant(string email, int? userId)
        {
            var result = (CIBcontext.SystemUsers.Any(s => s.Email == email && (s.SystemUserID != null ? s.SystemUserID != userId : s.SystemUserID == 0)));
            return !result;
        }
        public SystemUser SystemUserVMToSystemUser(SystemUserVM user)
        {
            var User = new SystemUser();
            User.SystemUserID = user.SystemUserID;
            User.Username = user.Username;
            User.Email = user.Email;
            User.FullName = user.FullName;
            User.Password = user.Password;
            User.PersonalImage = user.PersonalImage;
            User.IsAdmin = user.IsAdmin;
            User.IsSuperAdmin = user.IsSuperAdmin;
            User.IsUser = user.IsUser;
            User.CreationDate = user.CreationDate;
            User.FK_Addedby = user.FK_Addedby;
            User.LastModifiedDate = user.LastModifiedDate;
            User.FK_LastModifiedBy = user.FK_LastModifiedBy;
          
            return User;
        }
        public SystemUserVM SystemUserToSystemUserVM(SystemUser user)
        {
            var User = new SystemUserVM();
            User.SystemUserID = user.SystemUserID;
            User.Username = user.Username;
            User.Email = user.Email;
            User.FullName = user.FullName;
            User.Password = user.Password;
            User.PersonalImage = user.PersonalImage;
            User.IsAdmin = user.IsAdmin;
            User.IsSuperAdmin = user.IsSuperAdmin;
            User.IsUser = user.IsUser;
            User.CreationDate = user.CreationDate;
            User.FK_Addedby = user.FK_Addedby;
            User.LastModifiedDate = user.LastModifiedDate;
            User.FK_LastModifiedBy = user.FK_LastModifiedBy;
            return User;

        }

        public string TryGenerateSecureToken(int systemUserID)
        {
            SystemUser user = CIBcontext.SystemUsers.SingleOrDefault(su => su.SystemUserID == systemUserID);
            if (user == null)
            {
                return null;
            }
            bool allowReRequest = user.SecureTokenExpiryTime == null
                                 || user.SecureTokenExpiryTime.Value.AddHours(-1).AddMinutes(5) < DateTime.Now;
            if (!allowReRequest)
                return null;
            var secureTokens = new HashSet<string>(CIBcontext.SystemUsers.Select(u => u.SecureToken).ToList());
            var rand = new Random();
            string tempPassword;
            do
            {
                tempPassword = LongRandom((long)(1UL << 61), (long)((1UL << 62) - 1), rand).ToString();
                user.SecureToken = CalculateSecureToken(tempPassword);
                user.SecureTokenExpiryTime = DateTime.Now.AddHours(1);
            } while (secureTokens.Contains(user.SecureToken));
            try
            {
                CIBcontext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return tempPassword;
        }
        public long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
        public string CalculateSecureToken(string tempPassword)
        {
            //string salt = "." + userName;
            //string toEncrypt = tempPassword + salt;
            string secureToken = Hash(tempPassword);
            return secureToken;
        }
        public static string Hash(String s)
        {
            HashAlgorithm Hasher = new SHA256CryptoServiceProvider();
            byte[] strBytes = Encoding.UTF8.GetBytes(s);
            byte[] strHash = Hasher.ComputeHash(strBytes);
            return BitConverter.ToString(strHash).Replace("-", "").ToLowerInvariant().Trim();
        }
        public List<SystemUserVM> getAllSystemUsersVM()
        {
            var rawusers = CIBcontext.SystemUsers.ToList();
            List<SystemUserVM> result = new List<SystemUserVM>();
            List<SystemUserVM> result2 = new List<SystemUserVM>();
            result2 = rawusers.Select(u => new SystemUserVM()
            {
                SystemUserID = u.SystemUserID,
                Username = u.Username,
                Email = u.Email,
                FullName = u.FullName,
                CreationDateString = Convert.ToString(u.CreationDate),
               // Role = (((u.IsUser == null ? "" : "User") == "" && u.IsAdmin == null ? "" : "Admin") == "" && u.IsSuperAdmin == null? "" : "Super Admin")
               Role = (u.IsUser == null ? (u.IsAdmin == null ? (u.IsSuperAdmin == null ? "":"Super Admin") :"Admin") :"User")
                
            }).ToList();
            //foreach (var sysuser in rawusers)
            //{
            //    result.Add(SystemUserToSystemUserVM(sysuser));
            //}
            return result2;
        }
    }
}