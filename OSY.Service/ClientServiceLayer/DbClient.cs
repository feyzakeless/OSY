using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OSY.Model.ModelCreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.ClientServiceLayer
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<CreditCardViewModel> _creditCards;
        public DbClient(IOptions<PaymentDBConfig> paymentDbConfig)
        {
            var client = new MongoClient(paymentDbConfig.Value.Connection_String);
            var database = client.GetDatabase(paymentDbConfig.Value.Database_Name);
            _creditCards = database.GetCollection<CreditCardViewModel>(paymentDbConfig.Value.Payments_Collection_Name);
        }

        IMongoCollection<CreditCardViewModel> IDbClient.GetCardsCollection() => _creditCards;
    }
}
