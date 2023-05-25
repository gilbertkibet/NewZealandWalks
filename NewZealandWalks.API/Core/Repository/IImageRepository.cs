using NewZealandWalks.API.Core.Entities;
using System.Net;

namespace NewZealandWalks.API.Core.Repository
{
    public interface IImageRepository
    {
       Task<Image> Upload(Image image);
    }
}
