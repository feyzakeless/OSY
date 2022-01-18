using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelResident
{
    public class ResidentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Alanı Boş Olamaz !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim Alanı Boş Olamaz !")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email Alanı Boş Olamaz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola Alanı Boş Olamaz !")]
        [StringLength(8, ErrorMessage = "Parola 8 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "TC Alanı Boş Olamaz !")]
        public string IdentityNo { get; set; }

        [Required(ErrorMessage = "Telefon Alanı Boş Olamaz !")]
        public string PhoneNumber { get; set; }
        public string PlateNo { get; set; }

        [Required(ErrorMessage = "Daire Alanı Boş Olamaz !")]
        public int ApartId { get; set; }

        [Required(ErrorMessage = "Yetki Alanı Boş Olamaz !")]
        public string IsAdmin { get; set; }
        public bool IsSendWelcomeMail { get; set; }
    }
}
