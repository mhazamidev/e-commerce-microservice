using Domain.SeedWork;
using System.Net.Mail;

namespace CustomerService.Domain.Customers.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        try
        {
            var mail = new MailAddress(email);
        }
        catch
        {
            throw new DomainException("Invalid email format.");
        }

        return new Email(email.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
