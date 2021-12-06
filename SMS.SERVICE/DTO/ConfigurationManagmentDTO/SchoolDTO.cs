using JetBrains.Annotations;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ConfigurationManagmentDTO
{
    public class SchoolModel : AddTableModel
    {
        public int SchoolId { get; set; }
        [NotNull]
        [Required]
        public String SchoolName { get; set; }
        [Required]
        [NotNull]
        public String SchoolDescription { get; set; }
        [Required]
        [NotNull]
        public String TinNumber { get; set; }
        public String WebSite { get; set; }
        public String Email { get; set; }
        [Required]
        [NotNull]
        public String DisplayName { get; set; }
        public String PublicID { get; set; }
        [Required]
        [NotNull]
        public String VerificationStatus { get; set; }
        [Required]
        [NotNull]
        public bool IsActive { get; set; }
        public LocationModel HeadQuarterLocation { get; set; }
        public List<AddressModel> HeadQaurterAddress { get; set; }
        public List<BranchModel> Branches { get; set; }
 
        //subscription and verification status

    }
    public class BranchModel : AddTableModel
    {
        public int BranchId { get; set; }
        public int SchoolId { get; set; }
        public String BranchName { get; set; }
        public bool IsActive { get; set; }
        public bool IsBranchMain { get; set; }
        public LocationModel BranchLocation { get; set; }
        public List<AddressModel> BranchAddress { get; set; }
    }
}
