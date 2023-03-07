using System.Security.Cryptography;
using Meteor.Employees.Core.Contracts;
using Meteor.Employees.Infrastructure.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Meteor.Employees.Infrastructure;

public class Pbkdf2PasswordHasher : IPasswordHasher
{
    private readonly IOptionsSnapshot<PasswordsOptions> _passwordOptions;

    public Pbkdf2PasswordHasher(IOptionsSnapshot<PasswordsOptions> passwordOptions)
    {
        _passwordOptions = passwordOptions;
    }

    public (byte[] hash, byte[] salt) Hash(string password)
    {
        var salt = GenerateSalt();
        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            _passwordOptions.Value.IterationsCount,
            _passwordOptions.Value.RequestPasswordByteLength
        );

        return (hash, salt);
    }

    public byte[] Hash(string password, byte[] salt)
        => KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            _passwordOptions.Value.IterationsCount,
            _passwordOptions.Value.RequestPasswordByteLength
        );

    private byte[] GenerateSalt()
        => RandomNumberGenerator.GetBytes(_passwordOptions.Value.GeneratedSaltLength);
}