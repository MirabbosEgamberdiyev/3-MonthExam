using AutoMapper;
using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class CityServise(IUnitOfWork unitOfWork,
                          IMapper mapper) : ICityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddCityDtoAsynce(AddCityDto newCityDto)
    {
        if (!newCityDto.IsValidCity())
        {
            throw new CityException("City is null here ");
        }
        var list = await _unitOfWork.CityInterface.GetAllAsync();
        var city = _mapper.Map<City>(newCityDto);
        if (list.Any(list =>list.Name == city.Name &&
                            list.Language == list.Language &&
                            list.Country == city.Country &&
                            list.Area == city.Area &&
                            list.CountryId == city.CountryId
                ))
        {
            throw new CityException("City is already exist");
        }

        await _unitOfWork.CityInterface.AddAsync(city);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteCityDtoAsynce(int id)
    {
        var city = await _unitOfWork.CityInterface.GetByIdAsync(id);
        if (city == null)
        {
            throw new CityException("City not found ");
        }
        await _unitOfWork.CityInterface.DeleteAsync(city);
        await _unitOfWork.SaveAsync();
    }

    public async Task<PagedList<CityDto>> Filter(FilterParametrs parametrs)
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();

        if (parametrs.Title is not "")
        {
            list = list.Where(city => city.Name.ToLower()
                .Contains(parametrs.Title.ToLower()));
        }


        var dtos = list.Select(city => _mapper.Map<CityDto>(city)).ToList();
        if (parametrs.OrderByTitle)
        {
            dtos = dtos.OrderBy(city => city.Name).ToList();
        }
        else
        {
            dtos = dtos.OrderByDescending(city => city.Area).ToList();
        }

        PagedList<CityDto> pagedList = new(dtos, dtos.Count,
                                          parametrs.PageNumber, parametrs.PageSize);

        return pagedList.ToPagedList(dtos, parametrs.PageSize, parametrs.PageNumber);
    }

    public async Task<IEnumerable<CityDto>> GetAllAsync()
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();
        return list.Select(x => _mapper.Map<CityDto>(x));
    }
    
    public async Task<CityDto> GetByIdAsync(int id)
    {
        var city = await _unitOfWork.CityInterface.GetByIdAsync(id);
        return _mapper.Map<CityDto>(city);
    }

    public async Task<PagedList<CityDto>> GetPagedCities(int pageSize, int pageNumber)
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();
        var dtos = list.Select(c => _mapper.Map<CityDto>(c))
                       .ToList();

        PagedList<CityDto> pagedList = new(    dtos,
                                               dtos.Count(),
                                               pageNumber,
                                               pageSize);
        return pagedList.ToPagedList(dtos, pageSize, pageNumber);
    }

    public async Task UpdateCityDtoAsynce(UpdateCityDto cityDto)
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();
        var city = _mapper.Map<City>(cityDto);
        if(list.Any(c => c.Equals(city) && c.Id !=city.Id ))
        {
            throw new CityException("City is already exist here");
        }
        await _unitOfWork.CityInterface.UpdateAsync(city);
        await _unitOfWork.SaveAsync();

    }
}
