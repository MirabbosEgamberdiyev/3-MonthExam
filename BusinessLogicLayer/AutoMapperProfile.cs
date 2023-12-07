

using AutoMapper;
using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Dtos.CountryDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Country, CountryDto>()
            .ReverseMap();
        CreateMap<Country, AddCountryDto>();
        CreateMap<Country, UpdateCountryDbo>()
            .ReverseMap();

        CreateMap<City, CityDto>() 
            .ReverseMap();
        CreateMap<City, AddCityDto>();
        CreateMap<City, UpdateCityDto>()
            .ReverseMap();
    }
}
