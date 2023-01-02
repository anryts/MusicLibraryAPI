using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(); 
// builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<LibraryContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", async (LibraryContext context) =>
{
    var result = await context.Users.ToListAsync();
    return new OkObjectResult(result);
});

app.MapGet("/users/{id}", async (LibraryContext context, int id) =>
{
    var result = await context.Users.Include(x => x.UserSongs)
        .FirstOrDefaultAsync(x=> x.Id == id);
    return new OkObjectResult(result);
});

//TODO: Add response to user with list of music to prevent cycle reference

app.MapPost("/users", async ([FromBody] CreateUserRequest req ,LibraryContext context ) =>
{
    context.Users.Add( new User
    {
        FirstName = req.FirstName,
        LastName = req.LastName,
    });
    await context.SaveChangesAsync();
    return new OkObjectResult("User created");
});

app.MapPut("/users/{id}/songs/{song_id}", async (LibraryContext context, int id, int song_id) =>
{
    var user = await context.Users.FindAsync(id);
    var song = await context.Songs.FindAsync(song_id);
    user.UserSongs.Add(new UserSong
    {
        Song = song,
        User = user,
        isFavourite = false
    });
    await context.SaveChangesAsync();
    return new OkObjectResult("User updated");
});

app.MapPost("/songs", async ([FromBody] CreateSongRequest req ,LibraryContext context ) =>
{
    context.Songs.Add(new Song
    {
        Title = req.Title,
        Genre = await context.Genres.FirstOrDefaultAsync(g => g.Name == req.Genre) 
                ?? throw new Exception("Genre not found"),
    });
    await context.SaveChangesAsync();
    return new OkObjectResult(req);
});

app.MapGet("/songs/{id}", async (LibraryContext context, int id) =>
{
    var result = await context.Songs.FindAsync(id);
    return new OkObjectResult(result);
});


app.MapPost("/genres", async ([FromBody] CreateGenreRequest req ,LibraryContext context ) =>
{
    context.Genres.Add(new Genre
    {
        Name = req.Name,
    });
    await context.SaveChangesAsync();
    return new OkObjectResult(req);
});

app.MapGet("/genres", async (LibraryContext context) =>
{
    var result = await context.Genres.ToListAsync();
    return new OkObjectResult(result);
});

app.MapGet("/genres/{id}", async (LibraryContext context, int id) =>
{
    var result = await context.Genres.FindAsync(id);
    return new OkObjectResult(result);
});



app.UseHttpsRedirection();

app.Run();

