

using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UnitOfWork(ICountryInterface countryInterface,
                        ICityInterface  cityInterface,
                        ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public ICityInterface CityInterface { get; } = cityInterface;

    public ICountryInterface CountryInterface { get; } = countryInterface;

    public void Dispose()
            =>GC.SuppressFinalize(this);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
