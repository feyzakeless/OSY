using AutoMapper;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelApartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.ApartmentServiceLayer
{
    public class ApartmentService : IApartmentService
    {
        private readonly IMapper mapper;
        public ApartmentService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public General<ApartmentViewModel> Insert(ApartmentViewModel newApartment)
        {
            var result = new General<ApartmentViewModel>() { IsSuccess = false };

            try
            {
                var apartmentModel = mapper.Map<OSY.DB.Entities.Apartment>(newApartment);
                using (var context = new OSYContext())
                {
                    
                    //apartmentModel.Idate = DateTime.Now;
                    context.Apartment.Add(apartmentModel);
                    context.SaveChanges();
                    
                    result.Entity = mapper.Map<ApartmentViewModel>(apartmentModel);
                    result.IsSuccess = true;
                }
            }
            catch (System.Exception)
            {
                result.ExceptionMessage = "Daire ekleme gerçekleşmedi.";
            }

            return result;
        }

        // Daire Listeleme İslemi
        public General<ApartmentViewModel> GetList()
        {
            var result = new General<ApartmentViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var listedApartment = context.Apartment.OrderBy(i => i.Id);

                if (listedApartment.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(listedApartment);
                    result.TotalCount = result.List.Count;
                    result.SuccessMessage = "Daire listeleme işlemi başarılı!";
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Daire bulunamadi.";
                }

            }

            return result;

        }


        // Daire Guncelleme İslemi
        public General<ApartmentViewModel> Update(ApartmentViewModel apartment, int id)
        {

            var result = new General<ApartmentViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var updateApartment = context.Apartment.SingleOrDefault(i => i.Id == id);
                if (updateApartment is not null)
                {
                    updateApartment.BlokId = apartment.BlokId;
                    updateApartment.ApartmentNo = apartment.ApartmentNo;
                    updateApartment.ApartmentType = apartment.ApartmentType;
                    updateApartment.IsFull = apartment.IsFull;

                    context.SaveChanges();

                    result.Entity = mapper.Map<ApartmentViewModel>(updateApartment);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Daire bulunamadi.";
                }
            }

            return result;
        }

        // Daire Silme İslemi
        public General<ApartmentViewModel> Delete(int id)
        {
            var result = new General<ApartmentViewModel>();
            using (var context = new OSYContext())
            {
                var deleteApartment = context.Apartment.SingleOrDefault(i => i.Id == id);

                if (deleteApartment is not null)
                {
                    context.Apartment.Remove(deleteApartment);
                    context.SaveChanges();

                    result.Entity = mapper.Map<ApartmentViewModel>(deleteApartment);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Silinecek daire bulunamadı.";
                    result.IsSuccess = false;
                }
            }
            return result;
        }
    }
}
