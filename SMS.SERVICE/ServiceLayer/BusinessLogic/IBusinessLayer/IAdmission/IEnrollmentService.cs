using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission
{
    public interface IEnrollmentService
    {

        #region EnrollStudent
        EnrollSingleStudentModel EnrollStudent(EnrollSingleStudentModel studentModel);
        EnrollSingleStudentModel UpdateEnrolledStudent(EnrollSingleStudentModel studentModel);
        EnrolledMultipleStudentModel EnrollStudent(EnrolledMultipleStudentModel studentModel);
        EnrollSingleStudentModel AssignSection(int studentId, int sectionId, int updatedBy);
        List<EnrollSingleStudentModel> AssignSection(List<int> studentId, int sectionId, int updatedBy);
        StudentsModel GetStudents(int calendarYearId, int schoolId, int branchId, int classId, int sectionId, int studentId);
        bool IsStudentActive(int userId);
        bool IsStudentPartOfSection(int studentId, int sectionId);
        bool IsStudentPartOfClass(int studentId, int classId);
        #endregion

        #region Employee
        EmployeeModel AddEmployee(EmployeeModel employeeModel);
        EmployeeModel UpdateEmployee(EmployeeModel employeeModel);
        bool IsEmployeeActive(int userId);
        Employee GetActiveEmployeeByUserId(int userId);
        bool IsEmployeeMemeber(int employeeId, int batchId, int branchId);
        List<EmployeeModelResponse> GetEmployeesBySchool(int batchId, int schoolId, int branchId);
        List<EmployeeModelResponse> GetTeacherEmployeesBySchool(int batchId, int schoolId, int branchId);
        #endregion

        #region Teacher
        TeacherModel AddTeacher(TeacherModel teacherModel);
        TeacherModel UpdateTeacher(TeacherModel teacherModel);
        List<TeacherModelResponse> GetAssignedTeacherByClass(int batchId, int schoolId, int branchId, int classId, int sectionId);
        TeacherModelResponse GetAssignedTeacherByEmployee(int batchId, int schoolId, int branchId, int employeeId);
        bool IsTeacherAssignedToSubject(int employeeId, int classId, int sectionId, int subjectId);
        bool IsEmployeeTeacher(int userId);
        #endregion

    }
}
