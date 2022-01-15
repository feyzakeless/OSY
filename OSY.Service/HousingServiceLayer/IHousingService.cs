using OSY.Model;
using OSY.Model.ModelHousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.HousingServiceLayer
{
    public interface IHousingService
    {
        public General<HousingViewModel> Insert(HousingViewModel newHousing);
        public General<HousingViewModel> GetList();
        public General<HousingViewModel> Update(HousingViewModel housing, int id);
        public General<HousingViewModel> Delete(int id);
    }
}
