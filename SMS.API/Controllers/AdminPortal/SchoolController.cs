using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.AdminPortal.Domain.Request;
using SMS.API.WEB.Controllers.AdminPortal.Domain.Response;
using SMS.API.WEB.Controllers.AdminPortal.Request;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.Security.SecurityService;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.Controllers.Admin_Portal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_TDWEB_ADMIN)]
    [Route(ConstantValues.TD_WEB_ADMIN_LINK + "/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private ISchoolService _schoolService;
        private ILookupService _lookupService;
        private IEnrollmentService _enrollmentService;
        private IUserService _userService;
        private JwtSettings jwt;
        private IMapper _mapper;
        public IMainSystemService mainSystemService { get; }

        public SchoolController(JwtSettings jwt, IEnrollmentService enrollmentService,
            IUserService userService, ISchoolService schoolService, ILookupService lookupService, IMainSystemService mainSystemService,
            IMapper mapper)
        {
            this.jwt = jwt;
            _enrollmentService = enrollmentService;
            _userService = userService;
            _schoolService = schoolService;
            _lookupService = lookupService;
            this.mainSystemService = mainSystemService;
            _mapper = mapper;
        }

        #region Register School

        [HttpPost("registerSchool")]
        public SchoolResponse RegisterSchool([FromBody] AddSchoolRequest request)
        {
            SchoolResponse response = new SchoolResponse();
            int userId = jwt.GetCurrentUserId(User);
            SchoolModel school = new SchoolModel()
            {
                Branches = request.Branches,
                createdBy = userId,
                DisplayName = request.DisplayName,
                Email = request.Email,
                HeadQaurterAddress = request.HeadQaurterAddress,
                HeadQuarterLocation = request.HeadQuarterLocation,
                SchoolDescription = request.SchoolDescription,
                SchoolName = request.SchoolName,
                TinNumber = request.TinNumber,
                updatedBy = userId,
                WebSite = request.WebSite
            };
            response.school = _schoolService.CreateSchool(school);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("updateSchool")]
        public SchoolResponse UpdateSchool([FromBody] UpdateSchoolRequest request)
        {
            SchoolResponse response = new SchoolResponse();
            int userId = jwt.GetCurrentUserId(User);
            SchoolModel school = new SchoolModel()
            {
                SchoolId = request.schoolId,
                Branches = request.Branches,
                createdBy = userId,
                DisplayName = request.DisplayName,
                Email = request.Email,
                HeadQaurterAddress = request.HeadQaurterAddress,
                HeadQuarterLocation = request.HeadQuarterLocation,
                PublicID = request.PublicID,
                SchoolDescription = request.SchoolDescription,
                SchoolName = request.SchoolName,
                TinNumber = request.TinNumber,
                updatedBy = userId,
                WebSite = request.WebSite
            };
            response.school = _schoolService.UpdateSchool(school);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getAllSchool")]
        public SchoolListResponse GetAllSchool()
        {
            SchoolListResponse response = new SchoolListResponse();
            response.schools = _schoolService.GetAllSchool();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getSchoolById/{schoolId:int}")]
        public SchoolResponse GetSchoolById(int schoolId)
        {
            SchoolResponse response = new SchoolResponse();
            SchoolModel school = _schoolService.GetSchoolById(schoolId);
            if (school != null)
            {
                response.school = school;
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                return response;
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHOOL_DOESNT_EXIST);
                return response;
            }
        }

        #endregion

        #region Branch
        [HttpPost("registerBranch")]
        public Admin_BranchsListResponse RegisterBranch([FromBody] Admin_AddBranchListRequest request)
        {
            Admin_BranchsListResponse response = new Admin_BranchsListResponse();
            int userId = jwt.GetCurrentUserId(User);
            if (!_schoolService.SchoolExists(request.SchoolId))
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHOOL_DOESNT_EXIST);
                return response;
            }
            List<BranchModel> branches = new List<BranchModel>();
            foreach (var branch in request.branches)
            {
                branches.Add(new BranchModel()
                {
                    BranchAddress = branch.BranchAddress,
                    BranchLocation = branch.BranchLocation,
                    IsBranchMain = branch.IsBranchMain,
                    BranchName = branch.BranchName,
                    SchoolId = request.SchoolId,
                    createdBy = userId,
                    updatedBy = userId,
                });
            }
            response.branches = _schoolService.CreateListOfBranch(branches);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("updateBranch")]
        public Admin_BranchResponse UpdateBranch([FromBody] Admin_UpdateBranchRequest request)
        {
            Admin_BranchResponse response = new Admin_BranchResponse();
            int userId = jwt.GetCurrentUserId(User);
            BranchModel model = new BranchModel()
            {
                BranchAddress = request.BranchAddress,
                BranchId = request.BranchId,
                BranchLocation = request.BranchLocation,
                BranchName = request.BranchName,
                IsActive = request.IsActive,
                SchoolId = request.SchoolId,
                updatedBy = userId,
                IsBranchMain = request.IsBranchMain
            };
            response.branch = _schoolService.UpdateBranch(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getBranchById/{branchId:int}")]
        public Admin_BranchResponse GetBranchById(int branchId)
        {
            Admin_BranchResponse response = new Admin_BranchResponse();
            BranchModel _branch = _schoolService.GetBranchById(branchId);
            if (_branch != null)
            {
                response.branch = _branch;
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                return response;
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.BRANCH_DOESNT_EXIST);
                return response;
            }
        }

        [HttpGet("getBranchBySchoolId/{schoolId:int}")]
        public Admin_BranchsListResponse GetBranchBySchoolId(int schoolId)
        {
            Admin_BranchsListResponse response = new Admin_BranchsListResponse();
            response.branches = _schoolService.GetAllBranchBySchoolId(schoolId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion

        #region School Employee
        [HttpPost("addEmployeeForSchool")]
        public Admin_UserEmployeeResponse AddSchoolEmployee([FromBody] Admin_AddUserEmployeeRequest request)
        {
            Admin_UserEmployeeResponse response = new Admin_UserEmployeeResponse();
            int userId = jwt.GetCurrentUserId(User);
            int batchId = mainSystemService.GetActiveBatchIdByBranchId(request.BranchId);
            UserModel user = GetUserModel(request, userId);
            if (user.UserId > 0)
            {
                EmployeeModel model = new EmployeeModel()
                {
                    UserId = user.UserId,
                    BatchId = batchId,
                    BranchId = request.BranchId,
                    createdBy = userId,
                    EmployeeType = ConstantValues.LOOKUP_VALUE_NAMESPACE_IT_STAFF,
                    StartDate = request.StartDate,
                    updatedBy = userId
                };
                response.user = user;
                response.employee = _enrollmentService.AddEmployee(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                return response;
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
            }
            return response;
        }

        private UserModel GetUserModel(Admin_AddUserEmployeeRequest request, int userId)
        {
            UserModel user = new UserModel()
            {
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                FirstName = request.FirstName,
                Gender = request.Gender,
                LastName = request.LastName,
                Location = request.Location,
                MiddleName = request.MiddleName,
                NameSpace = ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                PlaceOfBirth = request.PlaceOfBirth,
                UserName = request.UserName,
                createdBy = userId,
                updatedBy = userId
            };
            user = _userService.CreateUser(user);
            return user;
        }

        [HttpPost("addEmployee")]
        public Admin_EmployeeResponse AddEmployee([FromBody] Admin_AddEmployeeRequest request)
        {
            Admin_EmployeeResponse response = new Admin_EmployeeResponse();
            int userId = jwt.GetCurrentUserId(User);
            int batchId = mainSystemService.GetActiveBatchIdByBranchId(request.BranchId);
            EmployeeModel model = new EmployeeModel()
            {
                UserId = request.UserId,
                BatchId = batchId,
                BranchId = request.BranchId,
                createdBy = userId,
                EmployeeType = ConstantValues.LOOKUP_VALUE_NAMESPACE_IT_STAFF,
                StartDate = request.StartDate,
                updatedBy = userId
            };
            response.employee = _enrollmentService.AddEmployee(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
        #endregion

        #region look up
        [AllowAnonymous]
        [HttpPost("getListOfAddressTypeForSchool")]
        public StringListResponse GetAddressTypeForUser()
        {
            StringListResponse response = new StringListResponse();
            response.lookups = _lookupService.GetAddressTypeForSchool();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;

        }
        #endregion
    }
}