using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CicekSepeti.Domain
{
    public class MongoBaseEntity : IBaseEntity
    {
        [BsonId]
        public override Guid Id { get; set; }
    }
}
