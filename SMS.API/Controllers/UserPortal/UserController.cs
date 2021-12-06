using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SMS.SERVICE.DTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.SMSBasic;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.API.WEB.Controllers.UserPortal.Request;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.API.WEB.Controllers.UserPortal.Response;

namespace SMS.API.WEB.Controllers.User
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ILookupService _lookupService;
        private IMapper _mapper;
        private IEnrollmentService enrollmentService;
        public JwtSettings Jwt { get; }

        public UserController(IUserService userService, IEnrollmentService enrollmentService,
            ILookupService lookupService, IMapper _mapper, JwtSettings jwt)
        {
            _userService = userService;
            _lookupService = lookupService;
            _mapper = _mapper;
            Jwt = jwt;
            this.enrollmentService = enrollmentService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public AuthenticatedUserReponse Authenticate([FromBody] AuthenticateRequest request)
        {
            AuthenticatedUserReponse response = new AuthenticatedUserReponse();
            var _user = _userService.Authenticate(request.UserName, request.Password);
            if (_user == null)
            {
                CommonMethods.SetResponse(response, CustomResponse.USER_NAME_AND_PASSWORD_DOESNT_EXIST);
                return response;
            }

            var employee = enrollmentService.GetActiveEmployeeByUserId(_user.UserID);

            response = GetUserModel(_user);

            if (employee != null)
            {
                response.EmployeeType = employee.EmployeeTypeId;
            }

            //jwt json web authentication

            response.Token = Jwt.GenerateToken(_user, response.EmployeeType);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [Authorize]
        [HttpGet("getUserInfo")]
        public AuthenticatedUserReponse GetUserInfo()
        {
            AuthenticatedUserReponse response = new AuthenticatedUserReponse();

            int userId = Jwt.GetCurrentUserId(User);
            var _user = _userService.GetUserById(userId);
            if (_user == null)
            {
                CommonMethods.SetResponse(response, CustomResponse.USER_NAME_AND_PASSWORD_DOESNT_EXIST);
                return response;
            }

            var employee = enrollmentService.GetActiveEmployeeByUserId(_user.UserID);

            response = GetUserModel(_user);

            if (employee != null)
            {
                response.EmployeeType = employee.EmployeeTypeId;
            }

            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [AllowAnonymous]
        [HttpPost("registerUser")]
        public UserReponse RegsiterUser([FromBody] UserModel userModel)
        {
            UserReponse response = new UserReponse();
            List<String> allowedNameSpace = _lookupService.GetNamespaceTypeForUser();
            if (String.IsNullOrEmpty(userModel.NameSpace))
            {
                CommonMethods.SetResponse(response, CustomResponse.NAMESPACE_NOT_PROVIDED);
                return response;
            }
            else if (!allowedNameSpace.Contains(userModel.NameSpace))
            {
                CommonMethods.SetResponse(response, CustomResponse.NAMESPACE_NOT_ALLOWED);
                return response;
            }
            response.user = _userService.CreateUser(userModel);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
        
        [AllowAnonymous]
        [HttpGet("getListOfNamespaceForUserRegistration")]
        public StringListResponse GetNamespaceForUserRegistration()
        {
            StringListResponse response = new StringListResponse();
            List<String> namespaceTypes = _lookupService.GetNamespaceTypeForUser();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            response.lookups = namespaceTypes;
            return response;
        }

        [AllowAnonymous]
        [HttpGet("getListOfAddressTypeForUser")]
        public StringListResponse GetAddressTypeForUser()
        {
            StringListResponse response = new StringListResponse();
            List<String> addressTypes = _lookupService.GetAddressTypeForUser();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            response.lookups = addressTypes;
            return response;
        }

        #region Get Model
        private AuthenticatedUserReponse GetUserModel(SCMS.DataAccess.User user)
        {
            if (user == null) return null;
            AuthenticatedUserReponse userModel = new AuthenticatedUserReponse()
            {
                UserId = user.UserID,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = user.FullName,
                NameSpace = user.NameSpace,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return userModel;
        }
        #endregion
    }

}