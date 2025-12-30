using CustomerService.Applilcation.Core.CQRS.CommandHandling;
using CustomerService.Applilcation.Core.Exceptions;
using CustomerService.Domain.Customers.ValueObjects;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Command;

public record UpdateCustomerCommand(Guid Id, string FirstName, string LastName, string Email, bool Enabled) : Command<bool>
{
    public override ValidationResult Validate()
    {
        return new UpdateCustomerCommandValidator().Validate(this);
    }
}

public class UpdateCustomerCommandHandler(ICustomerUnitOfWok unitOfWok) : CommandHandler<UpdateCustomerCommand, bool>
{
    public override async Task<bool> ExecuteCommand(UpdateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customer = await unitOfWok.Customers.GetAsync(new CustomerId(command.Id), cancellationToken);

        if (customer is null)
            throw new ApplicationDataException($"The Customer with Id '{command.Id}' not found");

        customer.ChangeName(command.FirstName, command.LastName);
        customer.ChangeEmail(command.Email);
        if (command.Enabled)
            customer.Enable();
        else
            customer.Disable();

        unitOfWok.Customers.Update(customer);

        var ex = await unitOfWok.CommitAsync(cancellationToken);

        if (ex <= 0)
            throw new ApplicationDataException("An error occurred while updating the customer.");

        return true;
    }
}

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Customer Id is required.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.")
                                 .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.")
                                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                             .EmailAddress().WithMessage("A valid email is required.")
                             .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
    }
}
