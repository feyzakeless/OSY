using AutoMapper;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelMessage;
using OSY.Service.ResidentServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.MessageServiceLayer
{
    public class MessageService : IMessageService
    {
        private readonly IResidentService residentService;
        private readonly IMapper mapper;
        public MessageService(IResidentService _residentService, IMapper _mapper)
        {
            residentService = _residentService;
            mapper = _mapper;
        }


        public General<MessageViewModel> SendMessage(SendMessageViewModel message)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };
            var model = mapper.Map<DB.Entities.Message>(message);

            using (var context = new OSYContext())
            {
                model.Seen = false;
                model.Idate = DateTime.Now;
                context.Message.Add(model);
                context.SaveChanges();

                result.Entity = mapper.Map<MessageViewModel>(model);
                result.SuccessMessage = "Mesaj gönderildi.";
                result.IsSuccess = true;
            }

            return result;
        }

        public General<MessageViewModel> GetMessage(int id)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };
           
            using (var context = new OSYContext())
            {
                var check = context.Message.Where(x => x.RecevierId == id).OrderByDescending(x => x.Idate).ToList()
                    .GroupBy(x => x.SenderId).Select(x => x.First());

                if (!check.Any())
                {
                    check = context.Message.Where(x => x.SenderId == id).OrderByDescending(x => x.Idate).ToList()
                    .GroupBy(x => x.RecevierId).Select(x => x.First());

                    if (!check.Any())
                    {
                        result.ExceptionMessage = "Hiçbir mesaj bulunmadı.";
                        return result;
                    }
                }

                result.List = mapper.Map<List<MessageViewModel>>(check);
                result.IsSuccess = true;

            }
            return result;
        }

        public General<MessageViewModel> GetListRelatedToAnyChat(int senderId , int receiverId)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            using (var context = new OSYContext())
            {
                var messages = context.Message.Where(x => (x.SenderId == senderId && x.RecevierId == receiverId) ||
                (x.SenderId == receiverId && x.RecevierId == senderId)).OrderBy(x => x.Idate);

                if (!messages.Any())
                {
                    result.ExceptionMessage = "Konuşma bulunamadı.";
                    return result;
                }


                foreach (var message in messages)
                    message.Seen = true;

                context.SaveChanges();
                result.List = mapper.Map<List<MessageViewModel>>(messages);
                result.IsSuccess = true;
            }

            return result;
        }

    }
}
