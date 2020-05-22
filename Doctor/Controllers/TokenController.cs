using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doctor.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Doctor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DoctorsDbContext dbContext;
        public Microsoft.Extensions.Configuration.IConfiguration _confg;
        public TokenController( Microsoft.Extensions.Configuration.IConfiguration confg, DoctorsDbContext context)
        {
           
            _confg = confg;
            dbContext = context;
        }
        [HttpPost]
        public IActionResult Post(Doctors doctor)
        {

            if (doctor != null && doctor.Email != null && doctor.Password != null)
            {
                var user = GetUser(doctor.Email, doctor.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _confg["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.DoctorId.ToString()),
                    new Claim("DoctorName", user.Name),
                    new Claim("Email", user.Email),
                    //new Claim(ClaimTypes.Role, user.Role)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_confg["Jwt:Issuer"], _confg["Jwt:Audience"], claims,
                        expires: DateTime.UtcNow.AddDays(1)
                        , signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private Doctors GetUser(string email, string password)
        {
            return dbContext.Doctors.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        //[HttpPost(Name ="Admin")]
        //public IActionResult GetAdmins(Admin admin)
        //{

        //    if (admin != null && admin.Email != null && admin.Password != null)
        //    {
        //        var user = GetAdmin(admin.Email, admin.Password);

        //        if (user != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, _confg["Jwt:Subject"]),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //            new Claim("Id", user.AdminId.ToString()),
        //            new Claim("DoctorName", user.Name),
        //            new Claim("Email", user.Email),
        //            new Claim(ClaimTypes.Role, user.Role)
        //           };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg["Jwt:Key"]));

        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //            var token = new JwtSecurityToken(_confg["Jwt:Issuer"], _confg["Jwt:Audience"], claims,
        //                expires: DateTime.UtcNow.AddDays(1)
        //                , signingCredentials: signIn);

        //            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid credentials");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //private Admin GetAdmin(string email, string password)
        //{
        //    return dbContext.Admins.FirstOrDefault(u => u.Email == email && u.Password == password);
        //}
    }
}