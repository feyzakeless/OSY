using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public int Iapartment { get; set; }

        public virtual Apartment IapartmentNavigation { get; set; }
    }
}
