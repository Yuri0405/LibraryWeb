using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryBackEnd.Model;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using LibraryBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace LibraryBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly BookDbContext _db;

        public UsersController(BookDbContext db)
        {
            _db = db;
        }

        private ClaimsIdentity GetIdentity(string username,string password)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if(user != null)
            {
                var claims = new List<Claim>
                { new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)};

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            var record = await _db.Users.FirstOrDefaultAsync(r => r.UserName == user.UserName);

            if(record == null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            else
            {
                return BadRequest("User already exist!");
            }

            return Ok(user.UserName);
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(User user)
        {
            var identity = GetIdentity(user.UserName, user.Password);
            if(identity == null)
            {
                //return BadRequest("Invalid username or password");
                return Unauthorized("Invalid username or password");
            }

            var now = DateTime.UtcNow;

            //create JWT token
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("usercheck")]
        public ActionResult SomeUseraction()
        {
            return Ok($"Your login: {User.Identity.Name}");
        }

    }
}
