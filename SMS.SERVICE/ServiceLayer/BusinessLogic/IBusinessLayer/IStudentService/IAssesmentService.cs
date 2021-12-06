using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService
{
    public interface IAssesmentService
    {
        #region Assesment
        AssesmentResponseModel GetAssesment(int batchId, int classId, int sectionId, int subjectId);
        AssesmentModel GetAssesmentById(int assesmentId);
        #endregion

        #region Exam
        ExamModel AddExam(ExamModel examModel);
        ExamModel UpdateExam(ExamModel examModel);
        #endregion

        #region Assignment
        AssignmentModel AddAssignment(AssignmentModel assignmentModel);
        AssignmentModel UpdateAssignment(AssignmentModel assignmentModel);

        #endregion

        #region Result
        ResultModelList AddResults(ResultModelList result);
        List<ResultResponseModel> GetResult(int classId, int sectionId, int subjectId, int assesmentId, int studentId);

        #endregion
    }
}
