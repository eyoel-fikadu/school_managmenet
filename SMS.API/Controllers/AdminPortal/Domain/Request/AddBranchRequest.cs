using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Request
{
    public class Admin_AddBranchRequest
    {

        [Required]
        [NotNull]
        public String BranchName { get; set; }
        [Required]
        [NotNull]
        public LocationModel BranchLocation { get; set; }
        [Required]
        [NotNull]
        public List<AddressModel> BranchAddress { get; set; }
        [Required]
        [NotNull]
        public bool IsBranchMain { get; set; }
    }
    public class Admin_UpdateBranchRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SchoolId { get; set; }
        [Required]
        [NotNull]
        public String BranchName { get; set; }
        [Required]
        [NotNull]
        public LocationModel BranchLocation { get; set; }
        [Required]
        [NotNull]
        public List<AddressModel> BranchAddress { get; set; }
        [Required]
        [NotNull]
        public bool IsActive { get; set; }
        [Required]
        [NotNull]
        public bool IsBranchMain { get; set; }
    }
    public class Admin_AddBranchListRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SchoolId { get; set; }
        [Required]
        [NotNull]
        public List<Admin_AddBranchRequest> branches { get; set; }
    }
}
