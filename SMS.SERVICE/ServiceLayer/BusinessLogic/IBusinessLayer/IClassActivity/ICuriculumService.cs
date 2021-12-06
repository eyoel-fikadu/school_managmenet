using SCMS.DataAccess;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivityInternal
{
    public interface ICuriculumService
    {
        #region Subject
        SubjectModel CreateSubject(SubjectModel subjectModel);
        SubjectModel UpdateSubject(SubjectModel subjectModel);
        List<SubjectModel> CreateListOfSubject(List<SubjectModel> subjectModels);
        SubjectModel GetSubjectById(int id);
        List<SubjectModel> GetAllSubjectByClassId(int schoolId);
        #endregion
    }
}
