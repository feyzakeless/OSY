using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Model.ModelCreditCard
{
    public class InsertCreditCardModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }

        public int ApartmentId { get; set; }

        public string CreditCardNumber { get; set; }

        public string CVC { get; set; }

        public string cardDate { get; set; }
    }
}
