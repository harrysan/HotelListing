using AspNetCoreRateLimit;
using HotelListing;
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    {
        Duration = 120
    });
})
        .AddNewtonsoftJson(op =>
    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListingAPI", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."

        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
    });
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configure MySQL
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("sqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 23))));

// Configure Caching
builder.Services.AddMemoryCache();

// Configure Rate Limiting
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();

// Configure Caching
builder.Services.ConfigureHttpCacheHeaders();

// Configure Identity
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddTransient<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Configure Versioning
builder.Services.ConfigureVersioning();

var app = builder.Build();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseIpRateLimiting();
//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=index}/{id?}");
//    endpoints.MapControllers();
//});
app.MapControllers();

app.Run();
