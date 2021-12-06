using javax.security.auth;
using SCMS.DataAccess;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IClassActivityInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subject = SCMS.DataAccess.Subject;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ClassActivityInternal
{
    public class CuriculumServiceInternal : ICuriculumServiceInternal
    {
        #region Subject
        public Subject AddSubject(SubjectModel model)
        {
            Subject subject = new Subject()
            {
                ClassId = model.ClassID,
                IsActive = true,
                SubjectName = model.SubjectName,
                SubjectTypeId = model.SubjectTypeId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };
            return subject;
        }

        public SCMS.DataAccess.Subject UpdateSubject(SubjectModel subjectModel)
        {
            Subject subject = new Subject()
            {
                SubjectId = subjectModel.SubjectID,
                ClassId = subjectModel.ClassID,
                IsActive = subjectModel.IsActive,
                SubjectName = subjectModel.SubjectName,
                SubjectTypeId = subjectModel.SubjectTypeId,
                UpdatedDate = DateTime.Now
            };
            return subject;
        }

        public Subject GetSubjectById(int id)
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.Subjects.Find(id);
        }

        public List<Subject> GetAllSubjectByClassId(int classId)
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.Subjects.Where(x => x.ClassId == classId).ToList();
        }

        public bool IsSubjectExist(int classId, string subjectName)
        {
            using(var context =  new SCMSEntities())
            {
                var subject = context.Subjects.FirstOrDefault(x => x.ClassId == classId && x.SubjectName == subjectName);
                if (subject == null) return false;
                return true;
            }
        }

        #endregion
    }
}
