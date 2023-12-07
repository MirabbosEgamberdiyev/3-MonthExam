using BusinessLogicLayer.Dtos.CityDtos;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController(ICityService cityService) : ControllerBase
{
    private readonly ICityService _cityService = cityService;

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var list = await _cityService.GetAllAsync();
            LoggingService.LogInfo("Cities took seccessfully");

            return Ok(list);
        }
        catch (CityException)
        {
            LoggingService.LogInfo("City not found");

            return NotFound("City not found");
        }
        catch (Exception)
        {
            LoggingService.LogInfo("Internal Server Error");

            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var city = await _cityService.GetByIdAsync(id);
            LoggingService.LogInfo("City took  seccessfully");

            return Ok(city);
        }
        catch (CityException)
        {
            LoggingService.LogInfo("City is not found");

            return NotFound("City not found");
        }
        catch (Exception)
        {
            LoggingService.LogInfo("500, Internal Server Error");
            return StatusCode(500, "Internal Server Error");
        }

    }

    [HttpPut("[action]")]
    public async Task<IActionResult> AddCityAsync([FromBody] AddCityDto newCity)
    {
        if (newCity == null)
        {
            LoggingService.LogInfo($"{newCity} is null here");

            throw new ArgumentNullException(nameof(newCity), "New city is null here");
        }
        else
        {
            await _cityService.AddCityDtoAsynce(newCity);
            return Ok(newCity); 
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> EditCityAsync(UpdateCityDto updatedCity)
    {
        try
        {
            await _cityService.UpdateCityDtoAsynce(updatedCity);
            LoggingService.LogInfo("Seccessfully edit");

            return Ok(updatedCity);
        }
        catch (CityException ex)
        {
            LoggingService.LogError("Some thing is error here");

            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            LoggingService.LogError("Try exiquit please");

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _cityService.DeleteCityDtoAsynce(id);
            LoggingService.LogInfo("Seccessfully deleted");

            return Ok();

        }
        catch (CityException ex)
        {
            LoggingService.LogInfo("City is not found");

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            LoggingService.LogInfo("City is not found");

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(int pageSize = 10, int pageNumber = 1)
    {
        var paged = await _cityService.GetPagedCities(pageSize, pageNumber);

        var data = new
        {
            paged.TotalCount,
            paged.PageSize,
            paged.CurrentPage,
            paged.HasNext,
            paged.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data));

        LoggingService.LogInfo("Pagination took");

        return Ok(paged.Data);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterParametrs parametrs)
    {
        var cities = await _cityService.Filter(parametrs);
        var ip = HttpContext.Connection.RemoteIpAddress.ToString();
        LoggingService.LogInfo("Filter is working");
        return Ok(cities);
    }
}
