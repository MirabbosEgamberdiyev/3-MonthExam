

using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)  {  }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
                    .HasData(new Country()
                    {
                        Id = 1,
                        Name = "Uzbekiston"
                    });

        //modelBuilder.Entity<City>()
        //            .HasData(new City()
        //            {
        //                Id = 1,
        //                Name = "Namangan",
        //                Area = 20000000,
        //                Language = "Uzbek",
        //                CountryId = 1
        //            });
        modelBuilder.Entity<City>()
                    .HasOne(city => city.Country)      
                    .WithMany(country => country.Cities) 
                    .HasForeignKey(city => city.CountryId);
    }
}
