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
            var userInfo =_dbcontext.UserModels.FirstOrDefault(d => d.Username == user.Username && d.Password == user.Password);

            if(userInfo != null)
            {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                
                    var clamis = new[]
                    {
                      new Claim(ClaimTypes.Name,userInfo.Username),
                      new Claim("Password ",userInfo.Password),
                      
                      new Claim(ClaimTypes.Role,userInfo.Roles.FirstOrDefault().ToString())
                    };
                    var Sectoken = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Issuer"],
                        clamis,
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
