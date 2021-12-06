using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Request;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Response;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)]
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_IT_STAFF)]
    [Route(ConstantValues.EMPLOYEE_LINK + "/[controller]")]
    [ServiceFilter(typeof(ItAdminAccessAPIFilter))]
    [ApiController]
    public class ManageStudentController : ControllerBase
    {
        private IEnrollmentService _enrollmentService;
        private IItAdminSecurityService securityService;
        private IUserService userService;
        private IMapper _mapper;

        public ManageStudentController(IUserService userService, IItAdminSecurityService securityService, IEnrollmentService enrollmentService, IMapper mapper)
        {
            this.userService = userService;
            this.securityService = securityService;
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }

        [HttpPost("enrollSingleStudent")]
        public EnrollStudentResponse EnrollSingleStudent([FromBody] EnrollStudentRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClassAndSection(info.classes, request.ClassId, request.SectionId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
                return response;
            }
            EnrollSingleStudentModel model = new EnrollSingleStudentModel()
            {
                BatchId = info.BatchId,
                ClassId = request.ClassId,
                EnrolledDate = request.EnrolledDate,
                UserId = request.UserId,
                SectionId = request.SectionId,
                createdBy = info.UserID,
                updatedBy = info.UserID
            };
            response.student = _enrollmentService.EnrollStudent(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("enrollMultipleStudents")]
        public EnrollMultipleStudentResponse EnrollStudents([FromBody] EnrollMultipleStudentRequest request)
        {
            EnrollMultipleStudentResponse response = new EnrollMultipleStudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClassAndSection(info.classes, request.ClassId, request.SectionId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
                return response;
            }
            EnrolledMultipleStudentModel model = new EnrolledMultipleStudentModel()
            {
                BatchId = info.BatchId,
                ClassId = request.ClassId,
                EnrolledDate = request.EnrolledDate,
                SectionId = request.SectionId,
                UserId = request.UserId,
                createdBy = info.UserID,
                IsActive = true,
                updatedBy = info.UserID
            };

            response.student = _enrollmentService.EnrollStudent(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("addEnrollStudent")]
        public UserStudentResponse AddEnrollStudent([FromBody] AddEnrollStudentRequest request)
        {
            UserStudentResponse response = new UserStudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClassAndSection(info.classes, request.ClassId, request.SectionId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
                return response;
            }

            UserModel user = GetUserModel(request);
            if (user.UserId > 0)
            {
                EnrollSingleStudentModel model = new EnrollSingleStudentModel()
                {
                    BatchId = info.BatchId,
                    ClassId = request.ClassId,
                    EnrolledDate = request.EnrolledDate,
                    UserId = user.UserId,
                    SectionId = request.SectionId,
                    createdBy = info.UserID,
                    updatedBy = info.UserID
                };
                response.enrollStudent = _enrollmentService.EnrollStudent(model);
                response.user = user;
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
            }

            return response;
        }

        private UserModel GetUserModel(AddEnrollStudentRequest request)
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
                NameSpace = ConstantValues.LOOKUP_VALUE_NAMESPACE_STUDENT,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                PlaceOfBirth = request.PlaceOfBirth,
                UserName = request.UserName
            };
            user = userService.CreateUser(user);
            return user;
        }

        [HttpPost("assignSingleStudentToSection")]
        public EnrollStudentResponse AddSection([FromBody] AssignSectionRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessSection(info.classes, request.SectionId))
            {
                response.student = _enrollmentService.AssignSection(request.StudentId, request.SectionId, info.UserID);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }

        [HttpPost("assignMultipleStudentToSection")]
        public EnrollStudentListResponse AssignStudentToSection([FromBody] AssignMultipleSectionRequest request)
        {
            EnrollStudentListResponse response = new EnrollStudentListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessSection(info.classes, request.SectionId))
            {
                response.student = _enrollmentService.AssignSection(request.StudentId, request.SectionId, info.UserID);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }

        [HttpGet("getStudentsBySchool")]
        public StudentResponse GetStudentModel()
        {
            StudentResponse response = new StudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.students = _enrollmentService.GetStudents(info.CalanderYearId, info.SchoolId, 0, 0, 0, 0);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getStudentsByBranch")]
        public StudentResponse GetStudentModelByBranch()
        {
            StudentResponse response = new StudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.students = _enrollmentService.GetStudents(info.CalanderYearId, info.SchoolId, info.BranchId, 0, 0, 0);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("getStudentsByClass")]
        public StudentResponse GetStudentModel([FromBody] GetStudentsByClassRequest request)
        {
            StudentResponse response = new StudentResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClass(info.classes, request.classId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
                return response;
            }
            response.students = _enrollmentService.GetStudents(info.CalanderYearId, info.SchoolId, info.BranchId, request.classId, request.sectionId, 0);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

    }
}