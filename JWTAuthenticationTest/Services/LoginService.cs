using AutoMapper;
using JWTAuthenticationTest.Data;
using JWTAuthenticationTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthenticationTest.Services
{
    public class LoginService : ILoginService
    {
        private static List<Claim> AuthClaims { get; set; }
        private readonly AuthenticationTest _dbcontext;
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        public LoginService(AuthenticationTest dbcontext,IConfiguration configuration,IMapper mapper) 
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
            _mapper = mapper;
        }

        public string Token(UserModelDTO user)
        {
            var userInfo = _dbcontext.Users.Where(d => d.Username == user.Username).Include(x=> x.Roles).FirstOrDefault();

            var roleInfo = _dbcontext.Roles.Where(s => userInfo.Roles.Select(d=>d.RoleId).Contains(s.Id)).Select(f=>f.Name);
                   
            if (userInfo != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                AuthClaims = new List<Claim>
                {
                      new Claim(ClaimTypes.Name,userInfo.Username),
                      new Claim("Password ",userInfo.Password),
                     // new Claim(ClaimTypes.Role, string.Join(",", roleInfo)),
                      //new Claim("Roles", string.Join(",", roleInfo))
                };

                foreach (var item in roleInfo)
                {
                    AuthClaims.Add(new Claim(ClaimTypes.Role, item));
                }
                var Sectoken = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    AuthClaims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return token;
            }
            else
            {
                return "Bunday foydalanuvchi topilmadi.";
            }
            return string.Empty;
        }
    }
}
