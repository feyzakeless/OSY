using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelCreditCard
{
    public class CreditCardViewModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public int ApartmentId { get; set; }
        public long CreditCardNumber { get; set; }
        public int CVV{ get; set; }
    }
}
