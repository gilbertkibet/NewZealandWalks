using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Helpers;
using NewZealandWalks.API.Infrastructure.Data;
using NewZealandWalks.API.Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//connection string
string connectionString = builder.Configuration.GetConnectionString("NewZealandWalksConnectionString");

//Injecting DbContext
builder.Services.AddDbContext<NewZealandWalksDbContext>(options => options.UseSqlServer(connectionString));

//INJECTIIN REPOSITORY
builder.Services.AddScoped<IRegionRepository, RegionRepository>();

//AUTOMAAPPER
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
