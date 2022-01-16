using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelBill
{
    public class AssignBillViewModel
    {
        public string BillType { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Idate { get; set; }
    }
}
