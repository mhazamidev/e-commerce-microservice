using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductService.Applilcation.Core.CQRS.QueryHandling;
using ProductService.Applilcation.Core.DTO;
using ProductService.Applilcation.Core.Exceptions;
using ProductService.Infrastructure.Persistence.UOW;

namespace ProductService.Applilcation.Products.Query;

public record GetProductByNameQuery(string Name) : Query<ProductDto>
{
    public override ValidationResult Validate()
    {
        return new GetProductByNameQueryValidator().Validate(this);
    }
}


public class GetProductByNameQueryHandler(IProductUnitOfWok unitOfWok, IMapper mapper) : QueryHandler<GetProductByNameQuery, ProductDto>
{
    public override async Task<ProductDto> ExecuteQuery(GetProductByNameQuery query, CancellationToken cancellationToken)
    {
        var product = await unitOfWok.Products.GetByNameAsync(query.Name, cancellationToken);
        if (product is null)
            throw new ApplicationDataException($"Product with Name {query.Name} was not found.");

        return mapper.Map<ProductDto>(product);
    }
}

public class GetProductByNameQueryValidator : AbstractValidator<GetProductByNameQuery>
{
    public GetProductByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name must not be empty.");
    }
}