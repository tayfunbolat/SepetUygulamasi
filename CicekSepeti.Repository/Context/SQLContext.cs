using CicekSepeti.Domain.SQLDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CicekSepeti.Repository
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {
            LoadData();
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        private void LoadData()
        {
            List<Product> products = new List<Product>
            {
                new Product
                {
                    Id = new Guid("f647f6be-15b1-4927-8e1c-bddb8663cb13"),
                Name = "Iphone11",
                Price = 7000M
                },
                new Product
                {
                    Id = new Guid("203a7848-2fbb-4010-8ca3-edc7001d2843"),
                    Name = "IphoneX",
                    Price = 6000M

                },
                new Product
                {
                    Id = new Guid("9ef19716-a95d-4422-8986-bc4f7f8ee594"),
                    Name = "Iphone11 ProMax",
                    Price = 9000M

                }
            };

            List<Stock> stocks = new List<Stock>
            {
                 new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = new Guid("9ef19716-a95d-4422-8986-bc4f7f8ee594"),
                    Piece  =1,
                    MaxPiece = 1,
                },
                new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = new Guid("203a7848-2fbb-4010-8ca3-edc7001d2843"),
                    Piece = 4,
                    MaxPiece = 3,

                },new Stock
                {
                   Id = Guid.NewGuid(),
                   ProductId = new Guid("f647f6be-15b1-4927-8e1c-bddb8663cb13"),
                   Piece = 5,
                   MaxPiece = 6,

                }
            };

            Products.AddRange(products);

            Stocks.AddRange(stocks);

            SaveChangesAsync();
        }
    }
}
