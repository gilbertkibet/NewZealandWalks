using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Dtos.ImageUpload
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
 

    }
}
