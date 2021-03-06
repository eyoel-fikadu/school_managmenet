//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCMS.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SCMSEntities : DbContext
    {
        public SCMSEntities()
            : base("name=SCMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Assesment> Assesments { get; set; }
        public virtual DbSet<AssesmentType> AssesmentTypes { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CalendarYear> CalendarYears { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassType> ClassTypes { get; set; }
        public virtual DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<EnrolledStudent> EnrolledStudents { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<NamespaceType> NamespaceTypes { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherType> TeacherTypes { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<vw_ActiveClassInformation> vw_ActiveClassInformation { get; set; }
        public virtual DbSet<vw_ActiveEmployess> vw_ActiveEmployess { get; set; }
        public virtual DbSet<vw_ActiveStudents> vw_ActiveStudents { get; set; }
        public virtual DbSet<vw_AttendanceView> vw_AttendanceView { get; set; }
        public virtual DbSet<vw_RegisteredBranchPerCalendarYear> vw_RegisteredBranchPerCalendarYear { get; set; }
        public virtual DbSet<vw_ResultView> vw_ResultView { get; set; }
        public virtual DbSet<vw_ScheduleView> vw_ScheduleView { get; set; }
        public virtual DbSet<vw_TeacherAssignedClassView> vw_TeacherAssignedClassView { get; set; }
    
        public virtual ObjectResult<sp_GetActiveEmployess_Result> sp_GetActiveEmployess(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetActiveEmployess_Result>("sp_GetActiveEmployess", userIdParameter);
        }
    
        public virtual ObjectResult<sp_GetActiveEmployessByBranch_Result> sp_GetActiveEmployessByBranch(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId, Nullable<int> employeeId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("employeeId", employeeId) :
                new ObjectParameter("employeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetActiveEmployessByBranch_Result>("sp_GetActiveEmployessByBranch", batchIdParameter, schoolIdParameter, branchIdParameter, employeeIdParameter);
        }
    
        public virtual ObjectResult<sp_GetActiveStudents_Result> sp_GetActiveStudents(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetActiveStudents_Result>("sp_GetActiveStudents", userIdParameter);
        }
    
        public virtual ObjectResult<sp_GetAssesments_Result> sp_GetAssesments(Nullable<int> batchId, Nullable<int> classId, Nullable<int> sectionId, Nullable<int> subjectId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            var subjectIdParameter = subjectId.HasValue ?
                new ObjectParameter("subjectId", subjectId) :
                new ObjectParameter("subjectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAssesments_Result>("sp_GetAssesments", batchIdParameter, classIdParameter, sectionIdParameter, subjectIdParameter);
        }
    
        public virtual ObjectResult<sp_GetBranchOnActiveCalendarYear_Result> sp_GetBranchOnActiveCalendarYear()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetBranchOnActiveCalendarYear_Result>("sp_GetBranchOnActiveCalendarYear");
        }
    
        public virtual ObjectResult<sp_GetEmployees_Result> sp_GetEmployees(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetEmployees_Result>("sp_GetEmployees", batchIdParameter, schoolIdParameter, branchIdParameter);
        }
    
        public virtual ObjectResult<sp_GetEmployeesTeacher_Result> sp_GetEmployeesTeacher(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetEmployeesTeacher_Result>("sp_GetEmployeesTeacher", batchIdParameter, schoolIdParameter, branchIdParameter);
        }
    
        public virtual ObjectResult<sp_GetActiveClassInformation_Result> sp_GetActiveClassInformation(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId, Nullable<int> classId, Nullable<int> sectionId, Nullable<int> subjectId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            var subjectIdParameter = subjectId.HasValue ?
                new ObjectParameter("subjectId", subjectId) :
                new ObjectParameter("subjectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetActiveClassInformation_Result>("sp_GetActiveClassInformation", batchIdParameter, schoolIdParameter, branchIdParameter, classIdParameter, sectionIdParameter, subjectIdParameter);
        }
    
        public virtual ObjectResult<sp_GetTeachersAssignedClassByClassId_Result> sp_GetTeachersAssignedClassByClassId(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId, Nullable<int> classId, Nullable<int> sectionId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetTeachersAssignedClassByClassId_Result>("sp_GetTeachersAssignedClassByClassId", batchIdParameter, schoolIdParameter, branchIdParameter, classIdParameter, sectionIdParameter);
        }
    
        public virtual ObjectResult<sp_GetTeachersAssignedClassByTeacherId_Result> sp_GetTeachersAssignedClassByTeacherId(Nullable<int> batchId, Nullable<int> schoolId, Nullable<int> branchId, Nullable<int> employeeId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("employeeId", employeeId) :
                new ObjectParameter("employeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetTeachersAssignedClassByTeacherId_Result>("sp_GetTeachersAssignedClassByTeacherId", batchIdParameter, schoolIdParameter, branchIdParameter, employeeIdParameter);
        }
    
        public virtual ObjectResult<sp_GetScheduleByEmployee_Result> sp_GetScheduleByEmployee(Nullable<int> batchId, Nullable<int> branchId, Nullable<int> employeeId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("employeeId", employeeId) :
                new ObjectParameter("employeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetScheduleByEmployee_Result>("sp_GetScheduleByEmployee", batchIdParameter, branchIdParameter, employeeIdParameter);
        }
    
        public virtual ObjectResult<sp_GetAttendance_Result> sp_GetAttendance(Nullable<int> classId, Nullable<int> sectionId, Nullable<int> subjectId, Nullable<int> scheduleId, Nullable<int> scheduleDetailId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<int> studentId)
        {
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            var subjectIdParameter = subjectId.HasValue ?
                new ObjectParameter("subjectId", subjectId) :
                new ObjectParameter("subjectId", typeof(int));
    
            var scheduleIdParameter = scheduleId.HasValue ?
                new ObjectParameter("scheduleId", scheduleId) :
                new ObjectParameter("scheduleId", typeof(int));
    
            var scheduleDetailIdParameter = scheduleDetailId.HasValue ?
                new ObjectParameter("scheduleDetailId", scheduleDetailId) :
                new ObjectParameter("scheduleDetailId", typeof(int));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            var studentIdParameter = studentId.HasValue ?
                new ObjectParameter("studentId", studentId) :
                new ObjectParameter("studentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAttendance_Result>("sp_GetAttendance", classIdParameter, sectionIdParameter, subjectIdParameter, scheduleIdParameter, scheduleDetailIdParameter, startDateParameter, endDateParameter, studentIdParameter);
        }
    
        public virtual ObjectResult<sp_GetResults_Result> sp_GetResults(Nullable<int> classId, Nullable<int> sectionId, Nullable<int> subjectId, Nullable<int> assesmentId, Nullable<int> studentId)
        {
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            var subjectIdParameter = subjectId.HasValue ?
                new ObjectParameter("subjectId", subjectId) :
                new ObjectParameter("subjectId", typeof(int));
    
            var assesmentIdParameter = assesmentId.HasValue ?
                new ObjectParameter("assesmentId", assesmentId) :
                new ObjectParameter("assesmentId", typeof(int));
    
            var studentIdParameter = studentId.HasValue ?
                new ObjectParameter("studentId", studentId) :
                new ObjectParameter("studentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetResults_Result>("sp_GetResults", classIdParameter, sectionIdParameter, subjectIdParameter, assesmentIdParameter, studentIdParameter);
        }
    
        public virtual ObjectResult<sp_GetStudentsListForEachClassByCalenderYear_Result> sp_GetStudentsListForEachClassByCalenderYear(Nullable<int> calenderYearId, Nullable<int> schoolId, Nullable<int> branchId, Nullable<int> classId, Nullable<int> sectionId, Nullable<int> studentId)
        {
            var calenderYearIdParameter = calenderYearId.HasValue ?
                new ObjectParameter("calenderYearId", calenderYearId) :
                new ObjectParameter("calenderYearId", typeof(int));
    
            var schoolIdParameter = schoolId.HasValue ?
                new ObjectParameter("schoolId", schoolId) :
                new ObjectParameter("schoolId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            var studentIdParameter = studentId.HasValue ?
                new ObjectParameter("studentId", studentId) :
                new ObjectParameter("studentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetStudentsListForEachClassByCalenderYear_Result>("sp_GetStudentsListForEachClassByCalenderYear", calenderYearIdParameter, schoolIdParameter, branchIdParameter, classIdParameter, sectionIdParameter, studentIdParameter);
        }
    
        public virtual ObjectResult<sp_GetScheduleListByBatchAndBranch_Result> sp_GetScheduleListByBatchAndBranch(Nullable<int> batchId, Nullable<int> branchId, Nullable<int> timeTableId, Nullable<int> classId, Nullable<int> sectionId)
        {
            var batchIdParameter = batchId.HasValue ?
                new ObjectParameter("batchId", batchId) :
                new ObjectParameter("batchId", typeof(int));
    
            var branchIdParameter = branchId.HasValue ?
                new ObjectParameter("branchId", branchId) :
                new ObjectParameter("branchId", typeof(int));
    
            var timeTableIdParameter = timeTableId.HasValue ?
                new ObjectParameter("timeTableId", timeTableId) :
                new ObjectParameter("timeTableId", typeof(int));
    
            var classIdParameter = classId.HasValue ?
                new ObjectParameter("classId", classId) :
                new ObjectParameter("classId", typeof(int));
    
            var sectionIdParameter = sectionId.HasValue ?
                new ObjectParameter("sectionId", sectionId) :
                new ObjectParameter("sectionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetScheduleListByBatchAndBranch_Result>("sp_GetScheduleListByBatchAndBranch", batchIdParameter, branchIdParameter, timeTableIdParameter, classIdParameter, sectionIdParameter);
        }
    }
}
