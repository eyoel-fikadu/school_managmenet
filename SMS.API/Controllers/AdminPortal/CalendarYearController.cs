using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.AdminPortal.Domain.Request;
using SMS.API.WEB.Controllers.AdminPortal.Domain.Response;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.SMSBasic;

namespace SMS.API.WEB.Controllers.AdminPortal
{
    [Authorize(Roles = ConstantValues.LOOKUP_VALUE_NAMESPACE_TDWEB_ADMIN)]
    [Route(ConstantValues.TD_WEB_ADMIN_LINK + "/[controller]")]
    [ApiController]
    public class CalendarYearController : ControllerBase
    {
        private IMainSystemService _mainSystemService;
        private IMapper _mapper;
        public IUserService userService;
        public JwtSettings jwt;

        public CalendarYearController(IMainSystemService mainSystemService, IUserService userService,
            IMapper mapper, JwtSettings jwt)
        {
            _mainSystemService = mainSystemService;
            this.userService = userService;
            _mapper = mapper;
            this.jwt = jwt;
        }

        #region calendar year

        [HttpPost("registerNewYear")]
        public CalanderYearResponse RegisterCalendarYear([FromBody] AddCalendarYearRequest request)
        {
            CalanderYearResponse response = new CalanderYearResponse();
            int userId = jwt.GetCurrentUserId(User);
            CalendarYearModel model = new CalendarYearModel()
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                yearDescription = request.yearDescription,
                createdBy = userId,
                updatedBy = userId
            };
            response.calendarYear = _mainSystemService.CreateCalendarYear(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("closeActiveCalendarYear")]
        public CalanderYearResponse CloseCalendarYear(CloseCalendarYearRequest request)
        {
            CalanderYearResponse response = new CalanderYearResponse();
            int userId = jwt.GetCurrentUserId(User);
            response.calendarYear = _mainSystemService.CloseActiveCalendarYear(request.EndDate, userId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getAllCalendarYear")]
        public CalanderYearListResponse GetAllCalendarYear()
        {
            CalanderYearListResponse response = new CalanderYearListResponse();
            response.calendarYears = _mainSystemService.GetAllCalendarYear();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getCalendarYearById/{calendarYearid:int}")]
        public CalanderYearResponse GetCalendarYearById(int calendarYearid)
        {
            CalanderYearResponse response = new CalanderYearResponse();
            response.calendarYear = _mainSystemService.GetCalendarYearById(calendarYearid);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getActiveCalanderYear")]
        public CalanderYearResponse GetActiveCalendarYear()
        {
            CalanderYearResponse response = new CalanderYearResponse();
            response.calendarYear = _mainSystemService.GetActiveCalendarYear();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion

        #region batch

        [HttpPost("registerBatch")]
        public BatchListResponse RegisterBatch([FromBody] AddBatchRequest request)
        {
            BatchListResponse response = new BatchListResponse();
            int userId = jwt.GetCurrentUserId(User);
            var cYear = _mainSystemService.GetActiveCalendarYear();
            if (cYear != null)
            {
                List<BatchModel> batchModeles = new List<BatchModel>();

                foreach (int branch in request.BranchId)
                {
                    batchModeles.Add(new BatchModel()
                    {
                        BranchId = branch,
                        createdBy = userId,
                        updatedBy = userId,
                        CalendarYearId = cYear.CalendarYearId,
                        IsActive = true
                    });
                }

                response.batches = _mainSystemService.CreateListOfBatches(batchModeles);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
                return response;
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.ACTIVE_CALENDAR_YEAR_DOESNT_EXIST);
                return response;
            }
        }

        [HttpPost("disableBatchByBranch/{branchId:int}")]
        public BatchResponse DisableBatchByBranch(int branchId)
        {
            BatchResponse response = new BatchResponse();
            int userId = jwt.GetCurrentUserId(User);
            response.batches = _mainSystemService.DisableBatch(branchId, userId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("disableBatchBySchool/{schoolId:int}")]
        public BatchListResponse DisableBatchBySchool(int schoolId)
        {
            BatchListResponse response = new BatchListResponse();
            int userId = jwt.GetCurrentUserId(User);
            response.batches = _mainSystemService.DisableBatchesBySchool(schoolId, userId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("enableBatchByBranch/{branchId:int}")]
        public BatchResponse ActivateBatchByBranch(int branchId)
        {
            BatchResponse response = new BatchResponse();
            int userId = jwt.GetCurrentUserId(User);
            response.batches = _mainSystemService.EnableBatch(branchId, userId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("enableBatchBySchool/{schoolId:int}")]
        public BatchListResponse ActivateBatchBySchool(int schoolId)
        {
            BatchListResponse response = new BatchListResponse();
            int userId = jwt.GetCurrentUserId(User);
            response.batches = _mainSystemService.EnableBatchesBySchool(schoolId, userId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getRegisteredBatches")]
        public RegisteredBatchResponse GetBatchList()
        {
            RegisteredBatchResponse response = new RegisteredBatchResponse();
            response.branches = _mainSystemService.GetBatchDetailByActiveCalendarYear();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getUnRegisteredBranches")]
        public Admin_BranchsListResponse GetUnregisterdBranches()
        {
            Admin_BranchsListResponse response = new Admin_BranchsListResponse();
            response.branches = _mainSystemService.GetUnRegisteredBranchesForCalenderYear();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getInActiveBatches")]
        public RegisteredBatchResponse GetInActiveBatchList()
        {
            RegisteredBatchResponse response = new RegisteredBatchResponse();
            response.branches = _mainSystemService.GetBatchDetailByActiveCalendarYear().Where(x => !x.isBatchActive).ToList();
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion
    }
}