using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

namespace MusicLibraryAPI.Controller;

[ApiController]
[Route("api/genres")]
[Produces("application/json")]
public class GenreController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public GenreController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<GetGenreResponse>> GetGenres()
    {
        var genres = _context.Genres.ToList();
        return Ok(_mapper.Map<IEnumerable<GetGenreResponse>>(genres));
    }
    
    [HttpGet("{id}")]
    public ActionResult<GetGenreResponse> GetGenre(int id)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GetGenreResponse>(genre));
    }
    
    [HttpPost]
    public ActionResult<GetGenreResponse> CreateGenre([FromBody] CreateGenreRequest request)
    {
        var genre = _mapper.Map<Genre>(request);
        _context.Genres.Add(genre);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, _mapper.Map<GetGenreResponse>(genre));
    }
    
    [HttpPut("{id}")]
    public ActionResult<GetGenreResponse> UpdateGenre(int id, [FromBody] string request)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null)
        {
            return NotFound();
        }
        
        genre.Name = request;
        _context.SaveChanges();
        return Ok(_mapper.Map<GetGenreResponse>(genre));
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteGenre(int id)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null)
        {
            return NotFound();
        }
        
        _context.Genres.Remove(genre);
        _context.SaveChanges();
        return NoContent();
    }
    
    
    
}