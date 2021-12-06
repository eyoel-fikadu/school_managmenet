using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.AdmissionDTO
{
    public class ClassModel : AddTableModel
    {
        public int ClassId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }

        [Required]
        [NotNull]
        public String ClassName { get; set; }
        public int ClassTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public bool HasSection { get; set; }

    }
    public class SectionModel : AddTableModel
    {
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        [NotNull]
        public String SectionName { get; set; }
        public int AssignedRoom { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
