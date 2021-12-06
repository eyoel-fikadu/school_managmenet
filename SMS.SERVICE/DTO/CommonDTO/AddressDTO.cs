using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.CommonDTO
{
    public class AddressModel : AddTableModel
    {
        public int AddressID { get; set; }
        public int ReferenceID { get; set; }
        public String AddressNameSpace { get; set; }
        [Required]
        [NotNull]
        public String AddressType { get; set; }
        [Required]
        [NotNull]
        public String AddressValue { get; set; }
        [Required]
        [NotNull]
        public bool AddressIsActive { get; set; }
        [Required]
        [NotNull]
        public bool AddressIsDefault { get; set; }

    }
}
