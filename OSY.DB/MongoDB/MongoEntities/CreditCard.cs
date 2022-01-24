using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OSY.DB.MongoDB.MongoEntities
{
    public class CreditCard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("apartmentId")]
        public int ApartmentId { get; set; }

        [BsonElement("creditCardNumber")]
        public string CreditCardNumber { get; set; }

        [BsonElement("cvc")]
        public string CVC { get; set; }

        [BsonElement("cardDate")]
        public string cardDate { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

    }
}
