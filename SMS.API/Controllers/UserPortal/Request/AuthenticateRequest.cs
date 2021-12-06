using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.UserPortal.Request
{
    public class AuthenticateRequest
    {
        [Required]
        [NotNull]
        public string UserName { get; set; }
        
        [Required]
        [NotNull]
        public string Password { get; set; }
    }
}
