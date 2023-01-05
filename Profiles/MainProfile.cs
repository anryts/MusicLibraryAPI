using AutoMapper;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

namespace MusicLibraryAPI.Profiles;

public class MainProfile: Profile
{
    public MainProfile()
    {
        CreateMap<UserSong, GetUserSongResponse>()
            .ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Song.Genre.Name))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Song.Title));
        
        CreateMap<User, GetUserResponse>()
            .ForMember(dest=> dest.FullName, 
                opt=>opt.MapFrom(src=> $"{src.FirstName} {src.LastName}"));

        CreateMap<CreateUserRequest, User>();
        
        
    }
}