using FluentValidation;
using FluentValidation.Results;
using ProductService.Applilcation.Core.CQRS.CommandHandling;
using ProductService.Infrastructure.Persistence.UOW;
using ProductWebApi.Domain.Entities;

namespace ProductService.Applilcation.Products.Command;

public record AddProductCommand(string Name, decimal Price, bool IsActive) : Command<Guid>
{
    public override ValidationResult Validate()
    {
        return new AddProductCommandValidator().Validate(this);
    }
}

public class AddProductCommandHandler(IProductUnitOfWok unitOfWok) : CommandHandler<AddProductCommand, Guid>
{
    public override async Task<Guid> ExecuteCommand(AddProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = Product.Create(command.Name, command.Price, command.IsActive);

        await unitOfWok.Products.AddAsync(product, cancellationToken);

        await unitOfWok.CommitAsync(cancellationToken);

        return product.Id.Value;
    }
}

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}
