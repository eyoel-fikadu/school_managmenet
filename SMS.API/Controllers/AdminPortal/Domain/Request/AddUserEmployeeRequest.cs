using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Request
{
    public class Admin_AddUserEmployeeRequest
    {
        [Required]
        [NotNull]
        public string UserName { get; set; }

        [Required]
        [NotNull]
        public string Password { get; set; }
        [Required]
        [NotNull]
        public string FirstName { get; set; }

        [Required]
        [NotNull]
        public string MiddleName { get; set; }

        [Required]
        [NotNull]
        public string LastName { get; set; }

        [Required]
        [NotNull]
        public string Gender { get; set; }

        [Required]
        [NotNull]
        public DateTime DateOfBirth { get; set; }
        public String PlaceOfBirth { get; set; }
        [Required]
        [NotNull]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public List<AddressModel> Address { get; set; }
        public LocationModel Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
    }
}
