namespace ConfigDemo.Models;

public class UserInfo
{
    public const string SectionName = nameof(UserInfo);

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public int Age { get; set; }
}