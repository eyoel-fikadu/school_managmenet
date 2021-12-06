using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivityInternal;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IClassActivityInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ClassActivity
{
    public class CuriculumService : ICuriculumService
    {
        private readonly ICuriculumServiceInternal curiculumServiceInternal;
        public CuriculumService()
        {
            curiculumServiceInternal = Singleton.GetCuriculumServiceInternal();
        }

        #region Subject
        public SubjectModel CreateSubject(SubjectModel model)
        {
            if(curiculumServiceInternal.IsSubjectExist(model.ClassID, model.SubjectName))
            {
                throw CommonMethods.GetException(CustomResponse.SUBJECT_ALREADY_EXISTS);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Subject subject = curiculumServiceInternal.AddSubject(model);
                context.Subjects.Add(subject);
                context.SaveChanges();
                model.SubjectID = subject.SubjectId;
                return model;
            }
        }
        public SubjectModel UpdateSubject(SubjectModel subjectModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Subject subject = curiculumServiceInternal.UpdateSubject(subjectModel);
                context.Subjects.AddOrUpdate(subject);
                context.SaveChanges();
                return subjectModel;
            }
        }
        public List<SubjectModel> CreateListOfSubject(List<SubjectModel> subjectModels)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (SubjectModel model in subjectModels)
                {
                    if (curiculumServiceInternal.IsSubjectExist(model.ClassID, model.SubjectName))
                    {
                        throw CommonMethods.GetException(CustomResponse.SUBJECT_ALREADY_EXISTS);
                    }
                    Subject subject = curiculumServiceInternal.AddSubject(model);
                    context.Subjects.Add(subject);
                }
                context.SaveChanges();
            }
            return subjectModels;
        }
        public SubjectModel GetSubjectById(int id)
        {
            Subject subject = curiculumServiceInternal.GetSubjectById(id);
            if (subject != null) return GetDtoModels.GetSubjectModel(subject);
            return null;
        }
        public List<SubjectModel> GetAllSubjectByClassId(int classId)
        {
            List<Subject> subjects = curiculumServiceInternal.GetAllSubjectByClassId(classId);
            List<SubjectModel> subjectModels = new List<SubjectModel>();
            foreach (Subject subject in subjects)
            {
                subjectModels.Add(GetDtoModels.GetSubjectModel(subject));
            }
            return subjectModels;
        }
        #endregion
    }
}
