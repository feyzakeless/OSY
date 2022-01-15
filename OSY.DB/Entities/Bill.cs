using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Paid { get; set; }
        public decimal UnPaid { get; set; }
        public decimal TotalDept { get; set; }
        public int Iapartment { get; set; }

        public virtual Apartment IapartmentNavigation { get; set; }
    }
}
