using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealandWalks.API.Infrastructure.Data
{
    public class NewZealandWalksAuthDbContext : IdentityDbContext
    {
        public NewZealandWalksAuthDbContext(DbContextOptions<NewZealandWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //list of roles
            var readerRoleId = "6c8ee484-689f-478a-a108-3c178e57e4a8";

            var writerRoleId = "a44d85b5-ef8f-4776-8c30-464599321ecd";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                  new IdentityRole()
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
