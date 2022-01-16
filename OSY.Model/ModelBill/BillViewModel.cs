using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelBill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public int Iapartment { get; set; }
    }
}
