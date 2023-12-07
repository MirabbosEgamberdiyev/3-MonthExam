
using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Dtos.CountryDtos;

namespace BusinessLogicLayer.Helpers;

public static class Validator
{
    public static bool IsValidCity(this AddCityDto dto)
        => !string.IsNullOrEmpty(dto.Name) &&
           !string.IsNullOrEmpty(dto.Language) &&
           dto.Area > 0 &&
           dto.CountryId >= 0;
    public static bool IsValidCountry(this AddCountryDto dto)
        => !string.IsNullOrEmpty(dto.Name);
}
