namespace MusicLibraryAPI.Models.Response;

public class GetUserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public ICollection<GetUserSongResponse> UserSongs { get; set; }
}