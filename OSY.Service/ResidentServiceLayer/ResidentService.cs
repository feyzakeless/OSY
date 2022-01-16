using AutoMapper;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelLogin;
using OSY.Model.ModelResident;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSY.Service.ResidentServiceLayer
{
    public class ResidentService : IResidentService
    {
        private readonly IMapper mapper;
        public ResidentService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        // Daire Sakini Kayıt İslemi (Admin)
        public General<ResidentViewModel> Insert(ResidentViewModel newResident)
        {
            var result = new General<ResidentViewModel>() { IsSuccess = false };
            try
            {
                var model = mapper.Map<OSY.DB.Entities.Resident>(newResident);
                using (var osy = new OSYContext())
                {
                    model.Idate = DateTime.Now;
                    model.IsActive = true;
                    osy.Resident.Add(model);
                    osy.SaveChanges();
                    result.Entity = mapper.Map<ResidentViewModel>(model);
                    result.IsSuccess = true;
                }
            }
            catch (Exception)
            {

                result.ExceptionMessage = "Kayıt işlemi gerçekleşmedi.";
            }

            return result;
        }

        // Daire Sakini Kayıt İslemi (Kullanıcı)
        public General<RegisterResidentViewModel> InsertForUser(RegisterResidentViewModel newResident)
        {
            var result = new General<RegisterResidentViewModel>() { IsSuccess = false };

            using (var osy = new OSYContext())
            {
                var userCheck = osy.Resident.Any(x => x.Name == newResident.Name &&  x.Surname == newResident.Surname
                    && x.Email == newResident.Email && x.IdentityNo == newResident.IdentityNo);
                var userInfo = osy.Resident.FirstOrDefault(x => x.Name == newResident.Name && x.Surname == newResident.Surname
                    && x.Email == newResident.Email && x.IdentityNo == newResident.IdentityNo);
                if (userCheck)
                {
                    result.ExceptionMessage = "Kayıt işleminiz gerçekleşmiştir. Uygulama şifreniz mailinize iletilmiştir.";
                    Console.WriteLine("Sayın "+userInfo.Name+"<br> Online Site Yönetimine Hoşgeldiniz ! <br>"+"Sisteme Giriş Şifreniz : "+userInfo.Password);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Kullanıcı bulunamadı. Lütfen site yönetimi ile iletişime geçiniz.";
                    result.IsSuccess = false;
                }
                
            }

            return result;
        }

        // Daire Sakini Login
        public General<ResidentViewModel> Login(LoginViewModel loginResident)
        {
            General<ResidentViewModel> result = new();
            using (var osy = new OSYContext())
            {
                //FirstOrDefault; bana bir tane değer gönder ve yoksa da hata verme.
                var _data = osy.Resident.FirstOrDefault(a => !a.IsDelete && a.IsActive && a.Email == loginResident.Email && a.Password ==
                loginResident.Password);

                if (_data is not null)
                {
                    result.IsSuccess = true;
                    result.Entity = mapper.Map<ResidentViewModel>(_data);
                }

            }
            return result;
        }


        // Daire Sakini Listeleme
        public General<ResidentViewModel> GetResidents()
        {
            var result = new General<ResidentViewModel>();

            using (var context = new OSYContext())
            {
                var data = context.Resident
                    .Where(x => !x.IsDelete && x.IsActive)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<ResidentViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir daire sakini bulunamadı.";
                }
            }

            return result;
        }

        //Daire Sakini Guncelleme İslemi
        public General<ResidentViewModel> Update(int id, ResidentViewModel resident)
        {
            var result = new General<ResidentViewModel>();

            using (var context = new OSYContext())
            {
                var updateResident = context.Resident.SingleOrDefault(i => i.Id == id);

                if (updateResident is not null)
                {
                    updateResident.Name = resident.Name;
                    updateResident.Surname = resident.Surname;
                    updateResident.PhoneNumber = resident.PhoneNumber;
                    updateResident.PlateNo = resident.PlateNo;
                    updateResident.ApartId = resident.ApartId;
                    //updateResident.Email = user.Email;
                    //updateUser.Password = user.Password;


                    context.SaveChanges();

                    result.Entity = mapper.Map<ResidentViewModel>(updateResident);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu.";
                }
            }

            return result;
        }

        // Daire Sakini Silme İslemi
        public General<ResidentViewModel> Delete(int id)
        {
            var result = new General<ResidentViewModel>();

            using (var context = new OSYContext())
            {
                var resident = context.Resident.SingleOrDefault(i => i.Id == id);

                if (resident is not null)
                {
                    context.Resident.Remove(resident); 
                    //Kullanıcıyı tamamamen silmemek için IDelete columnu true yapıldı.
                    //resident.IsDelete = true;
                    //resident.IsActive = false;
                    context.SaveChanges();

                    result.Entity = mapper.Map<ResidentViewModel>(resident);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Kullanıcı bulunamadı.";
                    result.IsSuccess = false;
                }
            }

            return result;
        }

    }
}
