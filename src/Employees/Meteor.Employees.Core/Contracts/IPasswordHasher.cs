namespace Meteor.Employees.Core.Contracts;

public interface IPasswordHasher
{
    public (byte[] hash, byte[] salt) Hash(string password);

    public byte[] Hash(string password, byte[] salt);
}