using BusinessLogicLayer.Dtos.CountryDtos;

namespace BusinessLogicLayer.Dtos.CityDtos;

public class CityDto : BaseDto
{

    public string Name { get; set; } = string.Empty;

    public double Area { get; set; }
    public string Language { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public virtual CountryDto Country { get; set; } = new CountryDto();
}
