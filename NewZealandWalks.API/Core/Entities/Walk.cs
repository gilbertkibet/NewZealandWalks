namespace NewZealandWalks.API.Core.Entities
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        //Navigation properties one to one relationship between walk and difficulty and walk and region ..this two properties are non nullable 

        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }
    }
}
