using FluentValidation;
using FluentValidation.Results;
using OrderService.Application.Core.CQRS.CommandHandling;
using OrderService.Domain.Orders.ValueObjects;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Orders.Command;

public record AddOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity, decimal UnitPrice) : Command<bool>
{
    public override ValidationResult Validate()
    {
        return new AddOrderItemCommandValidator().Validate(this);
    }
}

public class AddOrderItemCommandHandler(IOrderRepository _repository) : CommandHandler<AddOrderItemCommand, bool>
{
    public override async Task<bool> ExecuteCommand(AddOrderItemCommand command, CancellationToken cancellationToken = default)
    {
        var order = await _repository
           .GetAsync(new OrderId(command.OrderId), cancellationToken);

        if (order is null)
            throw new ApplicationException("Order not found");

        order.AddItem(
            ProductId.From(command.ProductId),
            command.Quantity,
            Money.From(command.UnitPrice)
        );

        _repository.Update(order);

        return true;
    }
}

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
    }
}