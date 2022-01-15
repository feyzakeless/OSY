using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelApartment;
using OSY.Service.ApartmentServiceLayer;

namespace OSY.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class ApartmentController : Controller
    {
        private readonly IApartmentService apartmentService;
        private readonly IMapper mapper;
        public ApartmentController(IApartmentService _apartmentService, IMapper _mapper)
        {
            apartmentService = _apartmentService;   
            mapper = _mapper;
        }


        // Daire Ekleme
        [HttpPost]
        public General<ApartmentViewModel> Insert([FromBody] ApartmentViewModel newApartment)
        {
            General<ApartmentViewModel> response = new();
            response = apartmentService.Insert(newApartment);
            return response;
        }

        // Daire Listeleme
        [HttpGet]
        public General<ApartmentViewModel> GetList()
        {
            return apartmentService.GetList();
        }

        // Daire Guncelleme
        [HttpPut("{id}")]
        public General<ApartmentViewModel> Update([FromBody] ApartmentViewModel apartment, int id)
        {
            return apartmentService.Update(apartment, id);
        }

        // Daire Silme
        [HttpDelete("{id}")]
        public General<ApartmentViewModel> Delete(int id)
        {
            return apartmentService.Delete(id);
        }
    }
}
