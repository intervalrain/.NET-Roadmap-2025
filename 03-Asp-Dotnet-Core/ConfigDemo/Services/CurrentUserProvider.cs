using ConfigDemo.Models;

using Microsoft.Extensions.Options;

namespace ConfigDemo.Services;

public class CurrentUserProvider : ICurrentUserProvider
{
    public CurrentUserProvider(IOptions<UserInfo> options)
    {
        var userInfo = options.Value;
        CurrentUser = new CurrentUser
        {
            Name = userInfo.Name,
            Email = userInfo.Email
        };
    }

    public CurrentUser CurrentUser { get; }
}