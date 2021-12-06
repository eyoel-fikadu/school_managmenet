using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Request;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Response;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
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
    public class LessonController : ControllerBase
    {
        private ITimeTableService _timeTableService;
        private ILookupService _lookupService;
        private IAssesmentService _assesmentService;
        private IMapper _mapper;
        private IItAdminSecurityService securityService;

        public LessonController(IAssesmentService assesmentService, ILookupService lookupService,
            ITimeTableService tableService, IMapper mapper, IItAdminSecurityService securityService)
        {
            _timeTableService = tableService;
            _lookupService = lookupService;
            _assesmentService = assesmentService;
            _mapper = mapper;
            this.securityService = securityService;
        }

        #region Time table

        [HttpPost("addTimeTable")]
        public TimeTableResponse AddTimeTable([FromBody] AddTimeTableRequest request)
        {
            TimeTableResponse response = new TimeTableResponse();
            var securityInfo = (EmployeeSecurityInfo)securityService.GetMySecurityInfo(User);
            TimeTableModel tableModel = new TimeTableModel()
            {
                BatchId = securityInfo.BatchId,
                BranchId = securityInfo.BranchId,
                DayOfTheWeek = request.DayOfTheWeek,
                Description = request.Description,
                EndTime = request.EndTime,
                IsActive = true,
                StartTime = request.StartTime,
                createdBy = securityInfo.UserID,
                updatedBy = securityInfo.UserID
            };

            response.timeTable = _timeTableService.AddTimeTable(tableModel);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getTimeTable")]
        public TimeTableListResponse GetActiveTimeTableByBranch()
        {
            TimeTableListResponse response = new TimeTableListResponse();
            var securityInfo = (EmployeeSecurityInfo)securityService.GetMySecurityInfo(User);
            response.timeTableList = _timeTableService.GetAllTimeTablePerBranch(securityInfo.BatchId, securityInfo.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("getTimeTablePerDay")]
        public TimeTableListResponse GetActiveTimeTableByBranchPerDay([FromBody] GetActiveTimeTableByBranchRequest request)
        {
            TimeTableListResponse response = new TimeTableListResponse();
            var securityInfo = (EmployeeSecurityInfo)securityService.GetMySecurityInfo(User);
            response.timeTableList = _timeTableService.GetAllTimeTablePerDayForBranch(securityInfo.BatchId, securityInfo.BranchId, request.DayOfTheWeek);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
       
        [AllowAnonymous]
        [HttpGet("getDayOfTheWeeks")]
        public StringListResponse GetDayOfTheWeeks()
        {
            StringListResponse response = new StringListResponse();
            List<String> dayOfWeeks = _lookupService.GetDayOfTheWeeks();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            response.lookups = dayOfWeeks;
            return response;
        }
        #endregion

        #region Schedule

        [HttpPost("addSchedule")]
        public AddScheduleResponse AddSchedule([FromBody] AddScheduleRequest request)
        {
            AddScheduleResponse response = new AddScheduleResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClassDataWithSubject
                    (info.classes, request.ClassId, request.SectionId, request.SubjectId) &&
                        securityService.isTimeTableAccessableByBranch(info.BatchId, info.BranchId, request.TimeTableId))
            {
                ScheduleModel model = new ScheduleModel()
                {
                    ClassId = request.ClassId,
                    SectionId = request.SectionId,
                    TimeTableId = request.TimeTableId,
                    SubjectId = request.SubjectId,
                    createdBy = info.UserID,
                    updatedBy = info.UserID
                };
                response.scheduleModel = _timeTableService.AddSchedule(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
            }
            return response;
        }

        [HttpPost("getSchedulePerClass")]
        public GetScheduleListResponse GetSchdule([FromBody] GetSchedulesRequest request)
        {
            GetScheduleListResponse response = new GetScheduleListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (!securityService.CanEmployeeAccessClass(info.classes, request.classId))
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_RESOURCE);
                return response;
            }
            response.scheduleModelList = _timeTableService.GetSchedulePerClass(info.BatchId, info.BranchId, 0, request.classId, request.sectionId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
      
        [HttpGet("getSchedulePerBranch")]
        public GetScheduleListResponse GetSchdule()
        {
            GetScheduleListResponse response = new GetScheduleListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.scheduleModelList = _timeTableService.GetSchedulePerBranch(info.BatchId, info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion
    }
}