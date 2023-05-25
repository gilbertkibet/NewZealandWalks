using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Infrastructure.Data;

namespace NewZealandWalks.API.Infrastructure.SqlServerImplementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NewZealandWalksDbContext _context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,NewZealandWalksDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<Image> Upload(Image image)
        {
            //local path to images
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", image.FileName,image.FileExtension);

            //upload image local path
            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);
            //https:servername/Images/image.jpg
            //httpcontext accessor to create path

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath= urlFilePath;

            //save changes to db
            await _context.Images.AddAsync(image);

            await _context.SaveChangesAsync();


            //return image back

            return image;









        }
    }
}
