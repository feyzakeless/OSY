using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelCreditCard
{
    public class CreditCardViewModel
    {
        public int BillId { get; set; }
        public long CreditCardNumber { get; set; }
        public int CVV{ get; set; }
    }
}
