using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelBill;
using OSY.Model.ModelCreditCard;
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

        // Fatura Listeleme
        [HttpGet]
        public General<BillViewModel> GetList()
        {
            return billService.GetList();
        }

        // Ödenmiş Fatura Listeleme
        [HttpGet("PaidBills")]
        public General<BillViewModel> GetPaidBillList()
        {
            return billService.GetPaidBillList();
        }

        // Ödenmemiş Fatura Listeleme
        [HttpGet("UnPaidBills")]
        public General<BillViewModel> GetUnPaidBillList()
        {
            return billService.GetUnPaidBillList();
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

        // Fatura Atama
        [HttpPost("AssignBills")]
        public General<BillViewModel> PostBill(decimal totalPrice, AssignBillViewModel newBills)
        {
            return billService.PostBill(totalPrice, newBills);
        }

        // Toplu Fatura Odeme
        [HttpPost("PayLumpSum")]
        public General<BillViewModel> PayLumpSum(string billType, CreditCardViewModel cardModel)
        {
            return billService.PayLumpSum(billType, cardModel);
        }
    }
}
