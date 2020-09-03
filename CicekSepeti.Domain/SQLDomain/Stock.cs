using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CicekSepeti.Domain.SQLDomain
{
    public class Stock:SQLBaseEntity
    {
        public int Piece { get; set; }

        public int MaxPiece { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        

    }
}
