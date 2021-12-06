using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.TeacherPortal.Action_Filters;
using SMS.API.WEB.Controllers.TeacherPortal.Request;
using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.ServiceLayer.Security.SecurityService;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.TeacherPortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)]
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER)]
    [Route(ConstantValues.TEACHER_LINK + "/[controller]")]
    [ServiceFilter(typeof(TeacherAccessAPIFilters))]
    [ApiController]
    public class StudentActivitiesController : ControllerBase
    {
        private IAttendanceService _attendanceService;
        private IEnrollmentService _enrollmentService;
        private IAssesmentService assesmentService;
        private IMapper _mapper;
        private ITeacherSecurityService securityService;
        private ISchoolService schoolService;
        private ITimeTableService timeTableService;

        public StudentActivitiesController(IAssesmentService assesmentService, IEnrollmentService enrollmentService,
            ITeacherSecurityService securityService, IAttendanceService attendanceService, ISchoolService schoolService, 
            ITimeTableService timeTableService, IMapper mapper)
        {
            _attendanceService = attendanceService;
            this.schoolService = schoolService;
            this.timeTableService = timeTableService;
            _enrollmentService = enrollmentService;
            this.securityService = securityService;
            this.assesmentService = assesmentService;
            _mapper = mapper;
        }

        #region  My Students
        [HttpPost("getStudentsModel")]
        public Teacher_StudentResponse GetStudentModel([FromBody] GetStudentListRequest request)
        {
            Teacher_StudentResponse response = new Teacher_StudentResponse();
            TeacherSecurityInfo info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassData(info.classInfos, request.classId, request.sectionId))
            {
                response.students = _enrollmentService.GetStudents(info.CalanderYearId, info.SchoolId, info.BranchId, request.classId, request.sectionId, 0);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }
        #endregion

        #region Schedule Detail 
        
        [HttpPost("addScheduleDetail")]
        public Teacher_ScheduleDetailResponse Teacher_AddScheduleDetail([FromBody] Teacher_AddScheduleDetailRequest request)
        {
            Teacher_ScheduleDetailResponse response = new Teacher_ScheduleDetailResponse();

            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessSchedule(info.BatchId, info.BranchId, request.ScheduleId, info.EmployeeId))
            {
                ScheduleDetailModel model = new ScheduleDetailModel()
                {
                    ScheduleId = request.ScheduleId,
                    createdBy = info.UserID,
                    Date = request.Date,
                    EndTime = request.EndTime,
                    StartTime = request.StartTime,
                    TeacherId = info.EmployeeId,
                    updatedBy = info.UserID
                };
                response.scheduleDetail = timeTableService.AddScheduleDetail(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                return response;
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHEDULE_NOT_ACCESSABLE_BY_TEACHER);
            }
            return response;
        }

        [HttpGet("getScheduleDetailBySchedule/{scheduleId:int}")]
        public Teacher_ScheduleDetailListResponse Teacher_GetScheduleDetailBySchedule(int scheduleId)
        {
            Teacher_ScheduleDetailListResponse response = new Teacher_ScheduleDetailListResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessSchedule(info.BatchId, info.BranchId, scheduleId, info.EmployeeId))
            {
                response.scheduleDetail = timeTableService.GetScheduleDetails(scheduleId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHEDULE_NOT_ACCESSABLE_BY_TEACHER);
            }
            return response;
        }
        #endregion

        #region Attendance

        [HttpPost("addAttendanceList")]
        public AttendanceResponse AddAttendance([FromBody] AttendanceListModel request)
        {
            AttendanceResponse response = new AttendanceResponse();
            if (request.attendanceList.Select(x => x.ScheduleDetailId).Distinct().Count() != 1)
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHEDULE_DETAIL_SHOULD_BE_IDENTICAL);
                return response;
            }
            List<int> studentIds = request.attendanceList.Select(x => x.StudentId).Distinct().ToList();
            if (studentIds.Count < request.attendanceList.Count())
            {
                CommonMethods.SetResponse(response, CustomResponse.STUDENT_ID_SHOULD_BE_IDENTICAL);
                return response;
            }
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            ScheduleModel schedule = timeTableService.GetScheduleById(request.attendanceList.FirstOrDefault().ScheduleDetailId);
            if (schedule == null)
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHEDULE_DOESNT_EXIST);
                return response;
            }

            if (!securityService.CanTeacherAccessSchedule(info.BatchId, info.BranchId, schedule.ScheduleId, info.EmployeeId))
            {
                CommonMethods.SetResponse(response, CustomResponse.SCHEDULE_NOT_ACCESSABLE_BY_TEACHER);
                return response;
            }

            if (securityService.AreStudentsPartOfClassSection(studentIds, info.CalanderYearId, info.SchoolId, info.BranchId, schedule.ClassId, schedule.SectionId))
            {
                response.attendance = _attendanceService.AddAttendance(request);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.STUDENT_IS_NOT_PART_OF_A_CLASS);
            }
            return response;
        }

        [HttpPost("getAttendance")]
        public AttendanceListResponse GetAttendance([FromBody] GetAttendanceRequest request)
        {
            AttendanceListResponse response = new AttendanceListResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassData(info.classInfos, request.classId, request.sectionId))
            {
                response.attendance = _attendanceService.GetAttendances(request.classId, request.sectionId, request.subjectId, request.scheduleId, request.scheduleDetailId, DateTime.MinValue, DateTime.MaxValue, 0);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }

        #endregion

        #region Results

        [HttpPost("addResultList")]
        public ResultResponse AddResult([FromBody] ResultModelList request)
        {
            ResultResponse response = new ResultResponse();
            if (request.results.Select(x => x.AssesmentId).Distinct().Count() != 1)
            {
                CommonMethods.SetResponse(response, CustomResponse.ASSESMENT_SHOULD_BE_IDENTICAL);
                return response;
            }
            List<int> studentIds = request.results.Select(x => x.StudentId).Distinct().ToList();
            if (studentIds.Count < request.results.Count())
            {
                CommonMethods.SetResponse(response, CustomResponse.STUDENT_ID_SHOULD_BE_IDENTICAL);
                return response;
            }
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);

            AssesmentModel assesment = assesmentService.GetAssesmentById(request.results.FirstOrDefault().AssesmentId);
            if (assesment == null)
            {
                CommonMethods.SetResponse(response, CustomResponse.ASSESMENT_DOESNT_EXIST);
                return response;
            }
            if (!securityService.CanTeacherAccessClassDataWithSubject(info.classInfos, assesment.ClassId, assesment.SectionId, assesment.SubjectId))
            {
                CommonMethods.SetResponse(response, CustomResponse.TEACHER_CANNOT_ACCESS_RESOURCE);
                return response;
            }
            if (securityService.AreStudentsPartOfClassSection(studentIds, info.CalanderYearId, info.SchoolId, info.BranchId, assesment.ClassId, assesment.SectionId))
            {
                response.result = assesmentService.AddResults(request);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.STUDENT_IS_NOT_PART_OF_A_CLASS);
            }
            return response;
        }

        [HttpPost("getResults")]
        public ResultListResponse GetResults([FromBody] GetResultRequest request)
        {
            ResultListResponse response = new ResultListResponse();
            var info = (TeacherSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanTeacherAccessClassDataWithSubject(info.classInfos, request.classId, request.sectionId, request.subjectId))
            {
                response.results = assesmentService.GetResult(request.classId, request.sectionId, request.subjectId, request.assesmentId, 0);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }

        #endregion
    }
}