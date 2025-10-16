using Isopoh.Cryptography.Argon2;

namespace Pakiza.Application.Common.Interfaces;

public class PasswordHasher : IPasswordHasher
{
    private readonly byte[] _pepper;
    private readonly ILogger<PasswordHasher> _logger;

    public PasswordHasher(IConfiguration configuration, ILogger<PasswordHasher> logger)
    {
        _pepper = Convert.FromBase64String(configuration["PasswordHashing:Pepper"]
            ?? throw new InvalidOperationException("Pepper must be configured."));
        _logger = logger;
    }

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        byte[] salt = GenerateSalt();
        var config = new Argon2Config
        {
            Type = Argon2Type.HybridAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 3,
            MemoryCost = 131072,
            Threads = Environment.ProcessorCount,
            Password = Encoding.UTF8.GetBytes(password),
            Salt = salt,
            Secret = _pepper,
            HashLength = 32
        };

        try
        {
            return Argon2.Hash(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during password hashing.");
            throw;
        }
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(providedPassword))
            return false;

        try
        {
            return Argon2.Verify(hashedPassword, Encoding.UTF8.GetBytes(providedPassword), _pepper);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during password verification.");
            return false;
        }
    }

    private static byte[] GenerateSalt(int length = 16) => RandomNumberGenerator.GetBytes(length);
}

