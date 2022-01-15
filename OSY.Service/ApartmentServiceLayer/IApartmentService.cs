using OSY.Model;
using OSY.Model.ModelApartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.ApartmentServiceLayer
{
    public interface IApartmentService
    {
        public General<ApartmentViewModel> Insert(ApartmentViewModel newApartment);
        public General<ApartmentViewModel> GetList();
        public General<ApartmentViewModel> Update(ApartmentViewModel apartment, int id);
        public General<ApartmentViewModel> Delete(int id);
    }
}
