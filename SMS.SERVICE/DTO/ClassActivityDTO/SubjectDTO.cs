using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ConfigurationManagmentDTO
{
    public class SubjectModel : AddTableModel
    {
        public int SubjectID { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassID { get; set; }
        [Required]
        [NotNull]
        public String SubjectName { get; set; }
        public int SubjectTypeId { get; set; }

        public String subjectType { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
