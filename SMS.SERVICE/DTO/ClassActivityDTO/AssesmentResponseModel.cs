using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class AssesmentResponseModel
    {
        public int classId { get; set; }
        public int sectionId { get; set; }
        public int subjectId { get; set; }
        public List<ExamResponseModel> exams { get; set; }
        public List<AssignmentResponseModel> assignments { get; set; } 
    }

    public class ExamResponseModel
    {
        public int assesmentId { get; set; }
        public int examId { get; set; }
        public String examType { get; set; } 
        public int eventId { get; set; }
        public int roomId { get; set; }
        public Nullable<DateTime> date { get; set; }
        public Nullable<TimeSpan> startTime { get; set; }
        public Nullable<TimeSpan> endTime { get; set; }
        public decimal OutOf { get; set; }

    }

    public class AssignmentResponseModel
    {
        public int assesmentId { get; set; }
        public int assignmentId { get; set; }
        public Nullable<DateTime> assignedDate { get; set; }
        public Nullable<DateTime> submissionDate { get; set; }
        public Nullable<TimeSpan> submissionTime { get; set; }
        public String assignmentType { get; set; }
        public decimal OutOf { get; set; }

    }
}
