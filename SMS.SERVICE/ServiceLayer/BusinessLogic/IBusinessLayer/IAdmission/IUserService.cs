using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission
{
    public interface IUserService
    {
        User Authenticate(string userName, string password);
        UserModel CreateUser(UserModel _user);
        User GetUserById(int userId);
        bool isUserEmployee(int userId);
        bool isUserStudent(int userId);
    }
}
