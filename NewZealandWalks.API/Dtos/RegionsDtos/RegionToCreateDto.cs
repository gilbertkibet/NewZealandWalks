using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Dtos.RegionsDtos
{
    public class RegionToCreateDto
    {
        //WE WANT THE FIRST TWO TO HAVE A VALUE CAN NEVER BE NULL
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 3 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
