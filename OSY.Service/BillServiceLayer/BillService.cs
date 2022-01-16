using AutoMapper;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelBill;
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
        public BillService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        // Fatura Ekleme İslemi
        public General<BillViewModel> Insert(BillViewModel newBill)
        {
            var result = new General<BillViewModel>() { IsSuccess = false };

            try
            {
                var billModel = mapper.Map<OSY.DB.Entities.Bill>(newBill);
                using (var context = new OSYContext())
                {

                    context.Bill.Add(billModel);
                    context.SaveChanges();


                    result.Entity = mapper.Map<BillViewModel>(billModel);
                    result.IsSuccess = true;
                }
            }
            catch (System.Exception)
            {
                result.ExceptionMessage = "Daire ekleme gerçekleşmedi.";
            }

            return result;
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

    }


}
