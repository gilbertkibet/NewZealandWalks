namespace NewZealandWalks.API.Dtos.RegionsDtos
{
    public class RegionToDisplayDto
    {

        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
