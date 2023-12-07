

using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class Repository<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} is null");
        }

        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
   
        _dbContext.Set<TEntity>().Remove(entity);
        await  _dbContext.SaveChangesAsync();
       
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} is null");
        }

        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
