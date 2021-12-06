using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.Admission
{
    public class EnrollmentService : IEnrollmentService
    {
        private IEnrollmentServiceInternal enrollmentServiceInternal;
        private IUserServiceInternal userServiceInternal;
        private ISchoolServiceInternal schoolServiceInternal;

        public EnrollmentService()
        {
            enrollmentServiceInternal = Singleton.GetEnrollmentServiceInternal();
            userServiceInternal = Singleton.GetUserServiceInternal();
            schoolServiceInternal = Singleton.GetSchoolServiceInternal();
        }

        #region Enrollment
        public EnrollSingleStudentModel EnrollStudent(EnrollSingleStudentModel model)
        {
            if (!isUserStudent(model.UserId))
            {
                throw CommonMethods.GetException(CustomResponse.ERROR_RESPONSE_USER_IS_NOT_STUDENT);
            }
            if (IsStudentActive(model.UserId))
            {
                throw CommonMethods.GetException(CustomResponse.STUDENT_ALREADY_REGISTERED);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                EnrolledStudent enrolledStudent = enrollmentServiceInternal.AddEnrolledStudent(model);
                context.EnrolledStudents.Add(enrolledStudent);
                context.SaveChanges();
                model.EnrolledStudentId = enrolledStudent.EnrolledStudentId;
                return model;
            }
            
        }
        public EnrollSingleStudentModel UpdateEnrolledStudent(EnrollSingleStudentModel model)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                EnrolledStudent enrolledStudent = enrollmentServiceInternal.UpdateEnrolledStudent(model);
                context.EnrolledStudents.AddOrUpdate(enrolledStudent);
                context.SaveChanges();
                return model;
            }
        }
        public EnrolledMultipleStudentModel EnrollStudent(EnrolledMultipleStudentModel model)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                EnrollSingleStudentModel enrollSingle = new EnrollSingleStudentModel()
                {
                    BatchId = model.BatchId,
                    ClassId = model.ClassId,
                    EnrolledDate = model.EnrolledDate,
                    IsActive = true,
                    createdBy = model.createdBy,
                    updatedBy = model.updatedBy
                };
                foreach (int user in model.UserId)
                {
                    if (!isUserStudent(user))
                    {
                        throw CommonMethods.GetException(CustomResponse.ERROR_RESPONSE_USER_IS_NOT_STUDENT);
                    }
                    if (IsStudentActive(user))
                    {
                        throw CommonMethods.GetException(CustomResponse.STUDENT_ALREADY_REGISTERED);
                    }
                    enrollSingle.UserId = user;
                    EnrolledStudent enrolledStudent = enrollmentServiceInternal.AddEnrolledStudent(enrollSingle);
                    context.EnrolledStudents.Add(enrolledStudent);
                }
                context.SaveChanges();
                return model;
            }
        }
        public EnrollSingleStudentModel AssignSection(int studentId, int sectionId, int updatedBy)
        {
            var student = enrollmentServiceInternal.GetStudentById(studentId);
            if(student != null)
            {
                var section = schoolServiceInternal.GetSectionById(sectionId);
                if(student.ClassId != section.ClassId)
                {
                    throw CommonMethods.GetException(CustomResponse.STUDENT_IS_NOT_PART_OF_A_CLASS);
                }
                using (SCMSEntities context = new SCMSEntities())
                {
                    EnrollSingleStudentModel model = new EnrollSingleStudentModel()
                    {
                        BatchId = student.BatchId,
                        ClassId = student.ClassId,
                        EnrolledDate = student.EnrolledDate,
                        EnrolledStudentId = student.EnrolledStudentId,
                        IsActive = student.IsActive,
                        SectionId = sectionId,
                        UserId = student.UserId,
                        updatedBy = updatedBy
                    };

                    EnrolledStudent enrolledStudent = enrollmentServiceInternal.UpdateEnrolledStudent(model);
                    context.EnrolledStudents.AddOrUpdate(enrolledStudent);
                    context.SaveChanges();
                    return model;
                }
            }
            else
            {
                throw CommonMethods.GetException(CustomResponse.STUDENT_DOESNT_EXIST);
            }
        }
        public List<EnrollSingleStudentModel> AssignSection(List<int> studentId, int sectionId, int updatedBy)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                List<EnrollSingleStudentModel> enrollSingleStudentModels = new List<EnrollSingleStudentModel>();
                var section = schoolServiceInternal.GetSectionById(sectionId);
                if (section != null)
                {
                    var students = enrollmentServiceInternal.GetStudentsByClassId(section.ClassId);
                    foreach (var std in studentId)
                    {
                        var student = students.FirstOrDefault(x => x.EnrolledStudentId == std);
                        if (student != null)
                        {
                            EnrollSingleStudentModel model = new EnrollSingleStudentModel()
                            {
                                BatchId = student.BatchId,
                                ClassId = section.ClassId,
                                EnrolledDate = student.EnrolledDate,
                                EnrolledStudentId = student.EnrolledStudentId,
                                IsActive = student.IsActive,
                                SectionId = sectionId,
                                UserId = student.UserId,
                                updatedBy = updatedBy
                            };
                            EnrolledStudent enrolledStudent = enrollmentServiceInternal.UpdateEnrolledStudent(model);
                            context.EnrolledStudents.AddOrUpdate(enrolledStudent);
                            enrollSingleStudentModels.Add(model);
                        }
                        else
                        {
                            throw CommonMethods.GetException(CustomResponse.STUDENT_DOESNT_EXIST);
                        }
                    }
                }
                context.SaveChanges();
                return enrollSingleStudentModels;
            }
        }
        public StudentsModel GetStudents(int calendarYearId, int schoolId, int branchId, int classId, int sectionId, int studentId)
        {
            var studentList = enrollmentServiceInternal.GetStudentList(calendarYearId, schoolId, branchId, classId, sectionId, studentId);
            return GetDtoModels.GetStudentsModel(studentList);
        }
        private bool isUserStudent(int userId)
        {
            var user = userServiceInternal.GetUserById(userId);
            if (user != null)
            {
                if (user.NameSpace == ConstantValues.LOOKUP_VALUE_NAMESPACE_STUDENT)
                    return true;
            }
            return false;
        }
        public bool IsStudentPartOfSection(int studentId, int sectionId)
        {
            using(var context = new SCMSEntities())
            {
                var std = context.EnrolledStudents.FirstOrDefault(x => x.IsActive && x.SectionId == sectionId);
                if (std == null) return false;
                return true;
            }
        }
        public bool IsStudentPartOfClass(int studentId, int classId)
        {
            using (var context = new SCMSEntities())
            {
                var std = context.EnrolledStudents.FirstOrDefault(x => x.IsActive && x.ClassId == classId);
                if (std == null) return false;
                return true;
            }
        }
        #endregion

        #region Employee
        public EmployeeModel AddEmployee(EmployeeModel model)
        {
            if (!isUserEmployee(model.UserId))
            {
                throw CommonMethods.GetException(CustomResponse.ERROR_RESPONSE_USER_IS_NOT_EMPLOYEE);
            }
            if (userServiceInternal.GetActiveEmployeResult(model.UserId) != null)
            {
                throw CommonMethods.GetException(CustomResponse.EMPLOYEE_ALREADY_REGISTERED);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Employee employee = enrollmentServiceInternal.AddEmployee(model);
                employee = context.Employees.Add(employee);
                context.SaveChanges();
                model.EmployeeId = employee.EmployeeId;
                return model;
            }
        }

        public EmployeeModel UpdateEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }
        public List<EmployeeModelResponse> GetEmployeesBySchool(int batchId, int schoolId, int branchId)
        {
            List<sp_GetEmployees_Result> employees = enrollmentServiceInternal.GetEmployee(batchId, schoolId, branchId);
            return GetDtoModels.GetEmployeeModel(employees);
        }
        public List<EmployeeModelResponse> GetTeacherEmployeesBySchool(int batchId, int schoolId, int branchId)
        {
            List<sp_GetEmployees_Result> employees = enrollmentServiceInternal.GetEmployee(batchId, schoolId, branchId);
            return GetDtoModels.GetEmployeeModel(employees.Where(x => x.EmployeeTypeId == ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER).ToList());
        }
        public bool IsEmployeeActive(int userId)
        {
            Employee employee = GetActiveEmployeeByUserId(userId);
            if (employee == null) return false;
            return true;
        }
        public bool IsEmployeeMemeber(int employeeId, int batchId, int branchId)
        {
            Employee employee = enrollmentServiceInternal.GetEmployeeById(employeeId);
            if (employee == null) return false;
            if(employee.BranchId == branchId && employee.BatchId == batchId)
            {
                return true;
            }
            return false;
        }
        public Employee GetActiveEmployeeByUserId(int userId)
        {
            var employee = enrollmentServiceInternal.GetEmployeeByUserId(userId);
            if (employee != null) return employee;
            return null;
        }
        private bool isUserEmployee(int userId)
        {
            var user = userServiceInternal.GetUserById(userId);
            if (user != null)
            {
                if (user.NameSpace == ConstantValues.LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE)
                    return true;
            }
            return false;
        }
        #endregion

        #region Teacher
        public TeacherModel AddTeacher(TeacherModel model)
        {
            if (IsTeacherAssignedToSubject(model.EmployeeId, model.ClassId, model.SectionId, model.SubjectId))
            {
                throw CommonMethods.GetException(CustomResponse.TEACHER_ALREADY_ASSIGNED);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                Teacher teacher = enrollmentServiceInternal.AddTeacher(model);
                teacher = context.Teachers.Add(teacher);
                context.SaveChanges();
                model.TeacherId = teacher.TeacherId;
                return model;
            }
        }
        public TeacherModel UpdateTeacher(TeacherModel teacherModel)
        {
            throw new NotImplementedException();
        }
        public List<TeacherModel> GetTeacherBySchool(int batchId, int schoolId, int branchId)
        {
            List<sp_GetEmployeesTeacher_Result> teacher = enrollmentServiceInternal.GetTeachers(batchId, schoolId, branchId);
            return GetDtoModels.GetTeacherDto(teacher);
        }
        public List<TeacherModelResponse> GetAssignedTeacherByClass(int batchId, int schoolId, int branchId, int classId, int sectionId)
        {
            List<sp_GetTeachersAssignedClassByClassId_Result> teacher = enrollmentServiceInternal.GetAssignedTeachers(batchId, schoolId, branchId, classId, sectionId);
            return GetDtoModels.GetAssignedTeacherResponse(teacher);
        }
        public TeacherModelResponse GetAssignedTeacherByEmployee(int batchId, int schoolId, int branchId, int employeeId)
        {
            List<sp_GetTeachersAssignedClassByTeacherId_Result> teacher = enrollmentServiceInternal.GetAssignedTeachers(batchId, schoolId, branchId, employeeId);
            return GetDtoModels.GetAssignedTeacherResponse(teacher);
        }
        public bool IsTeacherAssignedToSubject(int employeeId, int classId, int sectionId, int subjectId)
        {
            var teachers = enrollmentServiceInternal.GetTeachers(employeeId);
            if (teachers.FirstOrDefault(x => x.ClassId == classId && x.SectionId == sectionId && x.SubjectId == subjectId && x.IsActive) == null) 
                return false;
            return true;
        }
        public bool IsEmployeeTeacher(int employeeId)
        {
            var employee = enrollmentServiceInternal.GetEmployeeById(employeeId);
            if(employee.EmployeeTypeId == ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER)
            {
                return true;
            }
            return false;
        }
        public bool IsStudentActive(int userId)
        {
            EnrolledStudent student = enrollmentServiceInternal.GetStudentByUserId(userId);
            if (student == null) return false;
            return true;
        }
        #endregion
    }
}
