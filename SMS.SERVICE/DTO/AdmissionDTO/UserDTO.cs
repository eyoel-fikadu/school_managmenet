using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;

namespace SMS.SERVICE.DTO
{
    public class UserModel : AddTableModel
    {

        public int UserId { get; set; }

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

        public string FullName { get; set; }

        [Required]
        [NotNull]
        public string Gender { get; set; }

        [Required]
        [NotNull]
        public DateTime DateOfBirth { get; set; }
        public String PlaceOfBirth { get; set; }
        public string NameSpace { get; set; }
        public string Token { get; set; }
        [Required]
        [NotNull]
        public string PhoneNumber { get; set; }       
        public string Email { get; set; }
        public string Remark { get; set; }
        public List<AddressModel> Address { get; set; }
        public LocationModel Location { get; set; }
        public string EmployeeType { get; set; }

    }
}
