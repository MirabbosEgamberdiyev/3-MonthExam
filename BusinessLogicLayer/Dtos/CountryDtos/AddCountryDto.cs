using BusinessLogicLayer.Dtos.CityDtos;

namespace BusinessLogicLayer.Dtos.CountryDtos;

public class AddCountryDto
{
    public string Name { get; set; } = string.Empty;

    public virtual List<CityDto> Cities { get; set; } = new List<CityDto>();
}
