using SCMS.DataAccess;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.DTO.AdmissionDTO;

namespace SMS.SERVICE.Services.ConfigurationManagment
{
    public class SchoolServiceInternal : ISchoolServiceInternal
    {
        #region School
        public School AddSchool(SchoolModel model)
        {
            School _schoolObject = new School
            {
                SchoolName = model.SchoolName,
                DisplayName = model.DisplayName,
                Description = model.SchoolDescription,
                TinNumber = model.TinNumber,
                PublicId = model.PublicID,
                Website = model.WebSite,
                Email = model.Email,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };

            return _schoolObject;
        }
        public List<School> GetAllSchool()
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.ToList();
            }    
        }
        public School GetSchoolById(int id)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.Find(id);
            }
        }
        public School UpdateSchool(SchoolModel model)
        {
            School _schoolObject = new School
            {
                SchoolID = model.SchoolId,
                SchoolName = model.SchoolName,
                DisplayName = model.DisplayName,
                Description = model.SchoolDescription,
                PublicId = model.PublicID,
                TinNumber = model.TinNumber,
                Website = model.WebSite,
                Email = model.Email,
                IsActive = model.IsActive,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };

            return _schoolObject;
        }
        public School GetSchoolByName(string schoolName)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.Where(x => x.SchoolName == schoolName).FirstOrDefault();
            }
        }
        public School GetSchoolByEmail(string schoolEmail)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.Where(x => x.Email == schoolEmail).FirstOrDefault();
            }
        }
        public School GetSchoolByWebsite(string schoolWebsite)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.Where(x => x.Website == schoolWebsite).FirstOrDefault();
            }
        }
        public School GetSchoolByTinNumber(string schoolTinNumber)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schools.Where(x => x.TinNumber == schoolTinNumber).FirstOrDefault();
            }
        }

        #endregion

        #region Branch
        public Branch AddBranch(BranchModel model)
        {
            Branch _branchObject = new Branch
            {
                BranchName = model.BranchName,
                SchoolId = model.SchoolId,
                IsActive = true,
                IsMainBranch = model.IsBranchMain,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };
            return _branchObject;
        }
        public List<Branch> GetAllBranchBySchoolId(int schoolId)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.Branches.Where(x => x.SchoolId == schoolId).ToList();
        }
        public Branch GetBranchById(int id)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.Branches.Find(id);
        }
        public Branch UpdateBranch(BranchModel model)
        {
            Branch _branchObject = new Branch
            {
                BranchId = model.BranchId,
                BranchName = model.BranchName,
                SchoolId = model.SchoolId,
                IsActive = model.IsActive,
                IsMainBranch = model.IsBranchMain,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };
            return _branchObject;
        }
        public List<sp_GetBranchOnActiveCalendarYear_Result> GetBranchDetailByCalendarYear()
        {
            using(SCMSEntities context = new SCMSEntities())        
                return context.sp_GetBranchOnActiveCalendarYear().ToList();
        }
        public List<Branch> GetAllActiveBranches()
        {
            using(var context = new SCMSEntities())
            {
                return context.Branches.Where(x => x.IsActive).ToList();
            }
        }
        public bool IsMainbranchExist(int schoolId)
        {
            using (var context = new SCMSEntities())
            {
                var branch = context.Branches.FirstOrDefault(x => x.SchoolId == schoolId && x.IsMainBranch);
                if (branch == null) return false;
                return true;
            }
        }
        public bool branchExist(int schoolId, string branchName)
        {
            using (var context = new SCMSEntities())
            {
                var branch = context.Branches.FirstOrDefault(x => x.SchoolId == schoolId && x.BranchName == branchName);
                if (branch == null) return false;
                return true;
            }
        }
        #endregion

        #region Class
        public Class AddClass(ClassModel model)
        {
            Class _class = new Class()
            {
               IsActive = true,
               ClassName = model.ClassName,
               ClassTypeId = model.ClassTypeId,
               BranchId = model.BranchId,
               CreatedDate = DateTime.Now,
               UpdatedDate = DateTime.Now,
               UpdatedBy = model.updatedBy,
               CreatedBy = model.createdBy,
               HasSection = model.HasSection
            };
            return _class;
        }
        public Class UpdateClass(ClassModel model)
        {
            Class _class = new Class()
            {
                ClassId = model.ClassId,
                IsActive = model.IsActive,
                ClassName = model.ClassName,
                ClassTypeId = model.ClassTypeId,
                BranchId = model.BranchId,
                UpdatedBy = model.updatedBy,
                UpdatedDate = DateTime.Now
            };
            return _class;
        }
        public bool classExist(int branchId, string className)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Classes.FirstOrDefault( x => x.BranchId == branchId && x.ClassName == className) != null;
            }
        }
        public Class GetClassById(int id)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.Classes.Find(id);
        }
        public List<Class> GetAllClassByBranchId(int branchId)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.Classes.Where(x => x.BranchId == branchId).ToList();
        }
        public List<sp_GetActiveClassInformation_Result> GetActiveClassInformation(int schoolId, int batchId, int branchId, int classId, int sectionId, int subjectId)
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.sp_GetActiveClassInformation(batchId, schoolId, branchId, classId, sectionId, subjectId).ToList();
        }
        #endregion

        #region Section
        public Section AddSection(SectionModel model)
        {
            Section section = new Section()
            {
                IsActive = true,
                AssignedRoom = model.AssignedRoom,
                ClassId = model.ClassId,
                SectionName = model.SectionName,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedUser = model.createdBy,
                UpdatedUser = model.updatedBy
            };
            return section;
        }
        public Section UpdateSection(SectionModel model)
        {
            Section section = new Section()
            {
                SectionId = model.SectionId,
                IsActive = model.IsActive,
                AssignedRoom = model.AssignedRoom,
                ClassId = model.ClassId,
                SectionName = model.SectionName,
                UpdatedDate = DateTime.Now,
                UpdatedUser = model.updatedBy
            };
            return section;
        }
        public Section GetSectionById(int id)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.Sections.Find(id);
        }
        public List<Section> GetAllSectionByClassId(int classId)
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.Sections.Where(x => x.ClassId == classId).ToList();
        }
        public Section GetSectionByClassId(int classid)
        {
            throw new NotImplementedException();
        }
        public bool sectionExist(int classId, string sectionName)
        {
            using (var context = new SCMSEntities())
            {
                var section = context.Sections.FirstOrDefault(x => x.ClassId == classId && x.SectionName == sectionName);
                if (section == null) return false;
                return true;
            }
        }
        #endregion
    }
}
