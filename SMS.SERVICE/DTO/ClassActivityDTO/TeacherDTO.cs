using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ClassActivityDTO
{
    public class TeacherModel : AddTableModel
    {
        public int TeacherId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        [Required]
        [NotNull]
        public String TeacherTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
