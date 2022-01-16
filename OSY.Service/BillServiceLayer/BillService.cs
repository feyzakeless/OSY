using AutoMapper;
using MongoDB.Driver;
using OSY.DB.Entities;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelBill;
using OSY.Model.ModelCreditCard;
using OSY.Service.ApartmentServiceLayer;
using OSY.Service.ClientServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.BillServiceLayer
{
    public class BillService : IBillService
    {
        private readonly IMapper mapper;
        private readonly IApartmentService apartmentService;
        private readonly IMongoCollection<CreditCardViewModel> _creditCards;
        public BillService(IMapper _mapper, IApartmentService _apartmentService, IDbClient dbClient)
        {
            mapper = _mapper;
            apartmentService = _apartmentService;
            _creditCards = dbClient.GetCardsCollection();
        }

        // Fatura Listeleme İslemi
        public General<BillViewModel> GetList()
        {
            var result = new General<BillViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var listedBill = context.Bill.OrderBy(i => i.Id);

                if (listedBill.Any())
                {
                    result.List = mapper.Map<List<BillViewModel>>(listedBill);
                    result.IsSuccess = true;
                    result.SuccessMessage = "Fatura listeleme işlemi başarılı!";
                }
                else
                {
                    result.ExceptionMessage = "Fatura bulunamadi.";
                }

            }

            return result;

        }

        //Ödenmiş Faturaları Listeleme İslemi
        public General<BillViewModel> GetPaidBillList()
        {
            var result = new General<BillViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var listedPaidBill = context.Bill.Where(x => x.IsPaid);

                if (listedPaidBill.Any())
                {
                    result.IsSuccess = true;
                    result.List = mapper.Map<List<BillViewModel>>(listedPaidBill);
                    result.SuccessMessage = "Ödenmiş faturaları listeleme işlemi başarılı!";
                    result.TotalCount = result.List.Count;
                }
                else
                {
                    result.ExceptionMessage = "Hata Oluştu.";
                }
            }

            return result;

        }

        //Ödenmemiş Faturaları Listeleme İslemi
        public General<BillViewModel> GetUnPaidBillList()
        {
            var result = new General<BillViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var listedUnPaidBill = context.Bill.Where(x => !x.IsPaid);

                if (listedUnPaidBill.Any())
                {
                    result.IsSuccess = true;
                    result.List = mapper.Map<List<BillViewModel>>(listedUnPaidBill);
                    result.SuccessMessage = "Ödenmemiş faturaları listeleme işlemi başarılı!";
                    result.TotalCount = result.List.Count;
                }
                else
                {
                    result.ExceptionMessage = "Hata Oluştu.";
                }
            }

            return result;

        }



        // Fatura Guncelleme İslemi
        public General<BillViewModel> Update(BillViewModel bill, int id)
        {

            var result = new General<BillViewModel>() { IsSuccess = false };
            using (var context = new OSYContext())
            {
                var updateBill = context.Bill.SingleOrDefault(i => i.Id == id);

                if (updateBill is not null)
                {
                    updateBill.BillType = bill.BillType;
                    updateBill.IsPaid = bill.IsPaid;
                    updateBill.Price = bill.Price;
                    updateBill.Iapartment = bill.Iapartment;

                    context.SaveChanges();

                    result.Entity = mapper.Map<BillViewModel>(updateBill);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Fatura bulunamadi.";
                }

            }

            return result;
        }

        // Fatura Silme İslemi
        public General<BillViewModel> Delete(int id)
        {
            var result = new General<BillViewModel>();
            using (var context = new OSYContext())
            {
                var deleteBill = context.Bill.SingleOrDefault(i => i.Id == id);

                if (deleteBill is not null)
                {
                    context.Bill.Remove(deleteBill);
                    context.SaveChanges();

                    result.Entity = mapper.Map<BillViewModel>(deleteBill);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Silinecek fatura bulunamadı.";
                    result.IsSuccess = false;
                }
            }
            return result;
        }

        // Toplu Fatura/Aidat Atama İslemi
        public General<BillViewModel> PostBill(decimal totalPrice, AssignBillViewModel newBills)
        {

            var result = new General<BillViewModel>();
            var billModel = mapper.Map<OSY.DB.Entities.Bill>(newBills);

            // Toplam daire sayısı
            var apartmentCount = apartmentService.GetList().TotalCount;

            using (var context = new OSYContext())
            {
                if (newBills.BillType.ToLower() != "dues")
                {
                    // Daire basına düsen fatura ücreti
                    var pricePerApartment = Extensions.Extension.DivideTotalBill(apartmentCount, totalPrice);

                    if (pricePerApartment > 0 && apartmentCount > 0)
                    {
                        // Dolu olan dairelerin listesi
                        var apartmentList = context.Apartment.Where(x => x.IsFull).OrderBy(x => x.Id);

                        //Her bir daireye yeni fatura olusturma
                        foreach (var item in apartmentList)
                        {
                            var bill = new Bill
                            {
                                Iapartment = item.Id,
                                Price = pricePerApartment,
                                BillType = newBills.BillType,
                                IsPaid = newBills.IsPaid,
                                Idate = newBills.Idate
                            };

                            context.Bill.Add(bill);
                        }

                        context.SaveChanges();

                        result.SuccessMessage = newBills.BillType + " faturası oluşturma başarılı!";
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ExceptionMessage = "Hata oluştu !";
                    }
                }
                else 
                {
                    // Aidatları faturalandırma

                    // Tüm Dairelerin Listesi
                    var apartmentList = apartmentService.GetList().List;

                    // Aylık olarak tüm sitenin ödeyeceği toplam aidat tutarı
                    var monthlyTotalPrice = totalPrice / 12;

                    // Daire basına düsen aidat ücreti
                    var pricePerApartment = Extensions.Extension.DivideTotalBill(apartmentCount, monthlyTotalPrice);

                    decimal monthlyPrice = 0;

                    /*int count1 = 0;
                    int count2 = 0;
                    int count3 = 0;

                    foreach (var item in apartmentList)
                    {
                        if (item.ApartmentType == "2+1")
                        {
                            count1++;
                        }
                        else if (item.ApartmentType == "3+1")
                        {
                            count2++;
                        }
                        else if (item.ApartmentType == "4+1")
                        {
                            count3++;
                        }
                    }

                    var lcm = Extensions.Extension.LCM(count1, Extensions.Extension.LCM(count2, count3));*/

                    if (pricePerApartment > 0)
                    {

                        foreach (var item in apartmentList)
                        {

                            if (item.ApartmentType == "2+1")
                            {
                                monthlyPrice = pricePerApartment - 100;
                            }

                            else if (item.ApartmentType == "3+1")
                            {
                                monthlyPrice = pricePerApartment;
                            }

                            else if (item.ApartmentType == "4+1")
                            {
                                monthlyPrice = pricePerApartment + 100;
                            }


                            if (monthlyPrice > 0)
                            {
                                var bill = new Bill
                                {
                                    Iapartment = item.Id,
                                    Price = monthlyPrice,
                                    BillType = newBills.BillType,
                                    IsPaid = newBills.IsPaid,
                                    Idate = newBills.Idate
                                };

                                context.Bill.Add(bill);
                            }
                            else
                            {
                                result.ExceptionMessage = "Lütfen girdiğiniz tutarı kontrol ediniz !";
                                return result;
                            }

                        }
                        context.SaveChanges();

                        result.SuccessMessage = "Dues(aidat) faturası oluşturma başarılı!";
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen girdiğiniz tutarı kontrol ediniz !";
                        result.IsSuccess = false;
                    }
                    
                }

                return result;

            }

        }

        // MongoDB ye Cart Bilgisi Ekleme
        public CreditCardViewModel AddCard(CreditCardViewModel card)
        {
            _creditCards.InsertOne(card);
            return card;
        }

        // Toplu Ödeme İslemi
        public General<BillViewModel> PayLumpSum(string billType, CreditCardViewModel cardModel)
        {
            var result = new General<BillViewModel>();

            using (var context = new OSYContext())
            {
                var billList = context.Bill.Where(x => x.Iapartment == cardModel.ApartmentId && x.BillType == billType && !x.IsPaid);

                decimal totalUnPaid = 0;

                foreach (var item in billList)
                {
                    totalUnPaid += item.Price;


                    if (totalUnPaid > 0)
                    {
                        AddCard(cardModel);
                        item.IsPaid = true;
                        result.SuccessMessage = "Toplu ödeme başarılı!";
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ExceptionMessage = "Ödeme başarısız, lütfen bilgileri kontrol edin!";
                    }

                }

                context.SaveChanges();

            }

            return result;
        }


    }
}
