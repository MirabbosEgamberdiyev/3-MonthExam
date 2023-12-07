

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Country:BaseEntity
{
    [Required, MinLength(5), MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public virtual List<City> Cities { get; set; } = new List<City>();
}
