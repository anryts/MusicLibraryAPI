using System.Net;
using MusicLibraryAPI.Enums;

namespace MusicLibraryAPI.Exceptions;

public class BaseException : Exception
{
    public ErrorCode ErrorCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public BaseException(string message) : base(message) { }
}
