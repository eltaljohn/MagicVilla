using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
	public class VillaCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Detail { get; set; }
        [Required]
        public double Price { get; set; }        
        public int Occupants { get; set; }
        public int SquareMeters { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}

