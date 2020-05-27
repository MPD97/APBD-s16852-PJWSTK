using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cw4.DTOs;
using Cw4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cw4.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; set; }
        public IStudentDbService Service { get; set; }
        public IPasswordService passwordService { get; set; }

        public AccountController(IConfiguration configuration, IStudentDbService service, IPasswordService passwordService)
        {
            Configuration = configuration;
            Service = service;
            this.passwordService = passwordService;
        }

        [HttpPost]
        public IActionResult Login(LoginModel request)
        {
            var dbData = Service.GetHashAndSalt(request.index);
            if (dbData == null)
            {
                return BadRequest("Taki użytkownik nie isniteje");
            }

            var result = passwordService.Validate(request.Password, dbData.Salt, dbData.Password);
            if (result == false)
            {
                return BadRequest("Zły login, lub hasło");
            }

            string refreshToken = Guid.NewGuid().ToString();
            Service.SaveRefreshToken(refreshToken, new LoginSaltModel { index = request.index, Password = dbData.Password, Salt = dbData.Salt});


            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, request.index),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken
            });
        }
        [HttpPost("viaRefreshToken")]
        public IActionResult RefreshToken(string rToken)
        {
            var index = Service.LoginViaRefreshToken(rToken);
            if (index == null)
            {
                return BadRequest("Nieprawidłowy refresh token.");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, index),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = rToken
            });
        }
    }
}