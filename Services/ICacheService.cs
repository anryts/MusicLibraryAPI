namespace MusicLibraryAPI.Services;

public interface ICacheService
{
    void Set(string key, object value, int minutes);
    T Get<T>(string key);
    void Remove(string key);
}