using CrudApiAspNetCoreSql.Data;
using CrudApiAspNetCoreSql.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        public readonly AppDbContext _context;

        public TokenController(IConfiguration config, AppDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        // GET: Token
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        // GET: Token/Menu
        [HttpGet("/Token/Menu")]
        public async Task<IActionResult> Menu()
        {
            return View("Menu");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([Bind("UserName,UserPassword")] User _user)
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
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JwtConfig:Issuer"], 
                        audience: _configuration["JwtConfig:Audience"], 
                        claims: claims,
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                        signingCredentials: signIn
                        );

                    // return Ok(new JwtSecurityTokenHandler().WriteToken(token)); // USANDO POSTMAN: Retorna o token como uma resposta do servidor, daí tem que copiar esse token e colocar como uma parâmetro no header > authentication > bearer + token para acessar os métodos da API que tenham o [Authorize]

                    string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                    //Save token in session object
                    HttpContext.Session.SetString("JWToken", accessToken);

                    return RedirectToAction("Menu", "Token");
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Please enter your login and password!");
            }
        }

        [HttpGet("/Token/GetUser")]
        private async Task<User> GetUser(string username, string password)
        {
            return _context.User.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);
        }

        [HttpGet("/Token/Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Token");
        }

    }
    
}
