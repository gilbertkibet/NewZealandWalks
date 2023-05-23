using NewZealandWalks.API.Dtos.Difficulty;
using NewZealandWalks.API.Dtos.RegionsDtos;

namespace NewZealandWalks.API.Dtos.WalksDtos
{
    public class WalkToDisplayDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public RegionToDisplayDto Region { get; set; }

        public DifficultyToDisplayDto Difficulty { get; set; }

    }
}
