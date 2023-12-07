
using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Helpers;

namespace BusinessLogicLayer.Interfaces;

public interface ICityService
{
    Task<PagedList<CityDto>> Filter(FilterParametrs parametrs);
    Task<PagedList<CityDto>> GetPagedCities(int pageSize, int pageNumber);
    Task<IEnumerable<CityDto>> GetAllAsync();
    Task<CityDto> GetByIdAsync(int id);
    Task AddCityDtoAsynce(AddCityDto newCityDto);
    Task DeleteCityDtoAsynce(int id);
    Task UpdateCityDtoAsynce(UpdateCityDto cityDto);
}
