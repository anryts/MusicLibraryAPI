using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

namespace MusicLibraryAPI.Controller;

/// <summary>
/// Controller for working with Genres
/// </summary>
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

    /// <summary>
    /// Get all genres
    /// </summary>
    /// <response code="200">Return all genres</response>
    [HttpGet]
    public ActionResult<IEnumerable<Genre>> GetGenres()
    {
        var genres = _context.Genres.ToList();
        return genres;
    }

    /// <summary>
    /// Get genre with id
    /// </summary>
    /// <param name="id">id of genre you want to get</param>
    /// <response code="200">Return genre</response>
    /// <response code="400">If id is incorrect</response>
    /// <response code="404">If id does not exist</response>
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

    /// <summary>
    /// Create new genre
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Returns created genre</returns>
    [HttpPost]
    public ActionResult<GetGenreResponse> CreateGenre([FromBody] CreateGenreRequest request)
    {
        var genre = _mapper.Map<Genre>(request);
        _context.Genres.Add(genre);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, _mapper.Map<GetGenreResponse>(genre));
    }

    /// <summary>
    /// Update genre
    /// </summary>
    /// <param name="id">id of genre you want to update</param>
    /// <param name="request"></param>
    /// <response code="200">Updates genre</response>
    /// <response code="404">If id does not exist</response>
    [HttpPut("{id}")]
    public ActionResult<GetGenreResponse> UpdateGenre(int id, [FromBody] string request)
    {
        var genre = _context.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }
        
        genre.Name = request;
        _context.SaveChanges();
        return Ok(_mapper.Map<GetGenreResponse>(genre));
    }

    /// <summary>
    /// Delete genre
    /// </summary>
    /// <param name="id">id of genre you want to delete</param>
    /// <response code="200">Deletes genre</response>
    /// <response code="404">If id does not exist</response>
    [HttpDelete("{id}")]
    public ActionResult DeleteGenre(int id)
    {
        var genre = _context.Genres.Find(id);
        if (genre is null)
            return NotFound();
        
        
        _context.Genres.Remove(genre);
        _context.SaveChanges();
        return NoContent();
    }
}