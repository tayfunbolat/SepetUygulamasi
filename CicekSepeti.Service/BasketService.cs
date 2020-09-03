using CicekSepeti.Domain.Models;
using CicekSepeti.Domain.MongoDomain;
using CicekSepeti.Domain.SQLDomain;
using CicekSepeti.Repository;
using CicekSepeti.Repository.MongoRepository;
using CicekSepeti.Repository.MongoRepository.BasketRepository;
using CicekSepeti.Repository.MSSQLRepository;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CicekSepeti.Service
{
    public class BasketService:IBasketService
    {
        private readonly ISQLRepository<Product> _productRepository;
        private readonly ISQLRepository<Stock> _stockRepository;
        private readonly IMongoRepository<Basket> _mongoRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IBasketRepository _basketRepository;
        public BasketService(IMongoRepository<Basket> mongoRepository,
           ISQLRepository<Product> productRepository,
           ISQLRepository<Stock> stockRepository,
           IRedisRepository redisRepository,
           IBasketRepository basketRepository)
         {
            _basketRepository = basketRepository;
            _mongoRepository = mongoRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _redisRepository = redisRepository;

        }

        public async  Task<ServiceResult> AddBasketProduct(ProductBasket productBasket)
        {
            try
            {
                var serviceResult = new ServiceResult();

                var stockDmo = await _stockRepository.GetByIdAsync(x => x.ProductId == productBasket.ProductId);

                if (productBasket.ProductPiece > stockDmo.Piece)
                    throw new System.Exception("Ürün stokta bulunmamaktadır.");

                if (productBasket.ProductPiece > stockDmo.MaxPiece)
                    throw new System.Exception("Maksimum Ürün limitini aştınız.");


                var filterBasketList = await _mongoRepository.GetAllAsync(x => x.Product.Id, productBasket.ProductId);

                var basketProductSum = _basketRepository.Sum(filterBasketList);

                if (basketProductSum + productBasket.ProductPiece > stockDmo.MaxPiece || basketProductSum + productBasket.ProductPiece > stockDmo.Piece)
                    throw new Exception("Sepete Stokta bulunandan fazla veya Maksimum Ekleme limitini aştınız.");

                var productDmo = await _productRepository.GetByIdAsync(productBasket.ProductId);

                var basket = new Basket
                {
                    ProductPiece = productBasket.ProductPiece,
                    Product = new Product
                    {
                        Id = productDmo.Id,
                        Name = productDmo.Name,
                        Price = productDmo.Price
                    },
                    UserIpAdress = AuthUser.Current.RequestIp
                };

                await _mongoRepository.AddAsync(basket);

                var data = JsonConvert.SerializeObject(basket);

                await _redisRepository.Add(AuthUser.Current.RequestIp, data);

                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Sepete ekleme başarılı"
                };

            }
            catch (Exception ex)
            {
                throw new System.Exception($"{ex.Message}");
            }
             
        }
    }
}
