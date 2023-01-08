using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Filters;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Services;

namespace MusicLibraryAPI.Controller;

/// <summary>
/// Controller for working with Songs
/// </summary>
[ApiController]
[Route("api/songs")]
[Produces("application/json")]
public class SongController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    public SongController(LibraryContext context, IMapper mapper, ICacheService cacheService)
    {
        _context = context;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Song>> GetSongs([FromQuery]FilterSong? filter)
    {

        var songs = _context.Songs.Include(x => x.Genre);

        List<Song>? result;
        if (filter.GenreName is null)
        {
            result = songs.ToList();
            return Ok(result);
        }

        result = songs
            .Where(x => x.Genre.Name == filter.GenreName)
            .OrderBy(x=> x.Title)
            .ToList();
        
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Song> GetSong(int id)
    {
        if (_cacheService.Get<List<Song>>($"songs{id}") is not  null)
        {
            var cachedSong = _cacheService.Get<Song>("$songs{id}");
            return Ok(cachedSong);
        }
        var song = _context.Songs.Include(x=> x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);
        _cacheService.Set($"song{id}", song, 5);      
        
        return Ok(song.Result);
    }

    
    [HttpPost]
    public async Task<IActionResult> PostSong([FromBody] CreateSongRequest song)
    {
        if (string.IsNullOrWhiteSpace(song.Title) || string.IsNullOrWhiteSpace(song.Genre))
            throw new Exception("Title and Genre are required");
        
        _context.Songs.Add(
            new Song
                {
                    Title = song.Title,
                    Genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == song.Genre) 
                    ?? throw new Exception("Genre not found"),
      
                }
        );
        _context.SaveChangesAsync();
        return new OkObjectResult(song);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSong(int id, [FromBody] string title)
    {
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        
        if (song is null)
            return NotFound();
        
        
        song.Title = title;
        _context.Songs.Update(song);
        _context.SaveChangesAsync();
        return new OkObjectResult(song);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        
        if (song is null)
            return NotFound();
        
        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
        return new OkObjectResult(song);
    }

}