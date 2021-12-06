using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMS.SERVICE.DTO.AdmissionDTO
{
    public class EnrollSingleStudentModel : AddTableModel
    {
        public int EnrolledStudentId { get; set; }
        public int BatchId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int UserId { get; set; }
        [Required]
        public DateTime EnrolledDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int SectionId { get; set; }
    }
    public class EnrolledMultipleStudentModel : AddTableModel
    {
        public int EnrolledStudentId { get; set; }
        public int BatchId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        public List<int> UserId { get; set; }
        [Required]
        public DateTime EnrolledDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int SectionId { get; set; }
    }
}
