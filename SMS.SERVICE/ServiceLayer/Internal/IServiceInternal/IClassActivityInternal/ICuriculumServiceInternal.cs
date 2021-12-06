using SCMS.DataAccess;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IClassActivityInternal
{
    public interface ICuriculumServiceInternal
    {
        #region Subject
        Subject AddSubject(SubjectModel subjectModel);
        Subject UpdateSubject(SubjectModel subjectModel);
        Subject GetSubjectById(int id);
        List<Subject> GetAllSubjectByClassId(int classId);
        bool IsSubjectExist(int classId, string subjectName);

        #endregion

    }
}
