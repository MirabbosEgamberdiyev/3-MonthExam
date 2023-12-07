

using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class CityRepository : Repository<City>, ICityInterface
{
    public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
