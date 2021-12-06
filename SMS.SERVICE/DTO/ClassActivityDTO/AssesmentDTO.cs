using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ClassActivityDTO
{
    public class AssesmentModel : AddTableModel
    {
        public int AssesmentId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BatchId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        public string AssesmentTypeId { get; set; }
        [Required]
        [Range(1, Double.MaxValue)]
        public decimal OutOf { get; set; }

    }
    public class ExamModel : AssesmentModel
    {
        public int ExamId { get; set; }
        public int EventId { get; set; }
        [Required]
        [NotNull]
        public DateTime Date { get; set; }
        [Required]
        [NotNull]
        public DateTime StartTime { get; set; }
        [Required]
        [NotNull]
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
        [Required]
        [NotNull]
        public string ExamTypeId { get; set; }
    }
    public class AssignmentModel : AssesmentModel
    {
        public int AssignmentId { get; set; }
        [Required]
        [NotNull]
        public string AssignmentTypeId { get; set; }
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
    public class ResultModel
    {
        public int ResultId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int AssesmentId { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public decimal Score { get; set; }
        [Required]
        [NotNull]
        public bool IsAbscent { get; set; }
        [Required]
        [NotNull]
        public bool IsDisqualified { get; set; }
        public String DisqualifiedReason { get; set; }
    }
    public class ResultModelList
    {
        [Required]
        public List<ResultModel> results { get; set; }
    }
}
