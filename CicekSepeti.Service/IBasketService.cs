using CicekSepeti.Domain.Models;
using System.Threading.Tasks;

namespace CicekSepeti.Service
{
    public interface IBasketService
    {
        Task<ServiceResult> AddBasketProduct(ProductBasket productBasket);
    }
}
