using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelResident;
using OSY.Service.ResidentServiceLayer;
using System.Linq;
using System.Security.Claims;

namespace OSY.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]

    public class ResidentController : Controller
    {
        private readonly IResidentService residentService;
        private readonly IMapper mapper;
        public ResidentController(IResidentService _residentService, IMapper _mapper)
        {
            residentService = _residentService;
            mapper = _mapper;
        }

        private ResidentViewModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity is not null)
            {
                var userClaims = identity.Claims;

                return new ResidentViewModel
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                    Surname = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                    IsAdmin = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };

            }
            return null;
        }

        // Token Test Etme / Sisteme girmiş olan  kullanıcıyı Görme
        [HttpGet("Admins")]
        [Authorize(Roles = "Administor")]
        public IActionResult Admins()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Name}, you are an {currentUser.IsAdmin}");
        }

        // Daire Sakini Ekleme (Admin)
        [HttpPost("Insert")]
        [Authorize(Roles = "Administor")]
        public General<ResidentViewModel> Insert([FromBody] ResidentViewModel newResident)
        {
            return residentService.Insert(newResident);//CurrentUser ın Id si birden büyükse insert edip devam edicek.
        }

        // Daire Sakini Ekleme (User)
        [HttpPost("Insert/User")]
        public General<RegisterResidentViewModel> InsertForUser([FromBody] RegisterResidentViewModel newResident)
        {
            return residentService.InsertForUser(newResident);
        }

        [Authorize(Roles = "Administor")]
        // Daire Sakini listeleme
        [HttpGet]
        public General<ResidentViewModel> GetResidents()
        {
            return residentService.GetResidents();
        }

        [Authorize(Roles = "Administor")]
        // Daire Sakini Guncelleme
        [HttpPut("{id}")]
        public General<ResidentViewModel> Update(int id, [FromBody] ResidentViewModel resident)
        {
            return residentService.Update(id, resident);
        }

        [Authorize(Roles = "Administor")]
        // Daire Sakini Silme
        [HttpDelete]
        public General<ResidentViewModel> Delete(int id)
        {
            return residentService.Delete(id);
        }
    }
}
