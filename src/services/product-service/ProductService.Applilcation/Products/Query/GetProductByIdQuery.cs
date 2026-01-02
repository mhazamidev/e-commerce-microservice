using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductService.Applilcation.Core.CQRS.QueryHandling;
using ProductService.Applilcation.Core.Exceptions;
using ProductService.Applilcation.DTO;
using ProductService.Domain.Products.ValueObjects;
using ProductService.Infrastructure.Persistence.UOW;

namespace ProductService.Applilcation.Products.Query;

public record GetProductByIdQuery(Guid Id) : Query<ProductDto>
{
    public override ValidationResult Validate()
    {
        return new GetProductByIdQueryValidator().Validate(this);
    }
}

public class GetProductByIdQueryHandler(IProductUnitOfWok unitOfWok, IMapper mapper) : QueryHandler<GetProductByIdQuery, ProductDto>
{
    public override async Task<ProductDto> ExecuteQuery(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await unitOfWok.Products.GetByIdAsync(new ProductId(query.Id), cancellationToken);
        if (product is null)
            throw new ApplicationDataException($"Product with Id {query.Id} was not found.");

        return mapper.Map<ProductDto>(product);
    }
}

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product Id must not be empty.");
    }
}