using AutoMapper;
using CustomerService.Applilcation.Core.CQRS.QueryHandling;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Query;

public record GetActiveCustomersQuery(int PageSize, int CurrentPage) : Query<IEnumerable<ActiveCustomersDto>>
{
    public override ValidationResult Validate()
    {
        return new GetActiveCustomersQueryValidator().Validate(this);
    }
}

public class GetActiveCustomersQueryHandler(ICustomerUnitOfWok unitOfWok, IMapper mapper) : QueryHandler<GetActiveCustomersQuery, IEnumerable<ActiveCustomersDto>>
{
    public override async Task<IEnumerable<ActiveCustomersDto>> ExecuteQuery(GetActiveCustomersQuery query, CancellationToken cancellationToken)
    {
        var customers = await unitOfWok.Customers.GetActiveCustomersAsync(query.CurrentPage, query.PageSize);
        return mapper.Map<IEnumerable<ActiveCustomersDto>>(customers);
    }
}

public record ActiveCustomersDto(Guid Id, string FirstName, string LastName, string Email);


public class GetActiveCustomersQueryValidator : AbstractValidator<GetActiveCustomersQuery>
{
    public GetActiveCustomersQueryValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than zero.");
        RuleFor(x => x.CurrentPage)
            .GreaterThan(0)
            .WithMessage("Current page must be greater than zero.");
    }
}