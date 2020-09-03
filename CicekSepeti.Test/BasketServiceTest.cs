using AutoFixture.Xunit2;
using CicekSepeti.Domain.Models;
using CicekSepeti.Domain.MongoDomain;
using CicekSepeti.Domain.SQLDomain;
using CicekSepeti.Repository;
using CicekSepeti.Repository.MongoRepository;
using CicekSepeti.Repository.MongoRepository.BasketRepository;
using CicekSepeti.Repository.MSSQLRepository;
using CicekSepeti.Service;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace CicekSepeti.Test
{
    public class BasketServiceTest
    {

        public BasketServiceTest()
        {
            AuthUser authUser = new AuthUser();

            authUser.RequestIp = "198.168.15.20";

            AuthUser.Current = authUser;

        }

        [Theory, AutoMoqData]
        [Obsolete]
        public async Task AddBasketProduct_Success_Product_add_to_basket([Frozen]Mock<ISQLRepository<Product>> _productRepository,
           [Frozen]Mock<ISQLRepository<Stock>> _stockRepository, [Frozen]Mock<IMongoRepository<Basket>> _mongoRepository,
            [Frozen]Mock<IRedisRepository> redisRepository, [Frozen]Mock<IBasketRepository> _basketRepository,Stock stock,ProductBasket productBasket,BasketService basketService,List<Basket> basket,Product product)
        {

            //Arrange
            productBasket.ProductPiece = 2;
            stock.MaxPiece = 3;
            stock.Piece = 4;


            _stockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(stock);

             _mongoRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Basket,object>>>(),It.IsAny<object>()))
                .ReturnsAsync(basket);

            _basketRepository.Setup(x => x.Sum(basket)).Returns(1);



            basketService = new BasketService(_mongoRepository.Object, _productRepository.Object, _stockRepository.Object, redisRepository.Object,_basketRepository.Object);

            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);


            redisRepository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            //Act
            var act = await basketService.AddBasketProduct(productBasket);

            //Assert
            _mongoRepository.Verify(x => x.AddAsync(It.IsAny<Basket>()),Times.Once);

            redisRepository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()),Times.Once);

            act.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        [Theory, AutoMoqData]
        [Obsolete]
        public void AddBasketProduct_Failed_StockPiece_Is_More_Than_The_Piece_Of_Product([Frozen] Mock<ISQLRepository<Product>> _productRepository,
          [Frozen] Mock<ISQLRepository<Stock>> _stockRepository, [Frozen] Mock<IMongoRepository<Basket>> _mongoRepository,
           [Frozen] Mock<IRedisRepository> redisRepository, [Frozen] Mock<IBasketRepository> _basketRepository, Stock stock, ProductBasket productBasket, BasketService basketService, List<Basket> basket)
        {

            //Arrange
            productBasket.ProductPiece = 3;
            stock.MaxPiece = 3;
            stock.Piece = 2;


            _stockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(stock);


            basketService = new BasketService(_mongoRepository.Object, _productRepository.Object, _stockRepository.Object, redisRepository.Object, _basketRepository.Object);

            //Act
            Func<Task> act = async () =>
            {
                await basketService.AddBasketProduct(productBasket);
            };

            //Assert
            _mongoRepository.Verify(x => x.GetAllAsync(It.IsAny<Expression<Func<Basket, object>>>(), It.IsAny<object>()), Times.Never);


            _mongoRepository.Verify(x => x.AddAsync(It.IsAny<Basket>()), Times.Never);

            _basketRepository.Verify(x => x.Sum(basket),Times.Never);

            _productRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()),Times.Never);

            redisRepository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never);


            act.Should().Throw<Exception>().WithMessage("Ürün stokta bulunmamaktadır.");


        }

        [Theory, AutoMoqData]
        [Obsolete]
        public void AddBasketProduct_Failed_Stock_Maxpiece_Is_More_Than_The_Piece_Of_Product([Frozen] Mock<ISQLRepository<Product>> _productRepository,
          [Frozen] Mock<ISQLRepository<Stock>> _stockRepository, [Frozen] Mock<IMongoRepository<Basket>> _mongoRepository,
           [Frozen] Mock<IRedisRepository> redisRepository, [Frozen] Mock<IBasketRepository> _basketRepository, Stock stock, ProductBasket productBasket, BasketService basketService, List<Basket> basket, Product product)
        {

            //Arrange
            productBasket.ProductPiece = 2;
            stock.MaxPiece = 1;
            stock.Piece = 3;


            _stockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(stock);


            basketService = new BasketService(_mongoRepository.Object, _productRepository.Object, _stockRepository.Object, redisRepository.Object, _basketRepository.Object);

            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            //Act
            Func<Task> act = async () =>
            {
                await basketService.AddBasketProduct(productBasket);
            };

            //Assert
            _mongoRepository.Verify(x => x.GetAllAsync(It.IsAny<Expression<Func<Basket, object>>>(), It.IsAny<object>()), Times.Never);


            _mongoRepository.Verify(x => x.AddAsync(It.IsAny<Basket>()), Times.Never);

            _basketRepository.Verify(x => x.Sum(basket), Times.Never);

            _productRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);

            redisRepository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never);


            act.Should().Throw<Exception>().WithMessage("Maksimum Ürün limitini aştınız.");

        }


        [Theory, AutoMoqData]
        [Obsolete]
        public void AddBasketProduct_Failed_Item_In_The_Basket_Is_Out_Of_Stock([Frozen] Mock<ISQLRepository<Product>> _productRepository,
          [Frozen] Mock<ISQLRepository<Stock>> _stockRepository, [Frozen] Mock<IMongoRepository<Basket>> _mongoRepository,
           [Frozen] Mock<IRedisRepository> redisRepository, [Frozen] Mock<IBasketRepository> _basketRepository, Stock stock, ProductBasket productBasket, BasketService basketService, List<Basket> basket)
        {

            //Arrange
            productBasket.ProductPiece = 2;
            stock.MaxPiece = 3;
            stock.Piece = 4;


            _stockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(stock);

            _mongoRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Basket, object>>>(), It.IsAny<object>()))
               .ReturnsAsync(basket);

            _basketRepository.Setup(x => x.Sum(basket)).Returns(3);


            basketService = new BasketService(_mongoRepository.Object, _productRepository.Object, _stockRepository.Object, redisRepository.Object, _basketRepository.Object);

            //Act
            Func<Task> act = async () =>
            {
                await basketService.AddBasketProduct(productBasket);
            };


            //Assert
            _productRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);

            redisRepository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never);


            act.Should().Throw<Exception>().WithMessage("Sepete Stokta bulunandan fazla veya Maksimum Ekleme limitini aştınız.");

            

        }
    }
}
