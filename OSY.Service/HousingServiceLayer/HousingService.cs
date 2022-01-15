using AutoMapper;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelHousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.HousingServiceLayer
{
    public class HousingService : IHousingService
    {
        private readonly IMapper mapper;
        public HousingService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        // Blok(Bina) Ekleme İslemi
        public General<HousingViewModel> Insert(HousingViewModel newHousing)
        {
            var result = new General<HousingViewModel>() { IsSuccess = false };

            try
            {
                var housingModel = mapper.Map<OSY.DB.Entities.Housing>(newHousing);

                using (var context = new OSYContext())
                {
                    //var isChecked = context.Resident.Any(a => a.ApartId == housingModel.Id); //Daire Kontrolu

                    /*if (isChecked)
                    {*/
                        //apartmentModel.Idate = DateTime.Now;
                        context.Housing.Add(housingModel);
                        context.SaveChanges();
                   /* }
                    else
                    {
                        result.ExceptionMessage = "Ekleme işlemi için yetkiniz bulunmamaktadır.";
                    }*/



                    result.Entity = mapper.Map<HousingViewModel>(housingModel);
                    result.IsSuccess = true;
                }
            }
            catch (System.Exception)
            {
                result.ExceptionMessage = "Blok ekleme gerçekleşmedi.";
            }

            return result;
        }

        // Blok(Bina) Listeleme İslemi
        public General<HousingViewModel> GetList()
        {
            var result = new General<HousingViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var listedHousing = context.Housing.OrderBy(i => i.Id);

                if (listedHousing.Any())
                {
                    result.List = mapper.Map<List<HousingViewModel>>(listedHousing);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Blok bulunamadi.";
                }

            }

            return result;

        }

        // Blok(Bina) Guncelleme İslemi
        public General<HousingViewModel> Update(HousingViewModel housing, int id)
        {

            var result = new General<HousingViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var updateHousing = context.Housing.SingleOrDefault(i => i.Id == id);

                if (updateHousing is not null)
                {
                    updateHousing.BlokName = housing.BlokName;

                    context.SaveChanges();

                    result.Entity = mapper.Map<HousingViewModel>(updateHousing);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Blok bulunamadi.";
                }

                
            }

            return result;
        }

        // Blok(Bina) Silme İslemi
        public General<HousingViewModel> Delete(int id)
        {
            var result = new General<HousingViewModel>();
            using (var context = new OSYContext())
            {
                var deleteHousing = context.Housing.SingleOrDefault(i => i.Id == id);

                if (deleteHousing is not null)
                {
                    context.Housing.Remove(deleteHousing);
                    context.SaveChanges();

                    result.Entity = mapper.Map<HousingViewModel>(deleteHousing);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Silinecek blok bulunamadı.";
                    result.IsSuccess = false;
                }
            }
            return result;
        }
    }
}
