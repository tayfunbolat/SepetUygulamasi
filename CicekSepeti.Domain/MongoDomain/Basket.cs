using CicekSepeti.Domain.SQLDomain;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicekSepeti.Domain.MongoDomain
{
    [BsonIgnoreExtraElements]
    public class Basket:MongoBaseEntity
    {

        public int ProductPiece { get; set; }
        public Product Product { get; set; }
        public string UserIpAdress { get; set; }

        public override string ToString()
        {
            return "Basket";
        }
    }
}
