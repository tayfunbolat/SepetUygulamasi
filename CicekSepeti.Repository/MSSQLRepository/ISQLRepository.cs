using CicekSepeti.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Repository.MSSQLRepository
{
    public interface ISQLRepository<TEntity> : IRepository<TEntity> where TEntity : SQLBaseEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter);
    }
}
