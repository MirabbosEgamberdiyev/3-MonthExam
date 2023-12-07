
using AutoMapper;
using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Dtos.CountryDtos;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Diagnostics.Metrics;

namespace BusinessLogicLayer.Services;

public class CountryService(IUnitOfWork unitOfWork,
                            IMapper mapper) : ICountryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddCountryDtoAsynce(AddCountryDto newCountryDto)
    {
        if (!newCountryDto.IsValidCountry())
        {
            LoggingService.LogError("Country is null here");
            throw new ArgumentException("Country is null here"); 
        }
        var list = await _unitOfWork.CountryInterface.GetAllAsync();
        var country = _mapper.Map<Country>(newCountryDto);

        if(list.Any(c => c.Name== country.Name))
        {
            LoggingService.LogError("Country is already exist");

            throw new CountryException("Country is already exist ");

        }
        else
        {
            await _unitOfWork.CountryInterface.AddAsync(country);
            LoggingService.LogInfo("Country added seccessfully");

            await _unitOfWork.SaveAsync();
        }

    }

    public async Task DeleteCountryDtoAsynce(int id)
    {
        var country = await _unitOfWork.CountryInterface.GetByIdAsync(id);

        LoggingService.LogError("Country deleted seccessfully");
        await _unitOfWork.CountryInterface.DeleteAsync(country ?? new Country());
    }

    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var list = await _unitOfWork.CountryInterface.GetAllAsync();
        LoggingService.LogError("GetAllCategory");

        return list.Select(c => _mapper.Map<CountryDto>(c));
    }

    public async Task<CountryDto> GetByIdAsync(int id)
    {
        var country = await _unitOfWork.CountryInterface.GetByIdAsync(id);

        LoggingService.LogInfo("Country added seccessfully");
        return _mapper.Map<CountryDto>(country);
    }

    public async Task UpdateCountryDtoAsynce(UpdateCountryDbo countryDto)
    {
        var list = await _unitOfWork.CountryInterface.GetAllAsync();
        var country  =  _mapper.Map<Country>(countryDto);
        if(list.Any(c => c.Equals(country) && c.Id != country.Id))
        {
            throw new CountryException("Country is already exist ");
        }
        await _unitOfWork.CountryInterface.UpdateAsync(country);
    }

    public async Task<PagedList<CountryDto>> GetPagedCountries(int pageSize, int pageNumber)
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();
        var dtos = list.Select(c => _mapper.Map<CountryDto>(c))
                       .ToList();

        PagedList<CountryDto> pagedList = new(dtos,
                                               dtos.Count(),
                                               pageNumber,
                                               pageSize);
        LoggingService.LogInfo("Country paginition is working ");

        return pagedList.ToPagedList(dtos, pageSize, pageNumber);
    }

    public async Task<PagedList<CountryDto>> Filter(FilterParametrs parametrs)
    {
        var list = await _unitOfWork.CityInterface.GetAllAsync();

        if (parametrs.Title is not "")
        {
            list = list.Where(country => country.Name.ToLower()
                .Contains(parametrs.Title.ToLower()));
        }


        var dtos = list.Select(country => _mapper.Map<CountryDto>(country)).ToList();
        if (parametrs.OrderByTitle)
        {
            dtos = dtos.OrderBy(country => country.Name).ToList();
        }
        else
        {
            dtos = dtos.OrderByDescending(country => country.Cities).ToList();
        }

        PagedList<CountryDto> pagedList = new(dtos, dtos.Count,
                                          parametrs.PageNumber, parametrs.PageSize);
        LoggingService.LogInfo("Country filter is working");

        return pagedList.ToPagedList(dtos, parametrs.PageSize, parametrs.PageNumber);
    }
}
