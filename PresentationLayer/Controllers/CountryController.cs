using BusinessLogicLayer.Dtos.CountryDtos;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var list = await _countryService.GetAllAsync();
            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var country = await _countryService.GetByIdAsync(id);
            return Ok(country);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> AddCountryAsync([FromBody] AddCountryDto newCountry)
    {
        if (newCountry == null)
        {
            return BadRequest("New country data is null");
        }

        await _countryService.AddCountryDtoAsynce(newCountry);
        return Ok(newCountry);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateCountryAsync([FromBody] UpdateCountryDbo updatedCountry)
    {
        try
        {
            await _countryService.UpdateCountryDtoAsynce(updatedCountry);
            return Ok(updatedCountry);
        }

        catch (CountryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _countryService.DeleteCountryDtoAsynce(id);
            return Ok();
        }
        catch (CountryException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(int pageSize = 10, int pageNumber = 1)
    {
        var paged = await _countryService.GetPagedCountries(pageSize, pageNumber);

        var data = new
        {
            paged.TotalCount,
            paged.PageSize,
            paged.CurrentPage,
            paged.HasNext,
            paged.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data));

        return Ok(paged.Data);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterParametrs parametrs)
    {
        var cities = await _countryService.Filter(parametrs);
        var ip = HttpContext.Connection.RemoteIpAddress.ToString();
        return Ok(cities);
    }
}
