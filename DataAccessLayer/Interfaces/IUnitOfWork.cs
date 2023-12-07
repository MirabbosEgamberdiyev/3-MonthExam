

namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork: IDisposable
{
    ICityInterface CityInterface { get; }
    ICountryInterface CountryInterface { get; }

    Task SaveAsync();
}
