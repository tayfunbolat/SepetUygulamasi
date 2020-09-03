using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Domain.Models
{
    public class ProductBasket
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductPiece { get; set; }
    }
}
