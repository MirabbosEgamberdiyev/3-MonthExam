
namespace BusinessLogicLayer.Helpers;

public class CityException(string message):Exception
{
    private readonly string ErrorMessage = message;
}
