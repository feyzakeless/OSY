using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelLogin;
using OSY.Model.ModelResident;
using OSY.Service.ResidentServiceLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OSY.API.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration config;
        private IResidentService residentService;
        public LoginController(IConfiguration _config, IResidentService _residentService)
        {
            config = _config;
            residentService = _residentService;
        }

        /// <summary>
        /// NORMAL LOGIN İÇİN
        /// Login için Post işlemi gelecek
        /// Cache e kaydedilecek , Hatun hocanın dedi tek kişi girmeyebilir mantığını unutma
        /// Kaydolan kişinin cache bilgisi encyrited yapılarak şifrelenerek tutulacak (Tokenda tutulacak)
        /// </summary> 

        /*[HttpPost("Login")]
        public General<ResidentViewModel> Login([FromBody] LoginViewModel resident)
        {
            return residentService.Login(resident);
        }*/


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel userLogin)
        {
            var user = Authenticate(userLogin);

            if(user is not null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("Kullanıcı bulunamadı.");
        }

        private string Generate(ResidentViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.IsAdmin)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
                claims,
                expires: System.DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ResidentViewModel Authenticate(LoginViewModel userLogin)
        {
            
            
            using (var item = new OSYContext())
            {
                var currentUser = item.Resident.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

                if (currentUser is not null)
                {
                    //return GenerateToken(currentUser);
                }
                return null;
            }

        }

    }
}
