using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CicekSepeti.Domain.SQLDomain
{

    [Table("Product")]
    public class Product:SQLBaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
