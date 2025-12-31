namespace ProductService.Applilcation.Core.DTO;

public record ProductDto(Guid Id, string Name, decimal Price, bool IsActive);
