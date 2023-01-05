namespace MusicLibraryAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<UserSong> UserSongs { get; set; } = new List<UserSong>();
}