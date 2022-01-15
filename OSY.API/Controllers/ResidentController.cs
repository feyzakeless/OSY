using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelResident;
using OSY.Service.ResidentServiceLayer;

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

        /// <summary>
        /// KULLANICI KAYIT EKLEME
        /// Eğer Admin ise;
        /// İsim - Soyisim - Telefon - TC - Email - Password(değişken) - Daire - Blok - Plaka - IsAdmin
        /// Eğer Yeni bir kullanıcı ise;
        /// İsim - Soyisim - Telefon - TC - Email
        /// Eğer adminin girdiği bilgi ile Kullanıcı bilgisi uyuşmuyorsa veya yoksa hata mesajı verecek,
        /// uyuşuyorsa otomatik şifre gönderecek kullanıcıya
        /// </summary>
        
        // Daire Sakini Ekleme
        [HttpPost]
        public General<ResidentViewModel> Insert([FromBody] ResidentViewModel newResident)
        {
            return residentService.Insert(newResident);//CurrentUser ın Id si birden büyükse insert edip devam edicek.
        }

        // Daire Sakini listeleme
        [HttpGet]
        public General<ResidentViewModel> GetResidents()
        {
            return residentService.GetResidents();
        }

        // Daire Sakini Guncelleme
        [HttpPut("{id}")]
        public General<ResidentViewModel> Update(int id, [FromBody] ResidentViewModel resident)
        {
            return residentService.Update(id, resident);
        }

        // Daire Sakini Silme
        [HttpDelete]
        public General<ResidentViewModel> Delete(int id)
        {
            return residentService.Delete(id);
        }
    }
}
