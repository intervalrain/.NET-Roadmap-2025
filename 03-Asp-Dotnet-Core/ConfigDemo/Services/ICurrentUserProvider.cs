using ConfigDemo.Models;

namespace ConfigDemo.Services;

public interface ICurrentUserProvider
{
    public CurrentUser CurrentUser { get; }
}