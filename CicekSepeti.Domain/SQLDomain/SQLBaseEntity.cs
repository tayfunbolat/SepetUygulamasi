using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CicekSepeti.Domain
{
    public class SQLBaseEntity : IBaseEntity
    {
 
        [Key]
        [BsonId]
        public override Guid Id { get; set; }
    }
}
