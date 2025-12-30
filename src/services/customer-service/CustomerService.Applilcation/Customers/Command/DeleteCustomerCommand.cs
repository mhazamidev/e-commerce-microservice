using CustomerService.Applilcation.Core.CQRS.CommandHandling;
using CustomerService.Domain.Customers.ValueObjects;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Command;

public record DeleteCustomerCommand(Guid Id) : Command<bool>
{
    public override ValidationResult Validate()
    {
        return new DeleteCustomerCommandValidator().Validate(this);
    }
}

public class DeleteCustomerCommandHandler(ICustomerUnitOfWok UnitOfWok) : CommandHandler<DeleteCustomerCommand, bool>
{
    public override async Task<bool> ExecuteCommand(DeleteCustomerCommand command, CancellationToken cancellationToken = default)
    {
        await UnitOfWok.Customers.DeleteAsync(new CustomerId(command.Id));
        var ex = await UnitOfWok.CommitAsync(cancellationToken);
        return ex > 0;
    }
}

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer Id must not be empty.");
    }
}
