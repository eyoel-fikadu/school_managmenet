using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.Services.Admission
{
    public class EnrollmentServiceInternal : IEnrollmentServiceInternal
    {
        #region student enrollment
        public EnrolledStudent AddEnrolledStudent(EnrollSingleStudentModel model)
        {
            EnrolledStudent student = new EnrolledStudent()
            {
                UserId = model.UserId,
                ClassId = model.ClassId,
                BatchId = model.BatchId,
                IsActive = true,
                SectionId = model.SectionId,
                IsSectionAssigned = model.SectionId == 0 ? false : true,
                EnrolledDate = model.EnrolledDate,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };
            return student;

        }
        public EnrolledStudent UpdateEnrolledStudent(EnrollSingleStudentModel model)
        {
            EnrolledStudent student = new EnrolledStudent()
            {
                EnrolledStudentId = model.EnrolledStudentId,
                UserId = model.UserId,
                ClassId = model.ClassId,
                BatchId = model.BatchId,
                IsActive = model.IsActive,
                SectionId = model.SectionId,
                IsSectionAssigned = model.SectionId == 0 ? false : true,
                EnrolledDate = model.EnrolledDate,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };
            return student;
        }
        public List<sp_GetStudentsListForEachClassByCalenderYear_Result> GetStudentList(int calendarYearId, int schoolId, int branchId, int classId, int sectionId, int studentId)
        {
            using(SCMSEntities context = new SCMSEntities())
                return context.sp_GetStudentsListForEachClassByCalenderYear(calendarYearId, schoolId, branchId, classId, sectionId, studentId).ToList();
        }
        public EnrolledStudent GetStudentByUserId(int userId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EnrolledStudents.Where(x => x.UserId == userId && x.IsActive).FirstOrDefault();
            }
        }
        public List<EnrolledStudent> GetStudentsByClassId(int classId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EnrolledStudents.Where(x => x.ClassId == classId && x.IsActive).ToList();
            }
        }
        public List<EnrolledStudent> GetStudentsBySectionId(int sectionId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EnrolledStudents.Where(x => x.SectionId == sectionId && x.IsActive).ToList();
            }
        }
        public bool IsStudentPartOfSection(int studentId, int sectionId)
        {
            using (var context = new SCMSEntities())
            {
                var std = context.EnrolledStudents.FirstOrDefault(x => x.IsActive && x.SectionId == sectionId && x.EnrolledStudentId == studentId);
                if (std == null) return false;
                return true;
            }
        }
        public bool IsStudentPartOfClass(int studentId, int classId)
        {
            using (var context = new SCMSEntities())
            {
                var std = context.EnrolledStudents.FirstOrDefault(x => x.IsActive && x.ClassId == classId && x.EnrolledStudentId == studentId);
                if (std == null) return false;
                return true;
            }
        }
        #endregion

        #region Employee
        public Employee AddEmployee(EmployeeModel model)
        {
            Employee employee = new Employee()
            {
                BatchId = model.BatchId,
                BranchId = model.BranchId,
                CreatedDate = DateTime.Now,
                EmployeeTypeId = model.EmployeeType,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                IsActive = true,
                UserId = model.UserId,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };

            return employee;
        }
        public Employee UpdateEmployee(EmployeeModel model)
        {
            Employee employee = new Employee()
            {
                BatchId = model.BatchId,
                BranchId = model.BranchId,
                EmployeeTypeId = model.EmployeeType,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                IsActive = model.IsActive,
                UserId = model.UserId,
                UpdatedDate = DateTime.Now,
                EmployeeId = model.EmployeeId,
                UpdatedBy = model.updatedBy
            };

            return employee;
        }
        public List<sp_GetEmployees_Result> GetEmployee(int batchId, int schoolId, int branchId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetEmployees(batchId, schoolId, branchId).ToList();
            }
        }
        public Employee GetEmployeeByUserId(int userId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Employees.Where(x => x.UserId == userId && x.IsActive).FirstOrDefault();
            }
        }
        public Employee GetEmployeeById(int id)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Employees.Where(x => x.EmployeeId == id && x.IsActive).FirstOrDefault();
            }
        }
        public sp_GetActiveEmployessByBranch_Result GetEmployeeByBranch(int batchId, int schoolId, int branchId, int employeeId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetActiveEmployessByBranch(batchId, schoolId, branchId, employeeId).FirstOrDefault();
            }
        }
        #endregion

        #region Teacher
        public Teacher AddTeacher(TeacherModel model)
        {
            Teacher teacher = new Teacher()
            {
                ClassId = model.ClassId,
                CreatedDate = DateTime.Now,
                EmployeeId = model.EmployeeId,
                EndDate = model.EndDate,
                IsActive = true,
                SectionId = model.SectionId,
                SubjectId = model.SubjectId,
                TeacherTypeId = model.TeacherTypeId,
                StartDate = model.StartDate,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };

            return teacher;
        }
        public Teacher UpdateTeacher(TeacherModel model)
        {
            Teacher teacher = new Teacher()
            {
                TeacherId = model.TeacherId,
                ClassId = model.ClassId,
                EmployeeId = model.EmployeeId,
                EndDate = model.EndDate,
                IsActive = model.IsActive,
                SectionId = model.SectionId,
                SubjectId = model.SubjectId,
                TeacherTypeId = model.TeacherTypeId,
                StartDate = model.StartDate,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };

            return teacher;
        }
        public List<Teacher> GetTeachers(int employeeId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Teachers.Where(x => x.EmployeeId == employeeId && x.IsActive).ToList();
            }
        }
        public List<sp_GetEmployeesTeacher_Result> GetTeachers(int batchId, int schoolId, int branchId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetEmployeesTeacher(batchId, schoolId, branchId).ToList();
            }
        }
        public Teacher GetTeacherById(int teacherId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Teachers.FirstOrDefault(x => x.TeacherId == teacherId && x.IsActive);
            }
        }
        public List<sp_GetTeachersAssignedClassByClassId_Result> GetAssignedTeachers(int batchId, int schoolId, int branchId, int classId, int sectionId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetTeachersAssignedClassByClassId(batchId, schoolId, branchId, classId, sectionId).ToList();
            }
        }
        public List<sp_GetTeachersAssignedClassByTeacherId_Result> GetAssignedTeachers(int batchId, int schoolId, int branchId, int employeeId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetTeachersAssignedClassByTeacherId(batchId, schoolId, branchId, employeeId).ToList();
            }
        }

        public EnrolledStudent GetStudentById(int studentId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EnrolledStudents.FirstOrDefault(x => x.EnrolledStudentId == studentId && x.IsActive);
            }
        }
        #endregion
    }
}
