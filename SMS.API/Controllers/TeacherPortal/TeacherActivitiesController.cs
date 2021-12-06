using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.TeacherPortal.Action_Filters;
using SMS.API.WEB.Controllers.TeacherPortal.Request;
using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.ServiceLayer.Security.SecurityService;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.TeacherPortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)]
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER)]
    [Route(ConstantValues.TEACHER_LINK + "/[controller]")]
    [ServiceFilter(typeof(TeacherAccessAPIFilters))]
    [ApiController]
    public class TeacherActivitiesController : ControllerBase
    {

        private ITimeTableService timeTableService;
        private IAssesmentService _assesmentService;
        private ILookupService _lookupService;
        private IMapper _mapper;
        private ITeacherSecurityService securityService;
        private IEnrollmentService enrollmentService;

        public TeacherActivitiesController(IAssesmentService assesmentService, ILookupService lookupService,
            ITimeTableService tableService, IMapper mapper, ITeacherSecurityService securityService, 
            IEnrollmentService enrollmentService)
        {
            timeTableService = tableService;
            _lookupService = lookupService;
            _assesmentService = assesmentService;
            _mapper = mapper;
            this.securityService = securityService;
            this.enrollmentService = enrollmentService;
        }

        #region Informations
        [HttpGet("getMyClassInformation")]
        public TeacherDetailResponse GetClassInformationByBranchId()
        {
            TeacherDetailResponse response = new TeacherDetailResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            response.teacher = enrollmentService.GetAssignedTeacherByEmployee(info.BatchId, info.SchoolId, info.BranchId, info.EmployeeId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
   
        [HttpGet("getMySchedules")]
        public Teacher_GetScheduleListResponse GetSchedule()
        {
            Teacher_GetScheduleListResponse response = new Teacher_GetScheduleListResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            response.scheduleModelList = timeTableService.GetScheduleByEmployee(info.BatchId, info.BranchId, info.EmployeeId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
        #endregion
       
        #region Exam

        [HttpPost("addNewExam")]
        public Teacher_ExamResponse AddExam([FromBody] Teacher_AddExamRequest request)
        {
            Teacher_ExamResponse response = new Teacher_ExamResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassDataWithSubject(info.classInfos, request.ClassId, request.SectionId, request.SubjectId))
            {
                ExamModel model = new ExamModel()
                {
                    BatchId = info.BatchId,
                    createdBy = info.UserID,
                    ClassId = request.ClassId,
                    Date = request.Date,
                    EndTime = request.EndTime,
                    ExamTypeId = request.ExamType,
                    OutOf = request.OutOf,
                    SectionId = request.SectionId,
                    StartTime = request.StartTime,
                    SubjectId = request.SubjectId,
                    updatedBy = info.UserID
                };
                response.exam = _assesmentService.AddExam(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.TEACHER_CANNOT_ACCESS_RESOURCE);
            }
            return response;
        }

        [AllowAnonymous]
        [HttpGet("getExamType")]
        public StringListResponse GetExamType()
        {
            StringListResponse response = new StringListResponse();
            response.lookups = _lookupService.GetExamTypes();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion

        #region Assignment

        [HttpPost("addNewAssignment")]
        public Teacher_AssignmentResponse AddAssignment([FromBody] Teacher_AddAssignmentRequest request)
        {
            Teacher_AssignmentResponse response = new Teacher_AssignmentResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassDataWithSubject(info.classInfos, request.ClassId, request.SectionId, request.SubjectId))
            {
                AssignmentModel model = new AssignmentModel()
                {
                    BatchId = info.BatchId,
                    createdBy = info.UserID,
                    ClassId = request.ClassId,
                    OutOf = request.OutOf,
                    SectionId = request.SectionId,
                    SubjectId = request.SubjectId,
                    updatedBy = info.UserID,
                    AssignedDate = request.AssignedDate,
                    AssignmentTypeId = request.AssignmentType,
                    SubmissionDate = request.SubmissionDate,
                    SubmissionTime = request.SubmissionTime
                };
                response.assignment = _assesmentService.AddAssignment(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.TEACHER_CANNOT_ACCESS_RESOURCE);
            }
            return response;
        }

        [AllowAnonymous]
        [HttpGet("getAssignmentType")]
        public StringListResponse GetAssignmentType()
        {
            StringListResponse response = new StringListResponse();
            response.lookups = _lookupService.GetAssignmentTypes();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("getAssesments")]
        public AssesmentResponse GetAssesments([FromBody] GetAssesmentRequest request)
        {
            AssesmentResponse response = new AssesmentResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassDataWithSubject(info.classInfos, request.classId, request.sectionId, request.subjectId))
            {
                response.assesment = _assesmentService.GetAssesment(info.BatchId, request.classId, request.sectionId, request.subjectId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.TEACHER_CANNOT_ACCESS_RESOURCE);
            }
            return response;
        }
        #endregion
    }
}