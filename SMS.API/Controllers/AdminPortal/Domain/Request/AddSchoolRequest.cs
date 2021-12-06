using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Request
{
    public class AddSchoolRequest
    {
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
        [Required]
        [NotNull]
        public bool IsActive { get; set; }
        public LocationModel HeadQuarterLocation { get; set; }
        public List<AddressModel> HeadQaurterAddress { get; set; }
        public List<BranchModel> Branches { get; set; }
    }
    public class UpdateSchoolRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int schoolId { get; set; }
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
        public bool IsActive { get; set; }
        public LocationModel HeadQuarterLocation { get; set; }
        public List<AddressModel> HeadQaurterAddress { get; set; }
        public List<BranchModel> Branches { get; set; }
    }
}
