using FastEndpoints;
using MusicLibraryAPI.Entities;

namespace MusicLibraryAPI.Endpoints;

public class GetAllUsersEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(new
            {
                message = "Hello"
            }
            , ct);
    }
}