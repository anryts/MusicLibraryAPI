using System.ComponentModel.DataAnnotations;

namespace MusicLibraryAPI.Entities;

public class UserSong
{
   public int Id { get; set; }
   public int SongId { get; set; }
   public int UserId { get; set; }
   
   public Song Song { get; set; }
   public User User { get; set; }
}