using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment
{
    public interface ISchoolServiceInternal
    {
        #region School
        School AddSchool(SchoolModel school);
        List<School> GetAllSchool();
        School GetSchoolById(int id);
        School GetSchoolByName(String schoolName);
        School GetSchoolByEmail(String schoolEmail);
        School GetSchoolByWebsite(String schoolWebsite);
        School GetSchoolByTinNumber(String schoolTinNumber);
        School UpdateSchool(SchoolModel school);

        #endregion

        #region Branch
        Branch AddBranch(BranchModel branch);
        Branch UpdateBranch(BranchModel branch);
        Branch GetBranchById(int id);
        bool branchExist(int schoolId ,string branchName);
        bool IsMainbranchExist(int schoolId);
        List<Branch> GetAllBranchBySchoolId(int schoolId);
        List<Branch> GetAllActiveBranches();
        List<sp_GetBranchOnActiveCalendarYear_Result> GetBranchDetailByCalendarYear();
        
        #endregion

        #region Class
        Class AddClass(ClassModel classModel);
        Class UpdateClass(ClassModel classModel);
        Class GetClassById(int id);
        bool classExist(int branchId, string className);
        List<Class> GetAllClassByBranchId(int branchId);
        List<sp_GetActiveClassInformation_Result> GetActiveClassInformation(int schoolId, int batchId, int branchId, int classId, int sectionId, int subjectId);

        #endregion

        #region Section
        Section AddSection(SectionModel SectionModel);
        Section UpdateSection(SectionModel SectionModel);
        Section GetSectionById(int id);
        Section GetSectionByClassId(int classid);
        List<Section> GetAllSectionByClassId(int classId);
        bool sectionExist(int classId, string sectionName);

        #endregion


    }
}
