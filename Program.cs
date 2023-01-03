using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
// builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MusicLibraryAPI.Profiles.MainProfile));
builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    );


var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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



// app.MapGet("/users", async (LibraryContext context, IMapper mapper) =>
// {
//     var result = await context.Users.AsNoTracking()
//         .Include(x => x.UserSongs)
//         .ThenInclude(x => x.Song)
//         .ThenInclude(x => x.Genre)
//         .ToListAsync();
//
//     var response = mapper.Map<List<GetUserResponse>>(result);
//     
//     return new OkObjectResult(response);
// });
//
// app.MapGet("/users/{id}", async (LibraryContext context, int id, IMapper mapper ) =>
// {
//     var result = await context.Users.AsNoTracking()
//         .Include(x => x.UserSongs)
//         .ThenInclude(x => x.Song)
//         .ThenInclude(x => x.Genre)
//         .FirstOrDefaultAsync(x => x.Id == id);
//
//     var response = mapper.Map<GetUserResponse>(result);
//     
//     return new OkObjectResult(response);
// });
//
//
// app.MapPost("/users", async ([FromBody] CreateUserRequest req ,LibraryContext context ) =>
// {
//     context.Users.Add( new User
//     {
//         FirstName = req.FirstName,
//         LastName = req.LastName,
//     });
//     await context.SaveChangesAsync();
//     return new OkObjectResult("User created");
// });
//
// app.MapPut("/users/{id}/songs/{song_id}", async (LibraryContext context, int id, int song_id) =>
// {
//     var user = await context.Users.FindAsync(id);
//     var song = await context.Songs.FindAsync(song_id);
//     user.UserSongs.Add(new UserSong
//     {
//         Song = song,
//         User = user,
//         isFavourite = false
//     });
//     await context.SaveChangesAsync();
//     return new OkObjectResult("User updated");
// });
//
// app.MapPost("/songs", async ([FromBody] CreateSongRequest req ,LibraryContext context ) =>
// {
//     context.Songs.Add(new Song
//     {
//         Title = req.Title,
//         Genre = await context.Genres.FirstOrDefaultAsync(g => g.Name == req.Genre) 
//                 ?? throw new Exception("Genre not found"),
//     });
//     await context.SaveChangesAsync();
//     return new OkObjectResult(req);
// });
//
// app.MapGet("/songs/{id}", async (LibraryContext context, int id) =>
// {
//     var result = await context.Songs.FindAsync(id);
//     return new OkObjectResult(result);
// });
//
//
// app.MapPost("/genres", async ([FromBody] CreateGenreRequest req ,LibraryContext context ) =>
// {
//     context.Genres.Add(new Genre
//     {
//         Name = req.Name,
//     });
//     await context.SaveChangesAsync();
//     return new OkObjectResult(req);
// });
//
// app.MapGet("/genres", async (LibraryContext context) =>
// {
//     var result = await context.Genres.ToListAsync();
//     return new OkObjectResult(result);
// });
//
// app.MapGet("/genres/{id}", async (LibraryContext context, int id) =>
// {
//     var result = await context.Genres.FindAsync(id);
//     return new OkObjectResult(result);
// });








