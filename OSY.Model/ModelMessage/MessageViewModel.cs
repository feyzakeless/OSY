using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelMessage
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int RecevierId { get; set; }
        public int SenderId { get; set; }
        public DateTime Idate { get; set; }
        public bool Seen { get; set; }
    }
}
