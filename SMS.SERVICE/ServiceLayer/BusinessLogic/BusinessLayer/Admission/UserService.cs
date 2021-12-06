using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.ICommunicationServiceInternal;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.IService.ICommonService;
using SMS.SERVICE.ServiceLayer.Security;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.Admission
{
    public class UserService : IUserService
    {
        private readonly IUserServiceInternal userServiceInternal;
        private readonly IAddressService addressService;
        private readonly IPhoneNumberServiceInternal phoneNumberService;

        public UserService()
        {
            userServiceInternal = Singleton.GetUserServiceInternal();
            addressService = Singleton.GetAddressService();
            phoneNumberService = Singleton.GetPhoneNumberServiceInternal();
        }

        public UserModel CreateUser(UserModel _user)
        {
            validateUser(_user);

            byte[] _passwordHash;
            byte[] _passwordSalt;

            CreatePasswordHash(_user.Password, out _passwordHash, out _passwordSalt);

            using(var context = new SCMSEntities())
            {
                User _userObject = userServiceInternal.AddUser(_user, _passwordHash, _passwordSalt);
                context.Users.Add(_userObject);
                context.SaveChanges();

                _user.UserId = _userObject.UserID;

                //if User is saved
                if (_user.UserId > 0)
                {
                    if (_user.Address != null)
                    {
                        _user.Address.ForEach(x => x.ReferenceID = _user.UserId);//assign new user id to address
                        addressService.CreateAddress(_user.Address);
                    }

                    if (_user.Location != null)
                    {
                        _user.Location.RefernceId = _user.UserId;//assign new user id to address
                        addressService.CreateLocation(_user.Location);

                    }
                }
            }

            return _user;
        }

        #region security
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentException("Password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value Can not be empty or whitespace.", "password");
            }

            using (System.Security.Cryptography.HMACSHA512 _hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = _hmac.Key;
                passwordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
        public User Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw CommonMethods.GetException(CustomResponse.USER_NAME_AND_PASSWORD_DOESNT_EXIST);
            }
            User _user = userServiceInternal.GetUserByUserName(userName);
            if (_user == null)
            {
                throw CommonMethods.GetException(CustomResponse.USER_NAME_AND_PASSWORD_DOESNT_EXIST);
            }

            //check if password is correct
            //store password and salt as byte arrays
            if (!VerifyPasswordHash(password, _user.Password, _user.Salt))
            {
                throw CommonMethods.GetException(CustomResponse.USER_NAME_AND_PASSWORD_DOESNT_EXIST);
            }
            //autentication successfull// j
            return _user;
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid Length of Password Hash (64 bytes expected)", "PasswordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid Length of Password Salt (128 bytes expected)", "PasswordHash");
            }

            using (System.Security.Cryptography.HMACSHA512 _hashMac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var _computedHash = _hashMac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < _computedHash.Length; i++)
                {
                    if (_computedHash[i] != storedHash[i])
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        #endregion

        #region user validation
        private void validateUser(UserModel model)
        {
            List<SCMSException> errorMessages = new List<SCMSException>();
            User _checkUser = userServiceInternal.GetUserByUserName(model.UserName);
            if (_checkUser != null)
            {

                errorMessages.Add(CommonMethods.GetException(CustomResponse.USER_NAME_ALREADY_EXISTS));
            }

            User _checkPhone = userServiceInternal.GetUserByPhoneNumber(model.PhoneNumber);
            if (_checkPhone != null)
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.PHONE_NUMBER_ALREADY_EXISTS));
            }
            if (!String.IsNullOrEmpty(model.Email))
            {
                User _checkEmail = userServiceInternal.GetUserByEmail(model.Email);
                if (_checkEmail != null)
                {
                    errorMessages.Add(CommonMethods.GetException(CustomResponse.EMAIL_ALREADY_EXISTS));
                }
            }
            if (!phoneNumberService.CheckPhoneNumber(model.PhoneNumber))
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.PHONE_NUMBER_FORMAT_ERROR));
            }
            else if (!phoneNumberService.ValidatePhoneNumber(model.PhoneNumber))
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.PHONE_NUMBER_NOT_SUPPORTED));
            }
            CommonMethods.ThrowError(errorMessages);
        }
        public User GetUserById(int userId)
        {
            return userServiceInternal.GetUserById(userId);
        }

        public bool isUserEmployee(int userId)
        {
            var user = userServiceInternal.GetUserById(userId);
            if(user != null)
            {
                if (user.NameSpace == ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)
                    return true;
            }
            return false; 
        }

        public bool isUserStudent(int userId)
        {
            var user = userServiceInternal.GetUserById(userId);
            if (user != null)
            {
                if (user.NameSpace == ConstantValues.LOOKUP_VALUE_NAMESPACE_STUDENT)
                    return true;
            }
            return false;
        }
        #endregion
    }
}
