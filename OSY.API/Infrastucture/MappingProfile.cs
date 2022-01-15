﻿using AutoMapper;
using OSY.Model.ModelApartment;
using OSY.Model.ModelBill;
using OSY.Model.ModelHousing;
using OSY.Model.ModelResident;

namespace OSY.API.Infrastucture
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Kullanici(Daire Sakini) Mapping
            CreateMap<ResidentViewModel, OSY.DB.Entities.Resident>();
            CreateMap<OSY.DB.Entities.Resident, ResidentViewModel>();

            //Blok Mapping
            CreateMap<HousingViewModel, OSY.DB.Entities.Housing>();
            CreateMap<OSY.DB.Entities.Housing, HousingViewModel>();

            //Daire Mapping
            CreateMap<ApartmentViewModel, OSY.DB.Entities.Apartment>();
            CreateMap<OSY.DB.Entities.Apartment, ApartmentViewModel>();

            //Fatura Mapping
            CreateMap<BillViewModel, OSY.DB.Entities.Bill>();
            CreateMap<OSY.DB.Entities.Bill, BillViewModel>();
        }
    }
}