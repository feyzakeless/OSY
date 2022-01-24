using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelMessage
{
    public class SendMessageViewModel
    {
        public string MessageText { get; set; }
        public int RecevierId { get; set; }
        public int SenderId { get; set; }
    }
}
