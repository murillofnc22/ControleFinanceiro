using System.Linq.Expressions;
using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiControleFinanceiro.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> DbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            DbSet = db.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }        
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
