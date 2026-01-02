namespace OrderService.Application.DTO;

public record OrderDetailDto(Guid ProductId, int Quantity, decimal UnitPrice);
