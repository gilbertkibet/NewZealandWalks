using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.ImageUpload;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {

            ValidateFileUpload(imageUploadRequestDto);
            if(ModelState.IsValid) 
            {

                //convert dto to domain model repo deals with domain model

                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.Name),
                    FileSizeInBytes=imageUploadRequestDto.File.Length,
                    FileName=imageUploadRequestDto.FileName,
                    FileDescription=imageUploadRequestDto.FileDescription
                };

                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

               //use repo to upload
            }
            return BadRequest(ModelState);
        }
        [NonAction]
        public void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported File Extension");
            }

            if(imageUploadRequestDto.File.Length > 10485768) 
            {
                ModelState.AddModelError("File", "FileSize More Than 10mb");
            }
        
        }
    }
}
