using MongoDB.Driver;
using OSY.Model.ModelCreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.ClientServiceLayer
{
    public interface IDbClient
    {
        IMongoCollection<CreditCardViewModel> GetCardsCollection();
    }
}
