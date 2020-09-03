using CicekSepeti.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CicekSepeti.Repository.MongoRepository
{
    public interface IMongoRepository<TEntity> : IRepository<TEntity> where TEntity : MongoBaseEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> filter, object value);
    }
}
