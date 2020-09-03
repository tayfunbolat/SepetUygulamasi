using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CicekSepeti.Domain
{
    public abstract class IBaseEntity
    {
        [BsonIgnore]
        public abstract Guid Id { get; set; }
    }
}
