using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelLogin
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Alanı Boş Olamaz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola Alanı Boş Olamaz !")]
        public string Password { get; set; }
    }
}
