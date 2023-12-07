

using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Dtos.CountryDtos;
using BusinessLogicLayer.Helpers;

namespace BusinessLogicLayer.Interfaces;

public interface ICountryService
{

    Task<IEnumerable<CountryDto>> GetAllAsync();
    Task<CountryDto> GetByIdAsync(int id);
    Task AddCountryDtoAsynce(AddCountryDto newCountryDto);
    Task DeleteCountryDtoAsynce(int id);
    Task UpdateCountryDtoAsynce(UpdateCountryDbo countryDto);

    Task<PagedList<CountryDto>> Filter(FilterParametrs parametrs);
    Task<PagedList<CountryDto>> GetPagedCountries(int pageSize, int pageNumber);
}
