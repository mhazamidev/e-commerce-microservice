using FluentValidation;
using FluentValidation.Results;
using OrderService.Application.Core.CQRS.CommandHandling;
using OrderService.Domain.Orders.ValueObjects;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Orders.Command;

public record PayOrderCommand(Guid OrderId) : Command<bool>
{
    public override ValidationResult Validate()
    {
        return new PayOrderCommandValidator().Validate(this);
    }
}


public class PayOrderCommandHandler(IOrderRepository _repository) : CommandHandler<PayOrderCommand, bool>
{
    public override async Task<bool> ExecuteCommand(PayOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await _repository
             .GetAsync(new OrderId(command.OrderId), cancellationToken);

        if (order is null)
            throw new ApplicationException("Order not found");

        order.Pay(); 

        _repository.Update(order);

        return true;
    }
}

public class PayOrderCommandValidator : AbstractValidator<PayOrderCommand>
{
    public PayOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");
    }
}