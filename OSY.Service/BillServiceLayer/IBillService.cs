using OSY.Model;
using OSY.Model.ModelBill;
using OSY.Model.ModelCreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.BillServiceLayer
{
    public interface IBillService
    {
        public General<BillViewModel> GetList();
        public General<BillViewModel> GetPaidBillList();
        public General<BillViewModel> GetUnPaidBillList();
        public General<BillViewModel> Update(BillViewModel medicine, int id);
        public General<BillViewModel> Delete(int id);
        public General<BillViewModel> PostBill(decimal totalPrice, AssignBillViewModel newBills);
        public General<BillViewModel> PayLumpSum(string type, CreditCardViewModel cardModel);
    }
}
