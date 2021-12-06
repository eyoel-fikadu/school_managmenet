using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment
{
    public interface ISchoolService
    {
        #region School
        SchoolModel CreateSchool(SchoolModel school);
        List<SchoolModel> GetAllSchool();
        SchoolModel GetSchoolById(int id);
        SchoolModel UpdateSchool(SchoolModel school);
        bool SchoolExists(int schoolId);

        #endregion

        #region Branch
        BranchModel CreateBranch(BranchModel branch);
        BranchModel UpdateBranch(BranchModel branch);
        List<BranchModel> CreateListOfBranch(List<BranchModel> branches);
        BranchModel GetBranchById(int id);
        List<BranchModel> GetAllBranchBySchoolId(int schoolId);
        BranchDetailModel GetBranchDetailByCalendarYear(int batchId, int schoolId, int branchId);
        bool isMainBranch(int branchId);

        #endregion

        #region Class
        ClassModel CreateClass(ClassModel classModel);
        ClassModel UpdateClass(ClassModel classModel);
        List<ClassModel> CreateListOfClass(List<ClassModel> classModel);
        ClassModel GetClassById(int id);
        List<ClassModel> GetAllClassByBranchId(int branchId);
        List<int> GetAllClassIdByBranchId(int branchId);
        bool classHasSection(int classId);

        #endregion

        #region Section
        SectionModel CreateSection(SectionModel sectionModel);
        SectionModel UpdateSection(SectionModel sectionModel);
        List<SectionModel> CreateListOfSections(List<SectionModel> sectionModels);
        SectionModel GetSectionById(int id);
        List<SectionModel> GetAllSectionByClassId(int classId);
        List<int> GetAllSectionIdByClassId(int classId);

        #endregion


    }
}
