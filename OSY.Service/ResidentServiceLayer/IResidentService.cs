using OSY.Model;
using OSY.Model.ModelLogin;
using OSY.Model.ModelResident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.ResidentServiceLayer
{
    public interface IResidentService
    {
        public General<ResidentViewModel> Insert(ResidentViewModel newResident);
        public General<RegisterResidentViewModel> InsertForUser(RegisterResidentViewModel newResident);
        public General<ResidentViewModel> Login(LoginViewModel loginResident);
        public General<ResidentViewModel> GetResidents();
        public General<ResidentViewModel> Update(int id, ResidentViewModel resident);
        public General<ResidentViewModel> Delete(int id);
    }
}
