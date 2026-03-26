using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, passwordHash);
    }
}
