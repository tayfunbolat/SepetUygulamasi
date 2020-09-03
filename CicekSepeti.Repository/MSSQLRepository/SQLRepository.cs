using CicekSepeti.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Repository.MSSQLRepository
{
    public class SQLRepository<TEntity> : ISQLRepository<TEntity> where TEntity : SQLBaseEntity
    {
        private readonly SQLContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public SQLRepository(SQLContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async virtual Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            await _dbSet.AddAsync(entity);
        }

        public async virtual Task AddRangeAsync(List<TEntity> entity) => await _dbSet.AddRangeAsync(entity);

        public async virtual Task DeleteAsync(TEntity entity)
        {
            entity = await GetByIdAsync(x => x.Id == entity.Id);

            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async virtual Task DeleteAsync(string Id)
        {

            var guid = new Guid(Id);

            var entity = await GetByIdAsync(x => x.Id == guid);

            try
            {
                if (entity != null)
                    _dbSet.Remove(entity);
            }

            catch (Exception dbEx)
            {

                throw new Exception(dbEx.Message, dbEx);
            }
        }

        //public async virtual List<TEntity> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid Id)  => await _dbSet.FirstOrDefaultAsync(x => x.Id == Id) ?? throw new Exception("Veri bulunamadı");

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter) ?? throw new Exception("Veri bulunamadı");

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
