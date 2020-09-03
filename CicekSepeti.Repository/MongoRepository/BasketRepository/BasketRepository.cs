using CicekSepeti.Domain.MongoDomain;
using CicekSepeti.Repository.MongoRepository.BasketRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
   public class BasketRepository:IBasketRepository
    {
        public int Sum(List<Basket> baskets) => baskets.Sum(x => x.ProductPiece);
    }
}
