using SCMS.DataAccess;
using System;
using System.Linq;
using SMS.SERVICE.DTO;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;

namespace SMS.SERVICE.Services
{

    public class UserServiceInternal : IUserServiceInternal
    {
        public User AddUser(UserModel user, byte[] passwordHash, byte[] passwordSalt)
        {
            User _userObject = new User()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = string.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName),
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PlaceOfBirth = user.PlaceOfBirth,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberVerified = false,
                Email = user.Email,
                EmailVerified = false,
                UserName = user.UserName,
                NameSpace = user.NameSpace,
                Password = passwordHash,
                PwdChangeRequired = false,
                PwdChangeDate = DateTime.Now,
                FailedLoginCount = 0,
                IsEnabled = false,
                IsLocked = false,
                IsActive = true,
                LastLoginTime = DateTime.Now,
                LockedTime = null,                
                Salt = passwordSalt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };//implement public id , is creted by, updated by
           
            return _userObject;
        }
        public User UpdateUser(UserModel user, byte[] passwordHash, byte[] passwordSalt)
        {
            User _userObject = new User()
            {
                UserID = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = string.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName),
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PlaceOfBirth = user.PlaceOfBirth,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberVerified = false,
                Email = user.Email,
                EmailVerified = false,
                UserName = user.UserName,
                NameSpace = user.NameSpace,
                Password = passwordHash,
                PwdChangeRequired = false,
                PwdChangeDate = DateTime.Now,
                FailedLoginCount = 0,
                IsEnabled = false,
                IsLocked = false,
                IsActive = true,
                LastLoginTime = DateTime.Now,
                LockedTime = null,
                Salt = passwordSalt,
                UpdatedDate = DateTime.Now,
            };//implement public id , is creted by, updated by

            return _userObject;
        }
        public User GetUserByUserName(string userName)
        {
            using(var context = new SCMSEntities())
                return context.Users.Where(x => x.UserName == userName).FirstOrDefault();
        }
        public User GetUserByPhoneNumber(string phoneNumber)
        {
            using (var context = new SCMSEntities())
                return context.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
        }
        public User GetUserByEmail(string email)
        {
            using (var context = new SCMSEntities())
                return context.Users.Where(x => x.Email == email).FirstOrDefault();
        }
        public User GetUserById(int id)
        {
            using (var context = new SCMSEntities())
                return context.Users.Where(x => x.UserID == id).FirstOrDefault();
        }
        public sp_GetActiveEmployess_Result GetActiveEmployeResult(int userId)
        {
            using (var context = new SCMSEntities())
                return context.sp_GetActiveEmployess(userId).FirstOrDefault();
        }
        public sp_GetActiveStudents_Result GetActiveStudentResult(int userId)
        {
            using (var context = new SCMSEntities())
                return context.sp_GetActiveStudents(userId).FirstOrDefault();
        }
    }
}