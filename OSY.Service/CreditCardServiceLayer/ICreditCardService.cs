using OSY.Model;
using OSY.Model.ModelCreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.CreditCardServiceLayer
{
    public interface ICreditCardService
    {
        public General<CreditCardViewModel> AddCreditCard(InsertCreditCardModel card, decimal price);
    }
}
