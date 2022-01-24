using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelResident
{
    public class RegisterResidentViewModel
    {
        [Required(ErrorMessage = "İsim Alanı Boş Olamaz !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim Alanı Boş Olamaz !")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Soyisim Alanı Boş Olamaz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "TC Alanı Boş Olamaz !")]
        public string IdentityNo { get; set; }
        public string PhoneNumber { get; set; }
    }
}
