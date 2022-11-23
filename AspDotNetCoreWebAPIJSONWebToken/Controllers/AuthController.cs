using AspDotNetCoreWebAPIJSONWebToken.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace AspDotNetCoreWebAPIJSONWebToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (loginDTO.UserName == "aliyasin" && loginDTO.Password == "12345")
            {
                #region New JWT
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginDTO.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddSeconds(20),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                ); 
                #endregion
                return Ok(new AccessToken { ExpireDate = DateTime.Now.AddSeconds(20), Token = new JwtSecurityTokenHandler().WriteToken(token) });

            }
            return Unauthorized();

        }
    }
}
