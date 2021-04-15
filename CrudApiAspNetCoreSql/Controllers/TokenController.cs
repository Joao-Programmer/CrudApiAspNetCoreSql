﻿using CrudApiAspNetCoreSql.Data;
using CrudApiAspNetCoreSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Controllers
{
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly AppDbContext _context;

        public TokenController(IConfiguration config, AppDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User _user)
        {
            if (_user != null && _user.UserName != null && _user.UserPassword != null)
            {
                var user = await GetUser(_user.UserName, _user.UserPassword);

                if (user != null)
                {
                    // create claims details on the user information
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserFullName", user.UserFullName),
                    new Claim("UserName", user.UserName),
                    new Claim("UserEmail", user.UserEmail)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"], _configuration["JwtConfig:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        private async Task<User> GetUser(string username, string password)
        {
            return _context.User.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);
        }
            
    }
    
}