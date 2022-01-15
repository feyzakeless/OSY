using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Housing
    {
        public Housing()
        {
            Apartment = new HashSet<Apartment>();
        }

        public int Id { get; set; }
        public string BlokName { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
    }
}
