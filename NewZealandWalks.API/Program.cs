using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Helpers;
using NewZealandWalks.API.Infrastructure.Data;
using NewZealandWalks.API.Infrastructure.Implementations;
using NewZealandWalks.API.Infrastructure.SqlServerImplementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Nz Walks API",
        Version = "v1",


    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
     {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id=JwtBearerDefaults.AuthenticationScheme,
            },
            Scheme="Oauth2",
            Name=JwtBearerDefaults.AuthenticationScheme,
            In=ParameterLocation.Header,
        },
        new List<string>()
    }
    });
});



//getting connection string from the app settings.json
string connectionString = builder.Configuration.GetConnectionString("NewZealandWalksConnectionString");

string authConnectionString = builder.Configuration.GetConnectionString("NewZealandWalksAuthConnectionString");

//Injecting DbContext so that we can later used in our controller or interfaces
builder.Services.AddDbContext<NewZealandWalksDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<NewZealandWalksAuthDbContext>(options => options.UseSqlServer(authConnectionString));

//INJECTIIN REPOSITORY so that we can use anywhere in the application
builder.Services.AddScoped<IRegionRepository, RegionRepository>();

builder.Services.AddScoped<IWalkRepository, WalksRepository>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//AUTOMAAPPER injecting outmapper to the  builder to scan all the mapping when the application starts

builder.Services.AddAutoMapper(typeof(MappingProfile));


//ADD IDENTITY

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NzWalks")
    .AddEntityFrameworkStores<NewZealandWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

}
);
//add authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();
//add authetication to middleware pipeline
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
