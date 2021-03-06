using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> GetList()
        {
            return billService.GetList();
        }

        // Ödenmiş Fatura Listeleme
        [HttpGet("PaidBills")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> GetPaidBillList()
        {
            return billService.GetPaidBillList();
        }

        // Ödenmemiş Fatura Listeleme
        [HttpGet("UnPaidBills")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> GetUnPaidBillList()
        {
            return billService.GetUnPaidBillList();
        }

        // Fatura Guncelleme
        [HttpPut("{id}")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> Update([FromBody] BillViewModel bill, int id)
        {
            return billService.Update(bill, id);
        }


        // Fatura silme
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> Delete(int id)
        {
            return billService.Delete(id);
        }

        // Tek Fatura Atama
        [HttpPost("PostBill")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> PostBill(int id,  decimal price, AssignBillViewModel newBills)
        {
            return billService.PostBill(id, price, newBills);
        }

        // Toplu Fatura Atama
        [HttpPost("AssignTotalBills")]
        [Authorize(Roles = "Administor")]
        public General<BillViewModel> PostTotalBill(decimal totalPrice, AssignBillViewModel newBills)
        {
            return billService.PostTotalBill(totalPrice, newBills);
        }

        // Tek Fatura Odeme
        [HttpPost("PayBill")]
        [Authorize(Roles = "User")]
        public General<BillViewModel> PayBill(string billType, InsertCreditCardModel cardModel)
        {
            return billService.PayBill(billType, cardModel);
        }


        // Toplu Fatura Odeme
        [HttpPost("PayLumpSum")]
        [Authorize(Roles = "User")]
        public General<BillViewModel> PayLumpSum(string billType, InsertCreditCardModel cardModel)
        {
            return billService.PayLumpSum(billType, cardModel);
        }
    }
}
