using CicekSepeti.Domain;
using CicekSepeti.Domain.MongoDomain;
using Common.Api;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicekSepeti.Repository
{
    public class MongoContext:NoSQLContext
    {
        private IMongoDatabase _database = null;

        public MongoContext(IConnection Connection) : base(Connection)
        {
            Connect(Connection);
        }

        public IMongoCollection<T> GetMongoCollection<T>(string collectionName) where T: MongoBaseEntity
        {
            return _database.GetCollection<T>(collectionName);
        }

        protected override void Connect(IConnection connection)
        {
            var _mongoConnection = (MongoConnection)connection;

            var client = new MongoClient(_mongoConnection.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(_mongoConnection.Database);
        }
    }
}
