using Microsoft.AspNetCore.Mvc;
using OSY.Model.ModelCreditCard;
using OSY.Service.CreditCardServiceLayer;

namespace OSY.API.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService creditCardService;
        public CreditCardController(ICreditCardService _creditCardService)
        {
            creditCardService = _creditCardService;
        }

        [HttpPost]
        public IActionResult AddCreditCard(InsertCreditCardModel card, decimal price)
        {
            creditCardService.AddCreditCard(card , price);
            return Ok(card);
        }
    }
}
