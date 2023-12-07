

using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class CountryRepository : Repository<Country>, ICountryInterface
{
    public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }
}
