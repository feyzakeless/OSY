using AutoMapper;
using MongoDB.Driver;
using OSY.Model;
using OSY.Model.ModelCreditCard;
using OSY.Service.ClientServiceLayer;
using System;
using System.Globalization;

namespace OSY.Service.CreditCardServiceLayer
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IMongoCollection<CreditCardViewModel> _creditCards;
        private readonly IMapper mapper;
        public CreditCardService(IDbClient dbClient, IMapper _mapper)
        {
            _creditCards = dbClient.GetCardsCollection();
            mapper = _mapper;
        }


        // MongoDB ye Cart Bilgisi Ekleme
        public General<CreditCardViewModel> AddCreditCard(InsertCreditCardModel card, decimal price)
        {
            var result = new General<CreditCardViewModel>();
            var model = mapper.Map<OSY.DB.MongoDB.MongoEntities.CreditCard>(card);

            DateTime dt = DateTime.ParseExact(card.cardDate, "MM/yy", CultureInfo.InvariantCulture);
            if (dt < DateTime.Now)
            {
                result.IsSuccess = false;    
                return result; 
            }

            model.CreditCardNumber = Extensions.Extension.EncodeBase64(card.CreditCardNumber);
            model.CVC = Extensions.Extension.EncodeBase64(card.CVC);
            model.Price = price;

            var cardModel = mapper.Map<CreditCardViewModel>(model);

            _creditCards.InsertOne(cardModel);
            result.IsSuccess = true;
            return result;
        }
    }
}
