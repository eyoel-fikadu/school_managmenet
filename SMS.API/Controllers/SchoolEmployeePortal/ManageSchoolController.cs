using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Request;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Response;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivityInternal;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
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
    public class ManageSchoolController : ControllerBase
    {
        private ISchoolService _schoolService;
        private ICuriculumService _curiculumService;
        private IMapper _mapper;
        private IItAdminSecurityService securityService;

        public ManageSchoolController(ISchoolService schoolService, ICuriculumService curiculumService, 
            IMapper mapper, IItAdminSecurityService securityService)
        {
            this._schoolService = schoolService;
            _curiculumService = curiculumService;
            this._mapper = mapper;
            this.securityService = securityService;
        }

        #region Branch

        [HttpPost("registerBranch")]
        public BranchListResponse RegisterBranch([FromBody] AddBranchListRequest request)
        {
            BranchListResponse response = new BranchListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (_schoolService.isMainBranch(info.BranchId))
            {
                List<BranchModel> branches = new List<BranchModel>();
                foreach (var branch in request.branches)
                {
                    branches.Add(new BranchModel()
                    {
                        BranchAddress = branch.BranchAddress,
                        BranchLocation = branch.BranchLocation,
                        IsBranchMain = false,
                        BranchName = branch.BranchName,
                        SchoolId = info.SchoolId,
                        createdBy = info.UserID,
                        updatedBy = info.UserID,
                    });
                }
                response.branches = _schoolService.CreateListOfBranch(branches);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.BRANCH_SHOULD_BE_MAIN_BRANCH);
            }
            return response;
        }

        [HttpGet("getMyBranchDetail")]
        public BranchResponse GetBranchById()
        {
            BranchResponse response = new BranchResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.branch = _schoolService.GetBranchById(info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getBranchBySchool")]
        public BranchListResponse GetBranchBySchoolId()
        {
            BranchListResponse response = new BranchListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.branches = _schoolService.GetAllBranchBySchoolId(info.SchoolId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        #endregion

        #region class

        [HttpPost("addClass")]
        public ClassResponse AddClass([FromBody] AddClassRequest request)
        {
            ClassResponse response = new ClassResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            ClassModel model = new ClassModel()
            {
                BranchId = info.BranchId,
                ClassName = request.ClassName,
                createdBy = info.UserID,
                updatedBy = info.UserID,
                HasSection = request.HasSection
            };
            response.Class = _schoolService.CreateClass(model);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpPost("addListOfClass")]
        public ClassListResponse AddClassList([FromBody] AddClassListRequest classModels)
        {
            ClassListResponse response = new ClassListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            List<ClassModel> classes = new List<ClassModel>();
            foreach (var cls in classModels.classes)
            {
                classes.Add(new ClassModel()
                {
                    BranchId = info.BranchId,
                    ClassName = cls.ClassName,
                    HasSection = cls.HasSection,
                    createdBy = info.UserID,
                    updatedBy = info.UserID
                });
            }

            response.classes = _schoolService.CreateListOfClass(classes);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getClassById/{classId:int}")]
        public ClassResponse GetClassById(int classId)
        {
            ClassResponse response = new ClassResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, classId))
            {
                response.Class = _schoolService.GetClassById(classId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpGet("getClassByBranch")]
        public ClassListResponse GetClassByBranchId()
        {
            ClassListResponse response = new ClassListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.classes = _schoolService.GetAllClassByBranchId(info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }

        [HttpGet("getClassInformationByBranch")]
        public BranchDetailResponse GetClassInformationByBranchId()
        {
            BranchDetailResponse response = new BranchDetailResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            response.branch = _schoolService.GetBranchDetailByCalendarYear(info.BatchId, info.SchoolId, info.BranchId);
            CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            return response;
        }
        #endregion

        #region Section

        [HttpPost("addSection")]
        public SectionResponse AddSection([FromBody] AddSectionRequest request)
        {
            SectionResponse response = new SectionResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, request.ClassId))
            {
                SectionModel model = new SectionModel()
                {
                    ClassId = request.ClassId,
                    createdBy = info.UserID,
                    SectionName = request.SectionName,
                    updatedBy = info.UserID
                };
                response.section = _schoolService.CreateSection(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpPost("addListOfSectionPerClass")]
        public SectionListResponse AddSectionList([FromBody] AddSectionListRequest request)
        {
            SectionListResponse response = new SectionListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, request.ClassId))
            {
                List<SectionModel> sections = new List<SectionModel>();
                foreach (String str in request.SectionName)
                {
                    sections.Add(new SectionModel()
                    {
                        ClassId = request.ClassId,
                        createdBy = info.UserID,
                        SectionName = str,
                        updatedBy = info.UserID
                    });
                }
                response.sections = _schoolService.CreateListOfSections(sections);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpGet("getSectionById/{sectionId:int}")]
        public SectionResponse GetSectionById(int sectionId)
        {
            SectionResponse response = new SectionResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessSection(info.classes, sectionId))
            {
                response.section = _schoolService.GetSectionById(sectionId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpGet("getSectionByClassId/{classId:int}")]
        public SectionListResponse GetSectionByClassId(int classId)
        {
            SectionListResponse response = new SectionListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, classId))
            {
                response.sections = _schoolService.GetAllSectionByClassId(classId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }
        #endregion

        #region Subject

        [HttpPost("addSubject")]
        public SubjectResponse AddSubject([FromBody] AddSubjectRequest request)
        {
            SubjectResponse response = new SubjectResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, request.ClassID))
            {
                SubjectModel model = new SubjectModel()
                {
                    ClassID = request.ClassID,
                    createdBy = info.UserID,
                    SubjectName = request.SubjectName,
                    updatedBy = info.UserID
                };
                response.subject = _curiculumService.CreateSubject(model);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpPost("addListOfSubject")]
        public SubjectListResponse AddSubjectList([FromBody] AddSubjectListRequest request)
        {
            SubjectListResponse response = new SubjectListResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, request.ClassID))
            {
                List<SubjectModel> models = new List<SubjectModel>();
                foreach (String str in request.SubjectName)
                {
                    models.Add(new SubjectModel()
                    {
                        ClassID = request.ClassID,
                        createdBy = info.UserID,
                        SubjectName = str,
                        updatedBy = info.UserID
                    });
                }
                response.subjects = _curiculumService.CreateListOfSubject(models);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
        }

        [HttpGet("getSubjectById/{subjectId:int}")]
        public SubjectResponse GetSubjectById(int subjectId)
        {
            SubjectResponse response = new SubjectResponse();
            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessSubject(info.classes, subjectId))
            {
                response.subject = _curiculumService.GetSubjectById(subjectId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.SUBJECT_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return response;
        }

        [HttpGet("getSubjectByClassId/{classId:int}")]
        public SubjectListResponse GetSubjectByClassId(int classId)
        {
            SubjectListResponse response = new SubjectListResponse();

            var info = (ItAdminSecurityInfo)securityService.GetMySecurityInfo(User);
            if (securityService.CanEmployeeAccessClass(info.classes, classId))
            {
                response.subjects = _curiculumService.GetAllSubjectByClassId(classId);
                CommonMethods.SetResponse(response, CustomResponse.SUCCESS_RESPONSE);
            }
            else
            {
                CommonMethods.SetResponse(response, CustomResponse.UNABLE_TO_ACCESS_CLASS_INFORMATION);
            }
            return response;
            
        }
        #endregion
    }
}