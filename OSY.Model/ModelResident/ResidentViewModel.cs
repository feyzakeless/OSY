using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelResident
{
    public class ResidentViewModel
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
        public bool IsSendWelcomeMail { get; set; }
    }
}
