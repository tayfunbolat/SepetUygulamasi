using CicekSepeti.Domain;
using CicekSepeti.Repository.MongoRepository;
using Common.Api;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : MongoBaseEntity
    {
        private readonly MongoContext _mongoContext;

        private readonly string tableName = null;
        public MongoRepository(MongoContext mongoContext)
        {

            _mongoContext = mongoContext;

            tableName = Activator.CreateInstance(typeof(TEntity)).ToString();
        }

        public virtual async Task AddAsync(TEntity entity)
        {

           await _mongoContext.GetMongoCollection<TEntity>(tableName)
                .InsertOneAsync(entity);
        }

        public Task AddRangeAsync(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        //public async Task DeleteAsync(string Id)
        //{

        //    var docId = GetInternalId(Id);

        //    await _mongoContext.Baskets<MongoBaseEntity>(_collectionName)
        //        .DeleteOneAsync(x => x.Id == docId);

        //}

        public Task DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<List<TEntity>> GetAllAsync()
        {

           return await _mongoContext.GetMongoCollection<TEntity>(tableName)
                .Find(new BsonDocument()).ToListAsync();
        }

        public async virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> filter,object value)
        {

            var filterDefinition = Builders<TEntity>.Filter.Eq(filter,value);

            return await _mongoContext.GetMongoCollection<TEntity>(tableName)
                 .Find(filterDefinition).ToListAsync();
        }

        public Task<TEntity> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
