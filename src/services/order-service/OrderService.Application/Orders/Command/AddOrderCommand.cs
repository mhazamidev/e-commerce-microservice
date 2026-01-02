using FluentValidation;
using FluentValidation.Results;
using OrderService.Application.Core.CQRS.CommandHandling;
using OrderService.Domain.Orders.Entities;
using OrderService.Domain.Orders.ValueObjects;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Orders.Command;

public record AddOrderCommand(Guid CustomerId) : Command<Guid>
{
    public override ValidationResult Validate()
    {
        return new AddOrderCommandValidator().Validate(this);
    }
}

public class AddOrderCommandHandler(IOrderRepository _orderRepository) : CommandHandler<AddOrderCommand, Guid>
{
    public override async Task<Guid> ExecuteCommand(AddOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = Order.Create(
           CustomerId.From(command.CustomerId));

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.Id.Value;
    }
}

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId must not be empty.");
    }
}