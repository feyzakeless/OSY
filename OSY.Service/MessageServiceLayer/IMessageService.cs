using OSY.Model;
using OSY.Model.ModelMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.MessageServiceLayer
{
    public interface IMessageService
    {
        public General<MessageViewModel> SendMessage(SendMessageViewModel message);
        public General<MessageViewModel> GetMessage(int id);
        public General<MessageViewModel> GetListRelatedToAnyChat(int senderId, int receiverId);
    }
}
