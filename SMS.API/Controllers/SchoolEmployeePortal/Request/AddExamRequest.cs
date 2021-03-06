using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddExamRequest
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
}
