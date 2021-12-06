using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.CommonDTO
{
    public class LocationModel : AddTableModel
    {
        public int LocationId { get; set; }
        public int RefernceId { get; set; }
        public String LocationNameSpace { get; set; }
        [Required]
        [NotNull]
        public String Country { get; set; }
        public String Region { get; set; }  
        public String City { get; set; }
        public String AddressLocation { get; set; }//this is to generalize woreda/kebele or road and any information, we will discus and decide about it
        public decimal Lattitude { get; set; }
        public decimal Longtiude { get; set; }
        public bool IsActive { get; set; }

    }
}
