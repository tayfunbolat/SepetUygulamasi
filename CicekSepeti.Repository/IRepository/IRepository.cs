using CicekSepeti.Domain;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    

    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entity);
        Task<List<TEntity>> GetAllAsync();

        void Update(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(Guid guid);

        Task<TEntity> GetByIdAsync(Guid Id);
    }

}
