using System.Security.Cryptography;
using DIDemo.Interfaces;

namespace DIDemo.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hash, string salt) HashPassword(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(32);
        string salt = Convert.ToBase64String(saltBytes);
        byte[] bytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 10000, HashAlgorithmName.SHA256, 32);
        string hash = Convert.ToBase64String(bytes);

        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        var bytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 10000, HashAlgorithmName.SHA256, 32) ;
        
        string computedHash = Convert.ToBase64String(bytes);

        return computedHash == hash;
    }

}