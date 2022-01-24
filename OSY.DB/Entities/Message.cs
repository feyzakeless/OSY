using System;
using System.Collections.Generic;

#nullable disable

namespace OSY.DB.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int RecevierId { get; set; }
        public int SenderId { get; set; }
        public DateTime Idate { get; set; }
        public bool Seen { get; set; }

        public virtual Resident Recevier { get; set; }
        public virtual Resident Sender { get; set; }
    }
}
