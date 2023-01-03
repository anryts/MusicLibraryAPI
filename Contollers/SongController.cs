using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;

namespace MusicLibraryAPI.Controller;

[ApiController]
[Route("api/songs")]
[Produces("application/json")]
public class SongController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public SongController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<List<Song>> GetSongs()
    {
        var songs = _context.Songs.Include(x=> x.Genre)
            .ToListAsync();
        return Ok(songs.Result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Song> GetSong(int id)
    {
        var song = _context.Songs.Include(x=> x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);
        return Ok(song.Result);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostSong([FromBody] CreateSongRequest song)
    {
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