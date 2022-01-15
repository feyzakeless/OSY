using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelHousing;
using OSY.Service.HousingServiceLayer;

namespace OSY.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class HousingController : Controller
    {
        private readonly IHousingService housingService;
        private readonly IMapper mapper;
        public HousingController(IHousingService _housingService, IMapper _mapper)
        {
            housingService = _housingService;
            mapper = _mapper;
        }


        // Blok(Bina) Ekleme
        [HttpPost]
        public General<HousingViewModel> Insert([FromBody] HousingViewModel newHousing)
        {
            General<HousingViewModel> response = new();
            response = housingService.Insert(newHousing);
            return response;
        }
        

        // Blok(Bina) Listeleme
        [HttpGet]
        public General<HousingViewModel> GetList()
        {
            return housingService.GetList();
        }


        // Blok(Bina) Guncelleme
        [HttpPut("{id}")]
        public General<HousingViewModel> Update([FromBody] HousingViewModel housing, int id)
        {
            return housingService.Update(housing, id);
        }


        // Blok(Bina) Silme
        [HttpDelete("{id}")]
        public General<HousingViewModel> Delete(int id)
        {
            return housingService.Delete(id);
        }
    }
}
