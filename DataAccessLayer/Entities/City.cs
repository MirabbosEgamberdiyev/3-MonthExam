using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class City : BaseEntity
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Area { get; set; }

        [Required, MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [ForeignKey("Country")]
        public  int CountryId { get; set; }

        public virtual Country Country { get; set; } = new Country();
    }
}
