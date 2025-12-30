using AutoMapper;
using CustomerService.Applilcation.Core.CQRS.QueryHandling;
using CustomerService.Applilcation.Core.Exceptions;
using CustomerService.Domain.Customers.ValueObjects;
using CustomerService.Infrastructure.Persistence.UOW;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerService.Applilcation.Customers.Query;

public record GetCustomerQuery(Guid Id) : Query<GetCustomerDto>
{
    public override ValidationResult Validate()
    {
        return new GetCustomerQueryValidator().Validate(this);
    }
}

public class GetCustomerQueryHandler(ICustomerUnitOfWok unitOfWok, IMapper mapper) : QueryHandler<GetCustomerQuery, GetCustomerDto>
{
    public override async Task<GetCustomerDto> ExecuteQuery(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        var customer = await unitOfWok.Customers.GetAsync(new CustomerId(query.Id));
        if (customer is null)
            throw new ApplicationDataException($"The Customer with Id '{query.Id}' not found");

        return mapper.Map<GetCustomerDto>(customer);
    }
}

public record GetCustomerDto(Guid Id, string FirstName, string LastName, string Email);

public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID must not be empty.");
    }
}
