using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddBranchRequest
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
    }
    public class UpdateBranchRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
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
    public class AddBranchListRequest
    {
        [Required]
        [NotNull]
        public List<AddBranchRequest> branches { get; set; }
    }
}
