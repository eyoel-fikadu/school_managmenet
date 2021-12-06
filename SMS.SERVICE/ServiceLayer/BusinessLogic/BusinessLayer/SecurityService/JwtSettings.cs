using Microsoft.IdentityModel.Tokens;
using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService
{
    public class JwtSettings
    {
        public String GenerateToken(User user, String additionalRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(6)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role, user.NameSpace)
                
            };
            if (!string.IsNullOrEmpty(additionalRole))
            {
                claims.Add(new Claim(ClaimTypes.Role, additionalRole)); 
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TDWEB_Secret_Token_Key_For_Testing_Purpose")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public int GetCurrentUserId(ClaimsPrincipal User)
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        public List<String> GetRoleList(ClaimsPrincipal User)
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        }
    }
}
