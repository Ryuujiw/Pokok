using System.ComponentModel.DataAnnotations;

namespace Pokok.Resources
{
    public class TreeResource
    {
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude { get; set; }
        public string species { get; set; }
    }
}
