namespace MusicLibraryAPI.Models.Response;

public class GetUserSongResponse
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public bool isFavourite { get; set; }
}