using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Request;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Response;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.ServiceLayer.Security.SecurityService;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)]
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_IT_STAFF)]
    [Route(ConstantValues.EMPLOYEE_LINK + "/[controller]")]
    [ServiceFilter(typeof(ItAdminAccessAPIFilter))]
    [ApiController]
    public class ManageEmployeeController : ControllerBase
    {
        private IEnrollmentService _enrollmentService;
        private IUserService _userService;
        private IItAdminSecurityService securityService;
        private ISchoolService schoolService;
        private ILookupService lookupService;
        private IMapper _mapper;

        public ManageEmployeeController(ISchoolService schoolService, IItAdminSecurityService securityService, 
            IEnrollmentService enrollmentService, IUserService userService, ILookupService lookupService, IMapper mapper)
        {
            this.securityService = securityService;
            this.schoolService = schoolService;
            this.lookupService = lookupService;
            _enrollmentService = enrollmentService;
            _userService = userService;
            _mapper = mapper;
        }

        #region Employee

        [HttpPost("addEmployee")]
        public EmployeeResponse AddEmployee([FromBody] AddEmployeeRequest request)
        {
            EmployeeResponse response = new EmployeeResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            EmployeeModel model = new EmployeeModel()
            {
                UserId = request.UserId,
                BatchId = info.BatchId,
                BranchId = info.BranchId,
                createdBy = info.UserID,
                EmployeeType = request.EmployeeType,
                StartDate = request.StartDate,
                updatedBy = info.UserID
            };
            response.employee = _enrollmentService.AddEmployee(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("addUserAndEmployee")]
        public UserEmployeeResponse AddEmployee([FromBody] AddUserEmployeeRequest request)
        {
            UserEmployeeResponse response = new UserEmployeeResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            UserModel user = GetUserModel(request);
            user = _userService.CreateUser(user);
            if (user.UserId > 0)
            {
                EmployeeModel model = new EmployeeModel()
                {
                    UserId = user.UserId,
                    BatchId = info.BatchId,
                    BranchId = info.BranchId,
                    createdBy = info.UserID,
                    EmployeeType = request.EmployeeType,
                    StartDate = request.StartDate,
                    updatedBy = info.UserID
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

        private static UserModel GetUserModel(AddUserEmployeeRequest request)
        {
            return new UserModel()
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
                UserName = request.UserName
            };
        }

        [HttpGet("getEmployees")]
        public EmployeeListResponse GetEmployee()
        {
            EmployeeListResponse response = new EmployeeListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.employee = _enrollmentService.GetEmployeesBySchool(info.BatchId, info.SchoolId, info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion

        #region Teacher

        [HttpPost("assignTeacher")]
        public AssignedTeacherResponse AssignTeacher([FromBody] AssignTeacherRequest request)
        {
            AssignedTeacherResponse response = new AssignedTeacherResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.isTeacherAccessableByBranch(info.BatchId, info.SchoolId, info.BranchId, request.EmployeeId))
            {
                CommonMethods.SetResponse(response, CustomResponse.EMPLOYEE_IS_NOT_A_MEMBER_OF_A_BRANCH);
                return response;
            }
            if (!securityService.CanEmployeeAccessClassDataWithSubject(info.classes, request.ClassId, request.SectionId, request.SubjectId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
                return response;
            }
            TeacherModel model = new TeacherModel()
            {
                SubjectId = request.SubjectId,
                SectionId = request.SectionId,
                ClassId = request.ClassId,
                EmployeeId = request.EmployeeId,
                StartDate = request.StartDate,
                TeacherTypeId = request.TeacherTypeId,
                createdBy = info.UserID,
                updatedBy = info.UserID
            };
            response.teacher = _enrollmentService.AddTeacher(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getTeachers")]
        public EmployeeListResponse GetTeachers()
        {
            EmployeeListResponse response = new EmployeeListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.employee = _enrollmentService.GetTeacherEmployeesBySchool(info.BatchId, info.SchoolId, info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("getAssignedTeachersByClass")]
        public AssignedTeacherListResponse GetAssignedTeachersByClass([FromBody] GetAssignedTeachersByClassRequest request)
        {
            AssignedTeacherListResponse response = new AssignedTeacherListResponse();

            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClass(info.classes, request.classId))
            {
                response.responseCode = CommonMethods.GetErrorCode(CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
                response.responseMessage = CommonMethods.GetErrorMessage(response.responseCode);
                return response;
            }

            response.teacher = _enrollmentService.GetAssignedTeacherByClass(info.BatchId, info.SchoolId, info.BranchId, request.classId, request.sectionId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("getAssignedTeachersByEmployee")]
        public SingleTeacherResponse GetAssignedTeachersByEmployee([FromBody] GetAssignedTeachersByEmployeeRequest request)
        {
            SingleTeacherResponse response = new SingleTeacherResponse();

            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            
            response.teacher = _enrollmentService.GetAssignedTeacherByEmployee(info.BatchId, info.SchoolId, info.BranchId, request.employeeId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [AllowAnonymous]
        [HttpGet("getTeacherTypes")]
        public StringListResponse GetAddressTypeForUser()
        {
            StringListResponse response = new StringListResponse();
            List<String> addressTypes = lookupService.GetTeacherTypes();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            response.lookups = addressTypes;
            return response;
        }
        #endregion
    }
}