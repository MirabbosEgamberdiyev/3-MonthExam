

namespace BusinessLogicLayer.Helpers;

public class CountryException(string message):Exception
{
    private readonly string ErrorMessage = message;
}
