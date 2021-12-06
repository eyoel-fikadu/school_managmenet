using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.Service_Layer.IService.IAdmission
{
    public interface IEnrollmentServiceInternal
    {

        #region EnrollStudent

        #region CRUD
        EnrolledStudent AddEnrolledStudent(EnrollSingleStudentModel studentModel);
        EnrolledStudent UpdateEnrolledStudent(EnrollSingleStudentModel studentModel);
        EnrolledStudent GetStudentById(int studentId);

        #endregion
        List<sp_GetStudentsListForEachClassByCalenderYear_Result> GetStudentList(int calendarYearId, int schoolId, int branchId, int classId, int sectionId, int studentId);
        EnrolledStudent GetStudentByUserId(int userId);
        List<EnrolledStudent> GetStudentsByClassId(int classId);
        List<EnrolledStudent> GetStudentsBySectionId(int sectionId);
        bool IsStudentPartOfSection(int studentId, int sectionId);
        bool IsStudentPartOfClass(int studentId, int classId);
        #endregion

        #region Employee
        Employee AddEmployee(EmployeeModel employeeModel);
        Employee UpdateEmployee(EmployeeModel employeeModel);
        Employee GetEmployeeByUserId(int userId);
        Employee GetEmployeeById(int id);
        List<sp_GetEmployees_Result> GetEmployee(int batchId, int schoolId, int branchId);
        sp_GetActiveEmployessByBranch_Result GetEmployeeByBranch(int batchId, int schoolId, int branchId, int employeeId);
        #endregion

        #region Teacher
        Teacher AddTeacher(TeacherModel teacherModel);
        Teacher UpdateTeacher(TeacherModel teacherModel);
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetTeachers(int employeeId);
        List<sp_GetEmployeesTeacher_Result> GetTeachers(int batchId, int schoolId, int branchId);
        List<sp_GetTeachersAssignedClassByClassId_Result> GetAssignedTeachers(int batchId, int schoolId, int branchId, int classId, int sectionId);
        List<sp_GetTeachersAssignedClassByTeacherId_Result> GetAssignedTeachers(int batchId, int schoolId, int branchId, int employeeId);
        #endregion
    }
}
