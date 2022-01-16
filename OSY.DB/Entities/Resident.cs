using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Resident
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IdentityNo { get; set; }
        public string PhoneNumber { get; set; }
        public string PlateNo { get; set; }
        public int ApartId { get; set; }
        public string IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Idate { get; set; }
        public DateTime? Udate { get; set; }
        public bool IsSendWelcomeMail { get; set; }

        public virtual Apartment Apart { get; set; }
    }
}
