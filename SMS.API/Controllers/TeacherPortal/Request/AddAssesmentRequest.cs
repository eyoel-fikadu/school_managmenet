using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Request
{
    public class AddAssesmentRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        [Required]
        [Range(1, Double.MaxValue)]
        public decimal OutOf { get; set; }
    }
    public class Teacher_AddExamRequest : AddAssesmentRequest
    {
        [Required]
        [NotNull]
        public DateTime Date { get; set; }
        [Required]
        [NotNull]
        public DateTime StartTime { get; set; }
        [Required]
        [NotNull]
        public DateTime EndTime { get; set; }
        [Required]
        [NotNull]
        public string ExamType { get; set; }
    }
    public class Teacher_AddAssignmentRequest : AddAssesmentRequest
    {
        [Required]
        [NotNull]
        public string AssignmentType { get; set; }
        [Required]
        [NotNull]
        public DateTime AssignedDate { get; set; }
        [Required]
        [NotNull]
        public DateTime SubmissionDate { get; set; }
        [Required]
        [NotNull]
        public DateTime SubmissionTime { get; set; }
    }
}
