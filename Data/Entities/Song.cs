namespace MusicLibraryAPI.Entities;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }

    public Genre Genre { get; set; }
    
}