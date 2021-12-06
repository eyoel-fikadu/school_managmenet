using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.StudentService
{
    public class AssesmentService : IAssesmentService
    {

        private IAssesmentServiceInternal assesmentServiceInternal;

        public AssesmentService()
        {
            assesmentServiceInternal = Singleton.GetAssesmentServiceInternal();
        }
        #region Assignment
        public AssignmentModel AddAssignment(AssignmentModel assignmentModel)
        {
            Assesment assesment = assesmentServiceInternal.AddAssesment(assignmentModel);
            Assignment assignment = assesmentServiceInternal.AddAssignment(assignmentModel);
            using(SCMSEntities context = new SCMSEntities())
            {
                assesment.AssesmentTypeId = ConstantValues.LOOKUP_VALUE_ASSESMENT_ASSIGNMENT;
                assesment = context.Assesments.Add(assesment);
                context.SaveChanges();
                assignment.AssesmentId = assesment.AssesmentId;
                assignment = context.Assignments.Add(assignment);
                context.SaveChanges();
            };
            assignmentModel.AssignmentId = assignment.AssignmentId;
            assignmentModel.AssesmentId = assignment.AssesmentId;
            return assignmentModel;
        }
        public AssignmentModel UpdateAssignment(AssignmentModel assignmentModel)
        {
            Assesment assesment = assesmentServiceInternal.UpdateAssesment(assignmentModel);
            Assignment assignment = assesmentServiceInternal.UpdateAssignment(assignmentModel);
            using (SCMSEntities context = new SCMSEntities())
            {
                assesment.AssesmentTypeId = ConstantValues.LOOKUP_VALUE_ASSESMENT_ASSIGNMENT;
                context.Assesments.AddOrUpdate(assesment);
                context.Assignments.AddOrUpdate(assignment);
                context.SaveChanges();
            };
            return assignmentModel;

        }
        #endregion

        #region Exam
        public ExamModel AddExam(ExamModel examModel)
        {
            Assesment assesment = assesmentServiceInternal.AddAssesment(examModel);
            Exam exam = assesmentServiceInternal.AddExam(examModel);
            using (SCMSEntities context = new SCMSEntities())
            {
                assesment.AssesmentTypeId = ConstantValues.LOOKUP_VALUE_ASSESMENT_EXAM;
                assesment = context.Assesments.Add(assesment);
                context.SaveChanges();
                exam.AssesmentId = assesment.AssesmentId;
                exam = context.Exams.Add(exam);
                context.SaveChanges();
            };
            examModel.ExamId = exam.ExamId;
            examModel.AssesmentId = exam.AssesmentId;
            return examModel;
        }
        public ExamModel UpdateExam(ExamModel examModel)
        {
            Assesment assesment = assesmentServiceInternal.AddAssesment(examModel);
            Exam exam = assesmentServiceInternal.AddExam(examModel);
            using (SCMSEntities context = new SCMSEntities())
            {
                assesment.AssesmentTypeId = ConstantValues.LOOKUP_VALUE_ASSESMENT_EXAM;
                context.Assesments.AddOrUpdate(assesment);
                exam.AssesmentId = assesment.AssesmentId;
                context.Exams.AddOrUpdate(exam);
                context.SaveChanges();
            };
            return examModel;
        }
        #endregion

        #region Result
        public ResultModelList AddResults(ResultModelList results)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (ResultModel resultModel in results.results)
                {
                    Result res = assesmentServiceInternal.AddResult(resultModel);
                    context.Results.Add(res);
                }
                context.SaveChanges();
                return results;
            }
        }
        public List<ResultResponseModel> GetResult(int classId, int sectionId, int subjectId, int assesmentId, int studentId)
        {
            List<sp_GetResults_Result> results = assesmentServiceInternal.GetResults(classId, sectionId, subjectId, assesmentId, studentId);
            return GetDtoModels.GetResults(results);
        }
        #endregion

        #region Assesment
        public AssesmentResponseModel GetAssesment(int batchId, int classId, int sectionId, int subjectId)
        {
            List<sp_GetAssesments_Result> result = assesmentServiceInternal.GetAssesments(batchId, classId, sectionId, subjectId);
            return GetDtoModels.GetAssesments(result);
        }

        public AssesmentModel GetAssesmentById(int assesmentId)
        {
            return GetDtoModels.GetAssesmentModel(assesmentServiceInternal.GetAssesmentById(assesmentId));
        }
        #endregion
    }
}
