using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Apartment
    {
        public Apartment()
        {
            Bill = new HashSet<Bill>();
            Resident = new HashSet<Resident>();
        }

        public int Id { get; set; }
        public int BlokId { get; set; }
        public int ApartmentNo { get; set; }
        public string ApartmentType { get; set; }
        public bool IsFull { get; set; }

        public virtual Housing Blok { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
        public virtual ICollection<Resident> Resident { get; set; }
    }
}
