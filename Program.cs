using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Midllewares;
using MusicLibraryAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ICacheService, CacheService>();

builder.Services.AddAutoMapper(typeof(MusicLibraryAPI.Profiles.MainProfile));
builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    );


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MusicLibraryAPI", 
        Version = "v1",
        Description = "Привіт вам від студентів ТА-03",
    });
    
     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     options.IncludeXmlComments(xmlPath);
});


var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// to see our endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();



