using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelHousing
{
    public class HousingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Blok Alanı Boş Olamaz!")]
        public string BlokName { get; set; }
    }
}
