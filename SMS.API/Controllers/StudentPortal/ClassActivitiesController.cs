 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.StudentPortal.Action_Filters;
using SMS.API.WEB.Controllers.StudentPortal.Request;
using SMS.API.WEB.Controllers.StudentPortal.Response;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.StudentPortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_STUDENT)]
    [Route(ConstantValues.STUDENT_LINK + "/[controller]")]
    [ServiceFilter(typeof(StudentAccessAPIFilters))]
    [ApiController]
    public class ClassActivitiesController : ControllerBase
    {
        private readonly IStudentSecurityService securityService;
        private readonly IMapper mapper;
        private readonly IEnrollmentService enrollmentService;
        private readonly ITimeTableService timeTableService;
        private readonly IAttendanceService attendanceService;
        private readonly IAssesmentService assesmentService;

        public ClassActivitiesController(IStudentSecurityService securityService, IMapper mapper, IEnrollmentService enrollmentService,
            ITimeTableService timeTableService, IAttendanceService attendanceService, IAssesmentService assesmentService)
        {
            this.securityService = securityService;
            this.mapper = mapper;
            this.enrollmentService = enrollmentService;
            this.timeTableService = timeTableService;
            this.attendanceService = attendanceService;
            this.assesmentService = assesmentService;
        }

        
        [HttpGet("getMySchedules")]
        public Student_GetScheduleListResponse GetSchedule()
        {
            Student_GetScheduleListResponse response = new Student_GetScheduleListResponse();
            var info = (StudentSecurityInfo)securityService.GetMySecurityInfo(User);
            if (info != null)
            {

                response.scheduleModelList = timeTableService.GetSchedulePerClass(info.BatchId, info.BranchId, 0, info.classId, info.sectionId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
            }
            return response;
        }
     
        [HttpPost("getMyAttendance")]
        public Student_AttendanceListResponse GetAttendance([FromBody] Student_GetAttendanceRequest request)
        {
            Student_AttendanceListResponse response = new Student_AttendanceListResponse();
            try
            {
                var info = (StudentSecurityInfo)securityService.GetMySecurityInfo(User);
                if (info != null)
                {
                    if(request.endDate == null || request.endDate == DateTime.MinValue)
                    {
                        request.endDate = DateTime.Now;
                    }
                    response.attendance = attendanceService.GetAttendances(info.classId, info.sectionId, request.subjectId, request.scheduleId, request.scheduleDetailId, request.startDate, request.endDate, info.StudentId);
                    CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                }
                else
                {
                    CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
                }
            }
            catch (SCMSException x)
            {
                response.responseCode = x.responseCode;
                response.responseMessage = x.responseMessage;
            }
            catch (Exception x)
            {
                response.responseCode = CommonMethods.GetErrorCode(CustomResponse.ERROR_RESPONSE_GENERIC);
                response.responseMessage = x.Message;
            }
            return response;
        }
       
        [HttpPost("getResults")]
        public Student_ResultListResponse GetResults([FromBody] Student_GetResultRequest request)
        {
            Student_ResultListResponse response = new Student_ResultListResponse();
            try
            {
                var info = (StudentSecurityInfo)securityService.GetMySecurityInfo(User);
                if (info != null)
                {
                    response.results = assesmentService.GetResult(info.classId, info.sectionId, request.subjectId, request.assesmentId, info.StudentId);
                    CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                }
                else
                {
                    CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
                }
            }
            catch (SCMSException x)
            {
                response.responseCode = x.responseCode;
                response.responseMessage = x.responseMessage;
            }
            catch (Exception x)
            {
                response.responseCode = CommonMethods.GetErrorCode(CustomResponse.ERROR_RESPONSE_GENERIC);
                response.responseMessage = x.Message;
            }
            return response;
        }
        
        [HttpGet("getClassInformation")]
        public ClassInformationResponse GetStudentModel()
        {
            ClassInformationResponse response = new ClassInformationResponse();
            try
            {
                var info = (StudentSecurityInfo)securityService.GetMySecurityInfo(User);
                if (info != null)
                {
                    response.students = enrollmentService.GetStudents(info.CalanderYearId, info.SchoolId, info.BranchId, info.classId, info.sectionId, info.StudentId);
                    CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                }
                else
                {
                    CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
                }
            }
            catch (SCMSException x)
            {
                response.responseCode = x.responseCode;
                response.responseMessage = x.responseMessage;
            }
            catch (Exception x)
            {
                response.responseCode = CommonMethods.GetErrorCode(CustomResponse.ERROR_RESPONSE_GENERIC);
                response.responseMessage = x.Message;
            }
            return response;
        }
        
        [HttpPost("getMyAssesments")]
        public Student_AssesmentResponse GetAssesments([FromBody] Student_GetAssesmentRequest request)
        {
            Student_AssesmentResponse response = new Student_AssesmentResponse();
            try
            {
                var info = (StudentSecurityInfo)securityService.GetMySecurityInfo(User);
                if (info != null)
                {
                    response.assesment = assesmentService.GetAssesment(info.BatchId, info.classId, info.sectionId, request.subjectId);
                    CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                }
                else
                {
                    CommonMethods.SetResponse(response, CustomResponse.ERROR_RESPONSE_GENERIC);
                }
            }
            catch (SCMSException x)
            {
                response.responseCode = x.responseCode;
                response.responseMessage = x.responseMessage;
            }
            catch (Exception x)
            {
                response.responseCode = CommonMethods.GetErrorCode(CustomResponse.ERROR_RESPONSE_GENERIC);
                response.responseMessage = x.Message;
            }
            return response;
        }
    }
}