using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using OrderService.Application.Core.CQRS.QueryHandling;
using OrderService.Application.Core.Exceptions;
using OrderService.Application.DTO;
using OrderService.Domain.Orders.ValueObjects;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Orders.Query;

public record GetOrderQuery(Guid Id) : Query<OrderDto>
{
    public override ValidationResult Validate()
    {
        return new GetOrderQueryValidator().Validate(this);
    }
}

public class GetOrderQueryHandler(IOrderRepository context, IMapper mapper) : QueryHandler<GetOrderQuery, OrderDto>
{
    public override async Task<OrderDto> ExecuteQuery(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = await context.GetAsync(new OrderId(query.Id), cancellationToken);
        if (order == null)
            throw new ApplicationDataException($"Order with ID {query.Id} was not found.");

        return mapper.Map<OrderDto>(order);
    }
}

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Order ID must not be empty.");
    }
}