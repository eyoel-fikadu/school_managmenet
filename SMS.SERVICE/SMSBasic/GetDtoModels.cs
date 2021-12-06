using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.SMSBasic;

namespace SMS.SERVICE.DTO
{
    public class GetDtoModels
    {
        public static UserModel GetUserModel(User user)
        {
            if (user == null) return null;
            UserModel userModel = new UserModel()
            {
                UserId = user.UserID,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = user.FullName,
                NameSpace = user.NameSpace,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return userModel;
        }
        internal static List<AttendanceResponseModel> GetAttendanceResponse(List<sp_GetAttendance_Result> results)
        {
            List<AttendanceResponseModel> responses = new List<AttendanceResponseModel>();
            foreach(var att in results)
            {
                AttendanceResponseModel attendance = new AttendanceResponseModel()
                {
                    branchId = att.BranchId,
                    classId = att.ClassId,
                    className = att.ClassName,
                    dateOfBirth = att.DateOfBirth,
                    firstName = att.FirstName,
                    fullName = att.FullName,
                    gender = att.Gender,
                    lastName = att.LastName,
                    late = att.Late,
                    middleName = att.MiddleName,
                    permission = att.Permission,
                    phoneNumber = att.PhoneNumber,
                    present = att.Present,
                    scheduleId = att.ScheduleId,
                    scheudleDetailId = att.ScheduleDetailId,
                    sectionId = CommonMethods.GetValue(att.SectionId),
                    sectionName = att.SectionName,
                    subjectId = att.SubjectId,
                    subjectName = att.SubjectName
                };
                responses.Add(attendance);
            }
            return responses;
        }
        public static SchoolModel GetSchoolModel(School school)
        {
            if (school == null) return null;
            SchoolModel schoolModel = new SchoolModel
            {
                SchoolId = school.SchoolID,
                SchoolName = school.SchoolName,
                DisplayName = school.DisplayName,
                SchoolDescription = school.Description,
                TinNumber = school.TinNumber,
                WebSite = school.Website,
                Email = school.Email
            };
            return schoolModel;
        }
        public static List<ScheduleResponseModel> GetScheduleModelList(List<sp_GetScheduleListByBatchAndBranch_Result> schedules)
        {
            if (schedules == null) return null;
            List<ScheduleResponseModel> scheduleModels = new List<ScheduleResponseModel>();
            foreach(var schedule in schedules)
            {
                scheduleModels.Add(new ScheduleResponseModel()
                {
                    classId = schedule.ClassId,
                    className = schedule.ClassName,
                    dayOfWeek = schedule.DayOfTheWeekId,
                    isActive = schedule.IsActive,
                    scheduleId = schedule.ScheduleId,
                    sectionId = CommonMethods.GetValue(schedule.SectionId),
                    sectionName = schedule.SectionName,
                    subjectId = schedule.SubjectId,
                    subjectName = schedule.SubjectName,
                    timeTableDescription = schedule.Description,
                    timeTableId = schedule.TimeTableId,
                    employeeId = CommonMethods.GetValue(schedule.EmployeeId),
                    teacherName = schedule.FullName
                });
            }
            return scheduleModels;
        }
        public static List<ScheduleResponseModel> GetScheduleModelList(List<sp_GetScheduleByEmployee_Result> schedules)
        {
            if (schedules == null) return null;
            List<ScheduleResponseModel> scheduleModels = new List<ScheduleResponseModel>();
            foreach (var schedule in schedules)
            {
                scheduleModels.Add(new ScheduleResponseModel()
                {
                    classId = schedule.ClassId,
                    className = schedule.ClassName,
                    dayOfWeek = schedule.DayOfTheWeekId,
                    isActive = schedule.IsActive,
                    scheduleId = schedule.ScheduleId,
                    sectionId = CommonMethods.GetValue(schedule.SectionId),
                    sectionName = schedule.SectionName,
                    subjectId = schedule.SubjectId,
                    subjectName = schedule.SubjectName,
                    timeTableDescription = schedule.Description,
                    timeTableId = schedule.TimeTableId,
                    employeeId = CommonMethods.GetValue(schedule.EmployeeId),
                    teacherName = schedule.FullName
                });
            }
            return scheduleModels;
        }
        public static ScheduleModel GetScheduleModel(Schedule schedule)
        {
            if (schedule == null) return null;
            ScheduleModel model = new ScheduleModel()
            {
                ClassId = schedule.ClassId,
                IsActive = schedule.IsActive,
                ScheduleId = schedule.ScheduleId,
                SectionId = CommonMethods.GetValue(schedule.SectionId),
                SubjectId = schedule.SubjectId,
                TimeTableId = schedule.TimeTableId
            };
            return model;
        }

        internal static List<ResultResponseModel> GetResults(List<sp_GetResults_Result> results)
        {
            List<ResultResponseModel> responses = new List<ResultResponseModel>();
            foreach (var res in results)
            {
                ResultResponseModel result = new ResultResponseModel()
                {
                    classId = res.ClassId,
                    className = res.ClassName,
                    firstName = res.FirstName,
                    fullName = res.FullName,
                    gender = res.Gender,
                    lastName = res.LastName,
                    middleName = res.MiddleName,
                    phoneNumber = res.PhoneNumber,
                    sectionId = CommonMethods.GetValue(res.SectionId),
                    sectionName = res.SectionName,
                    subjectId = res.SubjectId,
                    subjectName = res.SubjectName,
                    assesmentId = res.AssesmentId,
                    disqualifiedReason = res.DisqualifiedReason,
                    isAbscent = res.IsAbscent,
                    outOf = res.OutOf,
                    isDisqualified = res.IsDisqualified,
                    score = res.Score,
                    assmentType = res.AssesmentTypeId
                };
                responses.Add(result);
            }
            return responses;
        }

        internal static AssesmentModel GetAssesmentModel(Assesment assesment)
        {
            if (assesment == null) return null;
            return new AssesmentModel()
            {
                AssesmentId = assesment.AssesmentId,
                AssesmentTypeId = assesment.AssesmentTypeId,
                OutOf = assesment.OutOf,
                SectionId = CommonMethods.GetValue(assesment.SectionId),
                SubjectId = assesment.SubjectId,
                ClassId = assesment.ClassId,
                BatchId = assesment.BatchId
            };
        }

        internal static AssesmentResponseModel GetAssesments(List<sp_GetAssesments_Result> result)
        {
            sp_GetAssesments_Result _Result = result.FirstOrDefault();
            if(_Result == null)
            {
                return null;
            }
            AssesmentResponseModel response = new AssesmentResponseModel()
            {
                classId = _Result.ClassId,
                sectionId = CommonMethods.GetValue(_Result.SectionId),
                subjectId = _Result.SubjectId
            };
            
            List<ExamResponseModel> examResponse = new List<ExamResponseModel>();
            List<AssignmentResponseModel> assignmentResponses = new List<AssignmentResponseModel>(); 
            foreach(var assesment in result)
            {
                if(assesment.AssesmentTypeId == ConstantValues.LOOKUP_VALUE_ASSESMENT_ASSIGNMENT)
                {
                    AssignmentResponseModel assignment = new AssignmentResponseModel()
                    {
                        assesmentId = assesment.AssesmentId,
                        assignedDate = assesment.AssignedDate,
                        assignmentId = CommonMethods.GetValue(assesment.AssignmentId),
                        assignmentType = assesment.AssignmentTypeId,
                        submissionDate = assesment.SubmissionDate,
                        submissionTime = assesment.SubmissionTIme,
                        OutOf = assesment.OutOf
                    };
                    assignmentResponses.Add(assignment);
                }
                else if(assesment.AssesmentTypeId == ConstantValues.LOOKUP_VALUE_ASSESMENT_EXAM)
                {
                    ExamResponseModel exam = new ExamResponseModel()
                    {
                        assesmentId = assesment.AssesmentId,
                        date = assesment.Date,
                        endTime = assesment.EndTime,
                        eventId = CommonMethods.GetValue(assesment.EventId),
                        examId = CommonMethods.GetValue(assesment.ExamId),
                        examType = assesment.ExamTypeId,
                        roomId = CommonMethods.GetValue(assesment.RoomId),
                        startTime = assesment.StartTime,
                        OutOf = assesment.OutOf
                    };
                    examResponse.Add(exam);
                }

            }
            response.exams = examResponse;
            response.assignments = assignmentResponses;
            return response;
        }

        internal static TeacherModelResponse GetAssignedTeacherResponse(List<sp_GetTeachersAssignedClassByTeacherId_Result> teacher)
        {
            var assignedTeacher = teacher.FirstOrDefault();
            if (assignedTeacher == null) return null;
            TeacherModelResponse response = new TeacherModelResponse()
            {
                EmployeeId = CommonMethods.GetValue(assignedTeacher.EmployeeId),
                EmployeeType = ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER,
                FirstName = assignedTeacher.FirstName,
                MiddleName = assignedTeacher.MiddleName,
                Gender = assignedTeacher.Gender,
                LastName = assignedTeacher.LastName,
                UserId = CommonMethods.GetValue(assignedTeacher.UserId),
                
            };

            List<ClassInformation> classInformation = new List<ClassInformation>();
            foreach(var info in teacher)
            {
                ClassInformation information = new ClassInformation()
                {
                    classId = CommonMethods.GetValue(info.ClassId),
                    className = info.ClassName,
                    sectionId = CommonMethods.GetValue(info.SectionId),
                    sectionName = info.SectionName,
                    subjectId = CommonMethods.GetValue(info.SubjectId),
                    subjectName = info.SubjectName

                };
                classInformation.Add(information);
            }
            response.classInformation = classInformation;
            return response;
        }
        internal static TeacherModelResponse GetAssignedTeacherResponseByClass(List<sp_GetTeachersAssignedClassByClassId_Result> teacher)
        {
            var assignedTeacher = teacher.FirstOrDefault();
            if (assignedTeacher == null) return null;
            TeacherModelResponse response = new TeacherModelResponse()
            {
                EmployeeId = CommonMethods.GetValue(assignedTeacher.EmployeeId),
                EmployeeType = ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER,
                FirstName = assignedTeacher.FirstName,
                MiddleName = assignedTeacher.MiddleName,
                Gender = assignedTeacher.Gender,
                LastName = assignedTeacher.LastName,
                UserId = CommonMethods.GetValue(assignedTeacher.UserId),

            };

            List<ClassInformation> classInformation = new List<ClassInformation>();
            foreach (var info in teacher)
            {
                ClassInformation information = new ClassInformation()
                {
                    classId = CommonMethods.GetValue(info.ClassId),
                    className = info.ClassName,
                    sectionId = CommonMethods.GetValue(info.SectionId),
                    sectionName = info.SectionName,
                    subjectId = CommonMethods.GetValue(info.SubjectId),
                    subjectName = info.SubjectName

                };
                classInformation.Add(information);
            }
            response.classInformation = classInformation;
            return response;
        }

        internal static List<TeacherModelResponse> GetAssignedTeacherResponse(List<sp_GetTeachersAssignedClassByClassId_Result> teacher)
        {
            List<TeacherModelResponse> teacherResponses = new List<TeacherModelResponse>();
            List<int> employeeId = teacher.Select(x => x.EmployeeId.Value).Distinct().ToList();
            foreach(int id in employeeId)
            {
                var teacherReponse = teacher.Where(x => x.EmployeeId == id).ToList();
                teacherResponses.Add(GetAssignedTeacherResponseByClass(teacherReponse));
            }
            return teacherResponses;
        }

        public static List<ScheduleDetailModel> GetScheduleDetailModelList(List<ScheduleDetail> scheduleDetails)
        {
            List<ScheduleDetailModel> list = new List<ScheduleDetailModel>();
            foreach(ScheduleDetail detail in scheduleDetails)
            {
                list.Add(new ScheduleDetailModel()
                {
                    Date = detail.Date,
                    EndTime = DateTime.Now + detail.EndTime,
                    IsActive = detail.IsActive,
                    ScheduleDetailId = detail.ScheduleDetailId,
                    ScheduleId = detail.Schedule,
                    StartTime = DateTime.Now + detail.StartTime,
                    TeacherId = detail.TeacherId == null ? 0 : detail.TeacherId.Value
                });
            }
            return list;
        }
        public static CalendarYearModel GetCalendarYear(CalendarYear calendarYear)
        {
            if (calendarYear == null) return null;
            CalendarYearModel calendar = new CalendarYearModel()
            {
                CalendarYearId = calendarYear.CalendarYearId,
                EndDate = calendarYear.EndDate,
                StartDate = calendarYear.StartDate,
                IsActive = calendarYear.IsActive,
                yearDescription = calendarYear.CalendarYearDescription
            };
            return calendar;
        }
        internal static List<EmployeeModelResponse> GetEmployeeModel(List<sp_GetEmployees_Result> employees)
        {
            List<EmployeeModelResponse> employeeModels = new List<EmployeeModelResponse>();
            foreach(var result in employees)
            {
                EmployeeModelResponse model = new EmployeeModelResponse()
                {
                    EmployeeId = result.EmployeeId,
                    EmployeeType = result.EmployeeTypeId,
                    EndDate = result.EndDate,
                    IsEmployeeActive = result.IsActive,
                    StartDate = result.StartDate,
                    UserId = result.UserId,
                    FirstName = result.FirstName,
                    MiddleName = result.MiddleName,
                    LastName = result.LastName
                };
                employeeModels.Add(model);
            }
            return employeeModels;
        }
        internal static List<TeacherModel> GetTeacherDto(List<sp_GetEmployeesTeacher_Result> teacher)
        {
            List<TeacherModel> teacherModels = new List<TeacherModel>();
            foreach(var result in teacher)
            {
                TeacherModel model = new TeacherModel()
                {
                    EmployeeId = result.EmployeeId,
                    EndDate = result.EndDate,
                    IsActive = result.IsActive,
                    StartDate = result.StartDate,
                    TeacherId = result.TeacherId
                };
                teacherModels.Add(model);
            }
            return teacherModels;
        }
        public static LocationModel GetLocationModel(Location location)
        {
            return new LocationModel
            {
                LocationId = location.LocationId,
                RefernceId = location.ReferenceId,
                LocationNameSpace = location.LocationNameSpace,
                Country = location.Country,
                City = location.City,
                AddressLocation = location.Area,
                Region = location.Region,
                Lattitude = location.Latitude.HasValue ? location.Latitude.Value : 0,
                Longtiude = location.Longtitude.HasValue ? location.Longtitude.Value : 0
            };
        }
        public static BranchModel GetBranchModel(Branch branch)
        {
            return new BranchModel
            {
                BranchId = branch.BranchId,
                SchoolId = branch.SchoolId,
                BranchName = branch.BranchName,
                IsBranchMain = branch.IsMainBranch,
                IsActive = branch.IsActive
            };
        }
        public static List<AddressModel> GetAddressModel(List<Address> addresses)
        {
            List<AddressModel> addressModels = new List<AddressModel>();
            foreach (Address address in addresses)
            {
                addressModels.Add(GetAddressModel(address));
            }
            return addressModels;
        }
        public static AddressModel GetAddressModel(Address address)
        {
            AddressModel addressModel = new AddressModel
            {
                AddressID = address.AddressId,
                ReferenceID = address.ReferenceId,
                AddressNameSpace = address.AddressNameSpace,
                AddressType = address.AddressType,
                AddressValue = address.AddressValue
            };
            return addressModel;
        }
        public static ClassModel GetClassModel(Class @class)
        {
            return new ClassModel()
            {
                BranchId = @class.BranchId,
                ClassId = @class.ClassId,
                ClassName = @class.ClassName,
                ClassTypeId = @class.ClassTypeId.HasValue ? @class.ClassTypeId.Value : 0,
                IsActive = @class.IsActive,
                HasSection = @class.HasSection
            };
        }
        public static SectionModel GetSectionModel(Section section)
        {
            return new SectionModel()
            {
                AssignedRoom = section.AssignedRoom.HasValue ? section.AssignedRoom.Value : 0,
                IsActive = section.IsActive,
                ClassId = section.ClassId,
                SectionId = section.SectionId,
                SectionName = section.SectionName
            };

        }
        public static SubjectModel GetSubjectModel(Subject subject)
        {
            return new SubjectModel()
            {
                ClassID = subject.ClassId,
                IsActive = subject.IsActive,
                SubjectID = subject.SubjectId,
                SubjectName = subject.SubjectName,
                SubjectTypeId = subject.SubjectTypeId.HasValue ? subject.SubjectTypeId.Value : 0
            };
        }
        public static BranchDetailModel GetBranchDetailModel(List<sp_GetActiveClassInformation_Result> result)
        {
            BranchDetailModel branchDetailModel = new BranchDetailModel();
            var branch = result.FirstOrDefault();
            branchDetailModel.BranchId = branch.BranchId;
            branchDetailModel.BranchName = branch.BranchName;
            branchDetailModel.IsBranchMain = branch.IsMainBranch;
            branchDetailModel.SchoolId = branch.SchoolID;
            branchDetailModel.SchoolName = branch.SchoolName;
            branchDetailModel.BatchId = branch.BatchId;

            List<ClassDetail> classDetails = new List<ClassDetail>();
            foreach(var single in result)
            {
                classDetails.Add(new ClassDetail()
                {
                    ClassID = CommonMethods.GetValue(single.ClassId),
                    ClassName = single.ClassName,
                    SectionId = CommonMethods.GetValue(single.SectionId),
                    SubjectID = CommonMethods.GetValue(single.SubjectId),
                    SectionName = single.SectionName,
                    SubjectName = single.SubjectName,
                    EmployeeId = CommonMethods.GetValue(single.EmployeeId),
                    TeacherId = CommonMethods.GetValue(single.TeacherId),
                    TeacherName = single.FullName,
                    TeacherType = single.TeacherTypeId
                });
            }
            branchDetailModel.classDetails = classDetails;
            return branchDetailModel;
        }
        public static StudentsModel GetStudentsModel(List<sp_GetStudentsListForEachClassByCalenderYear_Result> results)
        {
            var cCyear = results.FirstOrDefault();
            if (cCyear == null) return null;
            StudentsModel studentsModel = new StudentsModel()
            {
                calendarYearId = cCyear.CalendarYearId,
                schoolId = cCyear.SchoolID,
                schoolName = cCyear.SchoolName
            };

            List<StudentList> list = new List<StudentList>();
            foreach(var single in results)
            {
                list.Add(new StudentList()
                {
                    branchId = single.BranchId,
                    branchName = single.BranchName,
                    classId = single.ClassId,
                    className = single.ClassName,
                    sectionId = CommonMethods.GetValue(single.SectionId),
                    sectionName = single.SectionName,
                    studentFullName = single.FullName,
                    userId = single.UserID,
                    enrolledStudentId = single.EnrolledStudentId
                });
            }
            studentsModel.studentLists = list;
            return studentsModel;
        }
        public static List<TimeTableModel> GetTimeTable(List<TimeTable> timeTables)
        {
            List<TimeTableModel> timeTableModels = new List<TimeTableModel>();
            foreach(TimeTable timeTable in timeTables)
            {
                timeTableModels.Add(new TimeTableModel()
                {
                    BatchId = timeTable.BatchId,
                    BranchId = timeTable.BranchId,
                    DayOfTheWeek = timeTable.DayOfTheWeekId,
                    Description = timeTable.Description,
                    IsActive = timeTable.IsActive,
                    EndTimeResult = timeTable.EndTime.ToString(),
                    StartTimeResult = timeTable.StartTime.ToString(),
                    TimeTableId = timeTable.TimeTableId
                });
            }
            return timeTableModels;
        }
    }
}
