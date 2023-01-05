namespace MusicLibraryAPI.Models.Response;

public class GetUserResponse
{
    public string FullName { get; set; }


    public ICollection<GetUserSongResponse> UserSongs { get; set; }
}