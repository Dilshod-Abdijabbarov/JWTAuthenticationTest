using AutoMapper;
using JWTAuthenticationTest.Data;
using JWTAuthenticationTest.Models;
using JWTAuthenticationTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTAuthenticationTest.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserModelDTO user)
        {

            if (user != null)
            {
                 var entity = _loginService.Token(user);
               
                return Ok(entity);             
            }
            return NotFound("User Topilmadi.");

        }
    }

    

    
}
