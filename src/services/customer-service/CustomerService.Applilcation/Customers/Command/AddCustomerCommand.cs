using CustomerService.Applilcation.Core.CQRS.CommandHandling;
using CustomerService.Applilcation.Core.Exceptions;
using CustomerService.Domain.Customers.Entities;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Command;

public record AddCustomerCommand(string FirstName, string LastName, string Email, bool Enabled) : Command<Guid>
{
    public override ValidationResult Validate()
    {
        return new AddCustomerCommandValidator().Validate(this);
    }
}

public class AddCustomerCommandHandler(ICustomerUnitOfWok unitOfWok) : CommandHandler<AddCustomerCommand, Guid>
{
    public override async Task<Guid> ExecuteCommand(AddCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customer = Customer.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Enabled
        );
        await unitOfWok.Customers.AddAsync(customer);

        var ex = await unitOfWok.CommitAsync(cancellationToken);
        if (ex <= 0)
            throw new ApplicationDataException("Failed to add new customer.");

        return customer.Id.Value;
    }
}



public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
    }
}