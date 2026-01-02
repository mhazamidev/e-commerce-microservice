namespace ProductService.Applilcation.DTO;

public record ProductDto(Guid Id, string Name, decimal Price, bool IsActive);
