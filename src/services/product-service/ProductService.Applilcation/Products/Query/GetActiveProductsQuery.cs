using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductService.Applilcation.Core.CQRS.QueryHandling;
using ProductService.Applilcation.Core.DTO;
using ProductService.Infrastructure.Persistence.UOW;

namespace ProductService.Applilcation.Products.Query;

public record GetActiveProductsQuery : Query<IEnumerable<ProductDto>>
{
    public override ValidationResult Validate()
    {
        return new GetActiveProductsQueryValidator().Validate(this);
    }
}

public class GetActiveProductsQueryHandler(IProductUnitOfWok unitOfWok, IMapper mapper) : QueryHandler<GetActiveProductsQuery, IEnumerable<ProductDto>>
{
    public override async Task<IEnumerable<ProductDto>> ExecuteQuery(GetActiveProductsQuery query, CancellationToken cancellationToken = default)
    {
        var products = await unitOfWok.Products.GetActivesAsync(cancellationToken);
        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}

public class GetActiveProductsQueryValidator : AbstractValidator<GetActiveProductsQuery>
{
    public GetActiveProductsQueryValidator()
    {
    }
}




