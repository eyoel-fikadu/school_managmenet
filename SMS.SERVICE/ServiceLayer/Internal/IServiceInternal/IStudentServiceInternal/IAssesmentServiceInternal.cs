using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal
{
    public interface IAssesmentServiceInternal
    {
        #region Assesment
        Assesment AddAssesment(AssesmentModel assesmentModel);
        Assesment UpdateAssesment(AssesmentModel assesmentModel);
        List<sp_GetAssesments_Result> GetAssesments(int batchId, int classId, int sectionId, int subjectId);
        Assesment GetAssesmentById(int assesmentId);
        #endregion

        #region Exam
        Exam AddExam(ExamModel examModel);
        Exam UpdateExam(ExamModel examModel);
        #endregion

        #region Assignment
        Assignment AddAssignment(AssignmentModel assignmentModel);
        Assignment UpdateAssignment(AssignmentModel assignmentModel);
        #endregion

        #region Result
        Result AddResult(ResultModel resultModel);
        Result UpdateResult(ResultModel resultModel);
        List<sp_GetResults_Result> GetResults(int classId, int sectionId, int subjectId, int assesmentId, int studentId);

        #endregion
    }
}
