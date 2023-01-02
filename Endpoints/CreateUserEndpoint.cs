using Azure.Core;
using FastEndpoints;
using MusicLibraryAPI.Data;
using MusicLibraryAPI.Entities;
using MusicLibraryAPI.Models.Request;
using MusicLibraryAPI.Models.Response;

namespace MusicLibraryAPI.Endpoints;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    private readonly LibraryContext _dbContext;
    
    public CreateUserEndpoint(LibraryContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        _dbContext.Users.Add(new User
        {
            FirstName = req.FirstName,
            LastName = req.LastName
        });
        
        await SendOkAsync(new CreateUserResponse
            {
                FirstName = req.FirstName,
                LastName = req.LastName
            }, ct);
        
        await _dbContext.SaveChangesAsync(ct);
    }
}