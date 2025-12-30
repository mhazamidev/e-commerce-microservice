using CustomerService.Applilcation.Core.CQRS.CommandHandling;
using CustomerService.Applilcation.Core.Exceptions;
using CustomerService.Domain.Customers.ValueObjects;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Command;

public record AddAddressCommand(Guid CustomerId, string Line1, string? Line2, string City, string? State, string PostalCode, string Country, bool IsPrimary) : Command<Guid>
{
    public override ValidationResult Validate()
    {
        return new AddAddressCommandValidator().Validate(this);
    }
}


public class AddAddressCommandHandler(ICustomerUnitOfWok unitOfWok) : CommandHandler<AddAddressCommand, Guid>
{
    public override async Task<Guid> ExecuteCommand(AddAddressCommand command, CancellationToken cancellationToken = default)
    {
        var customer = await unitOfWok.Customers.GetAsync(new CustomerId(command.CustomerId), cancellationToken);

        if (customer is null)
            throw new ApplicationDataException("Customer not found");

        var address = customer.AddAddress(
            command.Line1,
            command.Line2,
            command.City,
            command.State,
            command.PostalCode,
            command.Country,
            command.IsPrimary);

        unitOfWok.Customers.Update(customer);

        await unitOfWok.CommitAsync(cancellationToken);

        return address.Id.Value;
    }
}

public class AddAddressCommandValidator : AbstractValidator<AddAddressCommand>
{
    public AddAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.Line1)
            .NotEmpty().WithMessage("Line1 is required.")
            .MaximumLength(200).WithMessage("Line1 must not exceed 200 characters.");
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");
        RuleFor(x => x.State)
            .MaximumLength(100).WithMessage("State must not exceed 100 characters.");
        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("PostalCode is required.")
            .MaximumLength(20).WithMessage("PostalCode must not exceed 20 characters.");
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
    }
}