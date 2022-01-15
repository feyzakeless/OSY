using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelBill;
using OSY.Service.BillServiceLayer;

namespace OSY.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly IBillService billService;
        private readonly IMapper mapper;
       public BillController(IBillService _billService, IMapper _mapper)
        {
            billService = _billService; 
            mapper = _mapper;
        }

        // Fatura Ekleme
        [HttpPost]
        public General<BillViewModel> Insert([FromBody] BillViewModel newBill)
        {
            General<BillViewModel> response = new();
            response = billService.Insert(newBill);
            return response;
        }

        // Fatura Listeleme
        [HttpGet]
        public General<BillViewModel> GetList()
        {
            return billService.GetList();
        }

        // Fatura Guncelleme
        [HttpPut("{id}")]
        public General<BillViewModel> Update([FromBody] BillViewModel bill, int id)
        {
            return billService.Update(bill, id);
        }


        // Fatura silme
        [HttpDelete("{id}")]
        public General<BillViewModel> Delete(int id)
        {
            return billService.Delete(id);
        }
    }
}
