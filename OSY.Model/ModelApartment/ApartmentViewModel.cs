using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelApartment
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public int BlokId { get; set; }
        public int ApartmentNo { get; set; }
        public string ApartmentType { get; set; }
        public bool IsFull { get; set; }
    }
}
