using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ConfigurationManagment
{
    public class SchoolService : ISchoolService
    {
        private readonly IAddressService addressService;
        private readonly ISchoolServiceInternal schoolServiceInternal;
        public SchoolService()
        {
            addressService = Singleton.GetAddressService();
            schoolServiceInternal = Singleton.GetSchoolServiceInternal();
        }

        #region School

        public SchoolModel CreateSchool(SchoolModel _schoolModel)
        {
            try
            {
                ValidateSchool(_schoolModel);
                using (SCMSEntities context = new SCMSEntities())
                {
                    var schoolInserted = schoolServiceInternal.AddSchool(_schoolModel);
                    context.Schools.Add(schoolInserted);
                    context.SaveChanges();

                    _schoolModel.SchoolId = schoolInserted.SchoolID;

                    BranchModel branch = new BranchModel()
                    {
                        BranchName = _schoolModel.SchoolName,
                        SchoolId = schoolInserted.SchoolID,
                        IsActive = true,
                        IsBranchMain = true
                    };

                    var branchInserted = schoolServiceInternal.AddBranch(branch);
                    context.Branches.Add(branchInserted);
                    context.SaveChanges();

                    var branchModel = GetDtoModels.GetBranchModel(branchInserted);

                    if (_schoolModel.HeadQaurterAddress != null)
                    {
                        branchModel.BranchAddress = addressService.CreateAddress(_schoolModel.HeadQaurterAddress, branchInserted.BranchId, ConstantValues.TABLE_BRANCH);

                    }
                    if (_schoolModel.HeadQuarterLocation != null)
                    {
                        branchModel.BranchLocation = addressService.CreateLocation(_schoolModel.HeadQuarterLocation, branchInserted.BranchId, ConstantValues.TABLE_BRANCH);
                    }
                    _schoolModel.Branches = new List<BranchModel>();
                    _schoolModel.Branches.Add(branchModel);
                    return _schoolModel;
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

        private void ValidateSchool(SchoolModel schoolModel)
        {
            List<SCMSException> errorMessages = new List<SCMSException>();

            School checkName = schoolServiceInternal.GetSchoolByName(schoolModel.SchoolName);
            if (checkName != null)
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.SCHOOL_NAME_EXIST));
            }

            School checkEmail = schoolServiceInternal.GetSchoolByEmail(schoolModel.Email);
            if (checkEmail != null)
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.SCHOOL_EMAIL_EXIST));
            }

            School checkTinnumber = schoolServiceInternal.GetSchoolByTinNumber(schoolModel.TinNumber);
            if (checkTinnumber != null)
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.SCHOOL_TINNUMBER_EXIST));
            }

            School checkWebsite = schoolServiceInternal.GetSchoolByWebsite(schoolModel.WebSite);
            if (checkWebsite != null)
            {
                errorMessages.Add(CommonMethods.GetException(CustomResponse.SCHOOL_WEBSITE_EXIST));
            }
            CommonMethods.ThrowError(errorMessages);
        }

        public List<SchoolModel> GetAllSchool()
        {
            List<School> schools = schoolServiceInternal.GetAllSchool();
            List<SchoolModel> schoolModels = new List<SchoolModel>();
            foreach (School school in schools)
            {
                SchoolModel schoolModel = GetDtoModels.GetSchoolModel(school);
                schoolModel.HeadQaurterAddress = addressService.GetAddress(school.SchoolID, ConstantValues.TABLE_SCHOOL);
                schoolModel.HeadQuarterLocation = addressService.GetLocation(school.SchoolID, ConstantValues.TABLE_SCHOOL);
                schoolModel.Branches = GetAllBranchBySchoolId(school.SchoolID);

                schoolModels.Add(schoolModel);
            }
            return schoolModels;
        }
        public SchoolModel GetSchoolById(int id)
        {
            School school = schoolServiceInternal.GetSchoolById(id);
            if (school != null)
            {
                SchoolModel schoolModel = GetDtoModels.GetSchoolModel(school);
                schoolModel.Branches = GetAllBranchBySchoolId(school.SchoolID);
                BranchModel mainBranch = schoolModel.Branches.Where(x => x.IsBranchMain).FirstOrDefault();
                if(mainBranch != null)
                {
                    schoolModel.HeadQaurterAddress = addressService.GetAddress(mainBranch.BranchId, ConstantValues.TABLE_BRANCH);
                    schoolModel.HeadQuarterLocation = addressService.GetLocation(mainBranch.BranchId, ConstantValues.TABLE_BRANCH);
                }
                return schoolModel;
            }
            return null;
        }
        public SchoolModel UpdateSchool(SchoolModel _schoolModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                School school = schoolServiceInternal.UpdateSchool(_schoolModel);
                context.Schools.AddOrUpdate(school);
                context.SaveChanges();
                if (_schoolModel.HeadQaurterAddress != null) addressService.UpdateAddress(_schoolModel.HeadQaurterAddress);
                if (_schoolModel.HeadQuarterLocation != null) addressService.UpdateLocation(_schoolModel.HeadQuarterLocation);

                return _schoolModel;
            }
        }

        #endregion

        #region Branch
        public BranchModel CreateBranch(BranchModel model)
        {
            if(schoolServiceInternal.branchExist(model.SchoolId, model.BranchName))
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_ALREADY_REGISTERED);
            }
            if(schoolServiceInternal.IsMainbranchExist(model.SchoolId) && model.IsBranchMain)
            {
                throw CommonMethods.GetException(CustomResponse.MAIN_BRANCH_ALREADY_EXISTS);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Branch branchInserted = schoolServiceInternal.AddBranch(model);
                branchInserted = context.Branches.Add(branchInserted);
                context.SaveChanges();
                AddBranchAddressLocation(model, branchInserted.BranchId);
                return model;
            }
        }
        private void AddBranchAddressLocation(BranchModel _branchModel, int branchId)
        {
            if (_branchModel.BranchAddress != null) addressService.CreateAddress(_branchModel.BranchAddress, branchId, ConstantValues.TABLE_BRANCH);
            if (_branchModel.BranchLocation != null) addressService.CreateLocation(_branchModel.BranchLocation, branchId, ConstantValues.TABLE_BRANCH);
        }
        public List<BranchModel> CreateListOfBranch(List<BranchModel> _brancheModels)
        {
            foreach (BranchModel model in _brancheModels)
            {
                if (schoolServiceInternal.branchExist(model.SchoolId, model.BranchName))
                {
                    throw CommonMethods.GetException(CustomResponse.BRANCH_ALREADY_REGISTERED);
                }
                if (schoolServiceInternal.IsMainbranchExist(model.SchoolId) && model.IsBranchMain)
                {
                    throw CommonMethods.GetException(CustomResponse.MAIN_BRANCH_ALREADY_EXISTS);
                }
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (BranchModel model in _brancheModels)
                {
                    Branch branchInserted = schoolServiceInternal.AddBranch(model);
                    branchInserted = context.Branches.Add(branchInserted);
                    context.SaveChanges();
                    AddBranchAddressLocation(model, branchInserted.BranchId);
                }
                return _brancheModels;
            }   
        }
        public BranchModel UpdateBranch(BranchModel _branchModel)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                Branch branch = schoolServiceInternal.UpdateBranch(_branchModel);
                context.Branches.AddOrUpdate(branch);
                context.SaveChanges();
                if (_branchModel.BranchAddress != null) addressService.UpdateAddress(_branchModel.BranchAddress);
                if (_branchModel.BranchLocation != null) addressService.UpdateLocation(_branchModel.BranchLocation);

                return _branchModel;
            }
            
        }
        public BranchModel GetBranchById(int id)
        {
            Branch branch = schoolServiceInternal.GetBranchById(id);
            if (branch == null) return null;
            BranchModel branchModel = GetDtoModels.GetBranchModel(branch);
            branchModel.BranchAddress = addressService.GetAddress(branch.BranchId, ConstantValues.TABLE_BRANCH);
            branchModel.BranchLocation = addressService.GetLocation(branch.BranchId, ConstantValues.TABLE_BRANCH);
            return branchModel;
        }
        public List<BranchModel> GetAllBranchBySchoolId(int schoolId)
        {
            List<Branch> branchs = schoolServiceInternal.GetAllBranchBySchoolId(schoolId);
            List<BranchModel> branchModels = new List<BranchModel>();
            foreach (Branch branch in branchs)
            {
                BranchModel branchModel = GetDtoModels.GetBranchModel(branch);
                branchModel.BranchAddress = addressService.GetAddress(branch.BranchId, ConstantValues.TABLE_BRANCH);
                branchModel.BranchLocation = addressService.GetLocation(branch.BranchId, ConstantValues.TABLE_BRANCH);
                branchModels.Add(branchModel);
            }
            return branchModels;
        }
        public BranchDetailModel GetBranchDetailByCalendarYear(int batchId, int schoolId, int branchId)
        {
            var branchDetail = schoolServiceInternal.GetActiveClassInformation(schoolId,batchId, branchId, 0, 0, 0);
            if (branchDetail == null || branchDetail.Count == 0) return null;
            return GetDtoModels.GetBranchDetailModel(branchDetail);
        }

        public bool isMainBranch(int branchId)
        {
            return schoolServiceInternal.GetBranchById(branchId).IsMainBranch;
        }
        #endregion

        #region Class
        public ClassModel CreateClass(ClassModel model)
        {
            if (schoolServiceInternal.classExist(model.BranchId, model.ClassName))
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_ALREADY_REGISTERED);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Class _class = schoolServiceInternal.AddClass(model);
                context.Classes.Add(_class);
                context.SaveChanges();
                model.ClassId = _class.ClassId;
                return model;
            }
            
        }
        public ClassModel UpdateClass(ClassModel classModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Class _class = schoolServiceInternal.UpdateClass(classModel);
                context.Classes.AddOrUpdate(_class);
                context.SaveChanges();
                return classModel;
            }

        }
        public List<ClassModel> CreateListOfClass(List<ClassModel> classModels)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (ClassModel model in classModels)
                {
                    if (schoolServiceInternal.classExist(model.BranchId, model.ClassName))
                    {
                        throw CommonMethods.GetException(CustomResponse.CLASS_ALREADY_REGISTERED);
                    }
                    Class _class = schoolServiceInternal.AddClass(model);
                    context.Classes.Add(_class);
                }
                context.SaveChanges();
                return classModels;
            }
        }
        public ClassModel GetClassById(int id)
        {
            Class @class = schoolServiceInternal.GetClassById(id);
            if (@class != null) return GetDtoModels.GetClassModel(@class);
            return null;
        }
        public List<ClassModel> GetAllClassByBranchId(int branchId)
        {
            List<Class> classes = schoolServiceInternal.GetAllClassByBranchId(branchId);
            List<ClassModel> classModels = new List<ClassModel>();
            foreach(Class @class in classes)
            {
                classModels.Add(GetDtoModels.GetClassModel(@class));
            }
            return classModels;
        }
        public List<int> GetAllClassIdByBranchId(int branchId)
        {
            return schoolServiceInternal.GetAllClassByBranchId(branchId).Select(x => x.ClassId).ToList();
        }
        public bool classHasSection(int classId)
        {
            Class @class = schoolServiceInternal.GetClassById(classId);
            if (@class != null)
                return @class.HasSection;
            return false;
        }
        #endregion

        #region Section
        public SectionModel CreateSection(SectionModel model)
        {
            if (!classHasSection(model.ClassId))
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_SECTION_LESS);
            }
            if (schoolServiceInternal.sectionExist(model.ClassId, model.SectionName))
            {
                throw CommonMethods.GetException(CustomResponse.SECTION_ALREADY_REGISTERED);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Section section = schoolServiceInternal.AddSection(model);
                context.Sections.Add(section);
                context.SaveChanges();
                model.SectionId = section.SectionId;
                return model;
            }
            
        }
        public SectionModel UpdateSection(SectionModel sectionModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Section section = schoolServiceInternal.UpdateSection(sectionModel);
                context.Sections.AddOrUpdate(section);
                context.SaveChanges();
                return sectionModel;
            }

        }

        public List<SectionModel> CreateListOfSections(List<SectionModel> sectionModels)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (SectionModel model in sectionModels)
                {
                    if (schoolServiceInternal.sectionExist(model.ClassId, model.SectionName))
                    {
                        throw CommonMethods.GetException(CustomResponse.SECTION_ALREADY_REGISTERED);
                    }
                    if (!classHasSection(model.ClassId))
                    {
                        throw CommonMethods.GetException(CustomResponse.CLASS_IS_SECTION_LESS);
                    }
                    Section section = schoolServiceInternal.AddSection(model);
                    context.Sections.Add(section);
                }
                context.SaveChanges();
            }
            return sectionModels;
        }
        public SectionModel GetSectionById(int id)
        {
            Section section = schoolServiceInternal.GetSectionById(id);
            if (section != null) return GetDtoModels.GetSectionModel(section);
            return null;
        }
        public List<SectionModel> GetAllSectionByClassId(int classId)
        {
            List<Section> sections = schoolServiceInternal.GetAllSectionByClassId(classId);
            List<SectionModel> sectionModels = new List<SectionModel>();
            foreach(Section section in sections)
            {
                sectionModels.Add(GetDtoModels.GetSectionModel(section));
            }
            return sectionModels;
        }

        public List<int> GetAllSectionIdByClassId(int classId)
        {
            return schoolServiceInternal.GetAllSectionByClassId(classId).Select(x => x.SectionId).ToList();
        }

        public bool SchoolExists(int schoolId)
        {
            var school = GetSchoolById(schoolId);
            if (school == null) return false;
            return true;
        }
        #endregion

    }
}
