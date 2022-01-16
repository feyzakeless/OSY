using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OSY.DB.Entities.DataContext;
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
        private IConfiguration config; //Inject configuration
        private IResidentService residentService;
        public LoginController(IResidentService _residentService, IConfiguration _config)
        {
            config = _config;
            residentService = _residentService;
        }

        /*[HttpPost("Login")]
        public General<ResidentViewModel> Login([FromBody] LoginViewModel resident)
        {
            return residentService.Login(resident);
        }*/

        // Login İslemi
        [AllowAnonymous] //Annotation for prevent authorization
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginViewModel userLogin)
        {
           
            var user = Authenticate(userLogin); // Kimlik dogrulaması yapılır

            if(user is not null) // Eğer dogrulama sağlandıysa token a atılır. 
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("Kullanıcı bulunamadı.");
        }

        // Token Olusturma
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
                expires: System.DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Kimlik dogrulama
        private ResidentViewModel Authenticate(LoginViewModel userLogin)
        {

            ResidentViewModel currentUser= null;

            //string decodePass = OSY.Service.Extensions.Extension.DecodeBase64(userLogin.Password);
            using (var item = new OSYContext())
            {
                var user = item.Resident.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

                if (user is null)
                {
                    return null;
                }

                currentUser = new ResidentViewModel { Email = user.Email, Password = user.Password, Name = user.Name, 
                    Surname = user.Surname, IsAdmin = user.IsAdmin };
                return currentUser;
            }

        }

    }
}
