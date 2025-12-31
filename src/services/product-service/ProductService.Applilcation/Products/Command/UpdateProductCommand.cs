using FluentValidation;
using FluentValidation.Results;
using ProductService.Applilcation.Core.CQRS.CommandHandling;
using ProductService.Applilcation.Core.Exceptions;
using ProductService.Domain.Products.ValueObjects;
using ProductService.Infrastructure.Persistence.UOW;

namespace ProductService.Applilcation.Products.Command;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, bool IsActive) : Command<bool>
{
    public override ValidationResult Validate()
    {
        return new UpdateProductCommandValidator().Validate(this);
    }
}

public class UpdateProductCommandHandler(IProductUnitOfWok unitOfWok) : CommandHandler<UpdateProductCommand, bool>
{
    public override async Task<bool> ExecuteCommand(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWok.Products.GetByIdAsync(new ProductId(command.Id), cancellationToken);

        if (product is null)
            throw new ApplicationDataException($"Product not found. Id : {command.Id}");

        product.ChangePrice(command.Price);
        product.ChangeName(command.Name);
        if (command.IsActive)
            product.Active();
        else
            product.Inactive();

        unitOfWok.Products.Update(product);

        var commit = await unitOfWok.CommitAsync(cancellationToken);

        if (commit <= 0)
            throw new ApplicationDataException("Could not update the product.");

        return commit > 0;
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Product price must be greater than or equal to zero.");
    }
}
