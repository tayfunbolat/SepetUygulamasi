using CicekSepeti.Domain.MongoDomain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository.MongoRepository.BasketRepository
{
    public interface IBasketRepository
    {
        int Sum(List<Basket> baskets);
    }
}
