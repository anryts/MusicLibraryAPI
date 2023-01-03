using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Services;

namespace MusicLibraryAPI.Controller;

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
    public ActionResult<List<Song>> GetSongs()
    {
        if (_cacheService.Get<List<Song>>("songs") is not  null)
        {
            return _cacheService.Get<List<Song>>("songs");
        }
        
        var songs = _context.Songs.Include(x=> x.Genre)
            .ToListAsync();
        _cacheService.Set("songs", songs.Result,5);
        
        return Ok(songs.Result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Song> GetSong(int id)
    {
        if (_cacheService.Get<List<Song>>("songs") is not  null)
        {
            var cachedSong = _cacheService.Get<List<Song>>("songs").FirstOrDefault(x=> x.Id == id);
            return Ok(cachedSong);
        }
        var song = _context.Songs.Include(x=> x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);
        return Ok(song.Result);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostSong([FromBody] CreateSongRequest song)
    {
        if (String.IsNullOrWhiteSpace(song.Title) || String.IsNullOrWhiteSpace(song.Genre))
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
        _cacheService.Remove("songs");
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        if (song == null)
        {
            return NotFound();
        }
        song.Title = title;
        _context.Songs.Update(song);
        _context.SaveChangesAsync();
        return new OkObjectResult(song);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        _cacheService.Remove("songs");
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        if (song == null)
        {
            return NotFound();
        }
        _context.Songs.Remove(song);
        _context.SaveChangesAsync();
        return new OkObjectResult(song);
    }

}