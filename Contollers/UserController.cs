using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

namespace MusicLibraryAPI.Controller;

/// <summary>
/// Controller for working with Users
/// </summary>
[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public UserController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _context.Users.AsNoTracking()
            .Include(x => x.UserSongs)
            .ThenInclude(x => x.Song)
            .ThenInclude(x => x.Genre)
            .ToListAsync();

        var response = _mapper.Map<List<GetUserResponse>>(result);

        return new OkObjectResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserSongs(int id, [FromQuery] bool isFavorite)
    {
        var result = await _context.Users.AsNoTracking()
            .Include(x => x.UserSongs
                .Where(x => x.isFavourite == isFavorite))
            .ThenInclude(x => x.Song)
            .ThenInclude(x => x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
            return NotFound();

        var response = _mapper.Map<GetUserResponse>(result);

        return new OkObjectResult(response);
    }

    [HttpPost()]
    public async Task<IActionResult> PostUser([FromBody] CreateUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new OkObjectResult(user);
    }

    [HttpPut("{id}/songs/{songId}")]
    public async Task<IActionResult> AddSongToUser(int id, int songId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == songId);

        if (user is null || song is null)
            return new NotFoundResult();


        var userSong = new UserSong
        {
            User = user,
            Song = song
        };

        _context.UserSongs.Add(userSong);
        await _context.SaveChangesAsync();

        return new OkResult();
    }

    [HttpDelete("{id}/songs/{songId}")]
    public async Task<IActionResult> RemoveSongFromUser(int id, int songId)
    {
        var userSong = await _context.UserSongs.FirstOrDefaultAsync(x => x.UserId == id && x.SongId == songId);

        if (userSong is null)
            return new NotFoundResult();


        _context.UserSongs.Remove(userSong);
        await _context.SaveChangesAsync();

        return new OkResult();
    }

    [HttpPut("{id}/songs{songId}/isFavorite")]
    public async Task<IActionResult> SetFavorite(int id, int songId, bool isFavourite)
    {
        var userSong = await _context.UserSongs.FirstOrDefaultAsync(x => x.UserId == id && x.SongId == songId);

        if (userSong is null)
            return new NotFoundResult();


        userSong.isFavourite = isFavourite;
        await _context.SaveChangesAsync();

        return new OkResult();
    }
}