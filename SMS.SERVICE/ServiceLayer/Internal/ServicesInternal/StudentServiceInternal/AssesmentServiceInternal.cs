using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.StudentServiceInternal
{
    public class AssesmentServiceInternal : IAssesmentServiceInternal
    {
        #region Assesment
        public Assesment AddAssesment(AssesmentModel assesmentModel)
        {
            Assesment assesment = new Assesment()
            {
                AssesmentTypeId = assesmentModel.AssesmentTypeId,
                BatchId = assesmentModel.BatchId,
                ClassId = assesmentModel.ClassId,
                OutOf = assesmentModel.OutOf,
                CreatedDate = DateTime.Now,
                SectionId = assesmentModel.SectionId,
                SubjectId = assesmentModel.SubjectId,
                UpdatedDate = DateTime.Now
            };
            return assesment;
        }
        public Assesment UpdateAssesment(AssesmentModel assesmentModel)
        {
            Assesment assesment = new Assesment()
            {
                AssesmentId = assesmentModel.AssesmentId,
                AssesmentTypeId = assesmentModel.AssesmentTypeId,
                BatchId = assesmentModel.BatchId,
                ClassId = assesmentModel.ClassId,
                OutOf = assesmentModel.OutOf,
                SectionId = assesmentModel.SectionId,
                SubjectId = assesmentModel.SubjectId,
                UpdatedDate = DateTime.Now
            };
            return assesment;
        }
        public List<sp_GetAssesments_Result> GetAssesments(int batchId, int classId, int sectionId, int subjectId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetAssesments(batchId, classId, sectionId, subjectId).ToList();
            }
        }
        public Assesment GetAssesmentById(int assesmentId)
        {
            using(var context = new SCMSEntities())
            {
                return context.Assesments.Find(assesmentId);
            }
        }

        #endregion

        #region Assignment
        public Assignment AddAssignment(AssignmentModel assignmentModel)
        {
            Assignment assignment = new Assignment()
            {
                AssesmentId = assignmentModel.AssesmentId,
                UpdatedDate = DateTime.Now,
                AssignmentTypeId = assignmentModel.AssignmentTypeId,
                AssignedDate = assignmentModel.AssignedDate,
                CreatedDate = DateTime.Now,
                SubmissionDate = assignmentModel.SubmissionDate,
                SubmissionTIme = assignmentModel.SubmissionTime.TimeOfDay

            };
            return assignment;
        }
        public Assignment UpdateAssignment(AssignmentModel assignmentModel)
        {
            Assignment assignment = new Assignment()
            {
                AssignmentId = assignmentModel.AssignmentId,
                AssesmentId = assignmentModel.AssesmentId,
                UpdatedDate = DateTime.Now,
                AssignmentTypeId = assignmentModel.AssignmentTypeId,
                AssignedDate = assignmentModel.AssignedDate,
                SubmissionDate = assignmentModel.SubmissionDate,
                SubmissionTIme = assignmentModel.SubmissionTime.TimeOfDay
            };
            return assignment;
        }

        #endregion

        #region Exam
        public Exam AddExam(ExamModel examModel)
        {
            Exam exam = new Exam()
            {
                AssesmentId = examModel.AssesmentId,
                CreatedDate = DateTime.Now,
                Date = examModel.Date,
                EndTime = examModel.EndTime.TimeOfDay,
                EventId = examModel.EventId,
                ExamTypeId = examModel.ExamTypeId,
                RoomId = examModel.RoomId,
                StartTime = examModel.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now
            };
            return exam;
        }    
        public Exam UpdateExam(ExamModel examModel)
        {
            Exam exam = new Exam()
            {
                ExamId = examModel.ExamId,
                AssesmentId = examModel.AssesmentId,
                Date = examModel.Date,
                EndTime = examModel.EndTime.TimeOfDay,
                EventId = examModel.EventId,
                ExamTypeId = examModel.ExamTypeId,
                RoomId = examModel.RoomId,
                StartTime = examModel.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now
            };
            return exam;
        }
        #endregion

        #region Result
        public Result AddResult(ResultModel resultModel)
        {
            Result result = new Result()
            {
                AssesmentId = resultModel.AssesmentId,
                CreatedDate = DateTime.Now,
                IsAbscent = resultModel.IsAbscent,
                IsDisqualified = resultModel.IsDisqualified,
                StudentId = resultModel.StudentId,
                Score = resultModel.Score,
                UpdatedDate = DateTime.Now,
                DisqualifiedReason = resultModel.DisqualifiedReason
            };
            return result;
        }
        public Result UpdateResult(ResultModel resultModel)
        {
            Result result = new Result()
            {
                ResultId = resultModel.ResultId,
                AssesmentId = resultModel.AssesmentId,
                IsAbscent = resultModel.IsAbscent,
                IsDisqualified = resultModel.IsDisqualified,
                StudentId = resultModel.StudentId,
                Score = resultModel.Score,
                UpdatedDate = DateTime.Now,
                DisqualifiedReason = resultModel.DisqualifiedReason
            };
            return result;
        }
        public List<sp_GetResults_Result> GetResults(int classId, int sectionId, int subjectId, int assesmentId, int studentId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetResults(classId, sectionId, subjectId, assesmentId, studentId).ToList();
            }
        }

        #endregion
    }
}
